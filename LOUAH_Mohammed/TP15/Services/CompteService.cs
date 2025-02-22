using TP15.Entities;
using TP15.Entities.Enums;
using TP15.Repositories;

namespace TP15.Services;

public class CompteService
{
    private readonly ICompteRepository _compteRepository;
    private readonly IUtilisateurRepository _utilisateurRepository;

    public CompteService(ICompteRepository compteRepository, IUtilisateurRepository utilisateurRepository)
    {
        _compteRepository = compteRepository;
        _utilisateurRepository = utilisateurRepository;
    }

    public void CreerCompte(string numero, int idProprietaire)
    {
        var proprietaire = _utilisateurRepository.ObtenirParId(idProprietaire);
        if (proprietaire == null)
            throw new Exception("Propriétaire non trouvé");

        var compte = new Compte
        {
            Numero = numero,
            IdProprietaire = idProprietaire,
            DateCreation = DateTime.Now,
            EstActif = true,
            Operations = new List<Operation>()
        };

        _compteRepository.Ajouter(compte);
    }

    public void Crediter(string numero, decimal montant, string description)
    {
        if (montant <= 0)
            throw new Exception("Le montant doit être positif");

        var compte = ObtenirCompte(numero);
        
        var operation = new Operation
        {
            Type = TypeOperation.Credit,
            Montant = montant,
            Date = DateTime.Now,
            Description = description
        };

        compte.Operations.Add(operation);
        compte.Crediter(montant);
        _compteRepository.Modifier(compte);
    }

    public void Debiter(string numero, decimal montant, string description)
    {
        if (montant <= 0)
            throw new Exception("Le montant doit être positif");

        var compte = ObtenirCompte(numero);
        if (compte.Solde < montant)
            throw new Exception("Solde insuffisant");

        var operation = new Operation
        {
            Type = TypeOperation.Debit,
            Montant = montant,
            Date = DateTime.Now,
            Description = description
        };

        compte.Operations.Add(operation);
        compte.Debiter(montant);
        _compteRepository.Modifier(compte);
    }

    public void Transferer(string numeroSource, string numeroDestination, decimal montant, string description)
    {
        if (numeroSource == numeroDestination)
            throw new Exception("Impossible de faire un transfert vers le même compte");

        var compteSource = ObtenirCompte(numeroSource);
        var compteDestination = ObtenirCompte(numeroDestination);

        if (compteSource.Solde < montant)
            throw new Exception("Solde insuffisant pour le transfert");

        // Créer les opérations pour les deux comptes
        var operationDebit = new Operation
        {
            Type = TypeOperation.Transfert,
            Montant = montant,
            Date = DateTime.Now,
            Description = $"Transfert vers {numeroDestination}: {description}"
        };

        var operationCredit = new Operation
        {
            Type = TypeOperation.Transfert,
            Montant = montant,
            Date = DateTime.Now,
            Description = $"Transfert reçu de {numeroSource}: {description}"
        };

        // Effectuer les opérations
        compteSource.Operations.Add(operationDebit);
        compteSource.Debiter(montant);
        
        compteDestination.Operations.Add(operationCredit);
        compteDestination.Crediter(montant);

        // Sauvegarder les modifications
        _compteRepository.Modifier(compteSource);
        _compteRepository.Modifier(compteDestination);
    }

    public Compte ObtenirCompte(string numero)
    {
        var compte = _compteRepository.ObtenirParNumero(numero);
        if (compte == null)
            throw new Exception($"Compte {numero} non trouvé");
        return compte;
    }

    public List<Compte> ObtenirTousLesComptes()
    {
        return _compteRepository.ObtenirTout();
    }

    public List<Operation> ObtenirHistorique(string numero)
    {
        var compte = ObtenirCompte(numero);
        return compte.Operations.OrderByDescending(o => o.Date).ToList();
    }

    public void SupprimerCompte(string numero)
    {
        var compte = ObtenirCompte(numero);
        if (compte.Solde > 0)
            throw new Exception("Impossible de supprimer un compte avec un solde positif");

        _compteRepository.Supprimer(numero);
    }

    public bool CompteExiste(string numero)
    {
        return _compteRepository.ObtenirParNumero(numero) != null;
    }
}