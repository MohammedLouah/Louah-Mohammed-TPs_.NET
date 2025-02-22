using TP15.Entities;
using TP15.Entities.Enums;

namespace TP15.Repositories;

public class CompteRepository : ICompteRepository
{
    private readonly string _fichierPath = "../../../Data/comptes.txt";
    private readonly IUtilisateurRepository _utilisateurRepository;
    private List<Compte> _comptes;

    public CompteRepository(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
        _comptes = new List<Compte>();
        ChargerComptes();
    }

    public void Ajouter(Compte compte)
    {
        if (ObtenirParNumero(compte.Numero) != null)
            throw new Exception($"Un compte avec le numéro {compte.Numero} existe déjà");

        _comptes.Add(compte);
        SauvegarderComptes();
    }

    public void Modifier(Compte compte)
    {
        var compteExistant = ObtenirParNumero(compte.Numero);
        if (compteExistant == null)
            throw new Exception($"Compte {compte.Numero} non trouvé");

        var index = _comptes.IndexOf(compteExistant);
        _comptes[index] = compte;
        SauvegarderComptes();
    }

    public void Supprimer(string numero)
    {
        var compte = ObtenirParNumero(numero);
        if (compte == null)
            throw new Exception($"Compte {numero} non trouvé");

        _comptes.Remove(compte);
        SauvegarderComptes();
    }

    public Compte ObtenirParNumero(string numero)
    {
        return _comptes.FirstOrDefault(c => c.Numero == numero);
    }

    public List<Compte> ObtenirTout()
    {
        return _comptes.ToList();
    }

    private void ChargerComptes()
    {
        if (!File.Exists(_fichierPath))
        {
            File.Create(_fichierPath).Close();
            return;
        }

        var lignes = File.ReadAllLines(_fichierPath);
        foreach (var ligne in lignes)
        {
            if (string.IsNullOrEmpty(ligne)) continue;

            var donnees = ligne.Split(';');
            var compte = new Compte
            {
                Numero = donnees[0],
                IdProprietaire = int.Parse(donnees[1]),
                DateCreation = DateTime.Parse(donnees[3]),
                EstActif = bool.Parse(donnees[4])
            };
            
            // Chargement du solde
            decimal.TryParse(donnees[2], out decimal solde);
            if (solde > 0) compte.Crediter(solde);

            // Chargement des opérations
            if (donnees.Length > 5)
            {
                var operations = donnees[5].Split('|');
                foreach (var op in operations)
                {
                    var opDonnees = op.Split(',');
                    compte.Operations.Add(new Operation
                    {
                        Type = (TypeOperation)Enum.Parse(typeof(TypeOperation), opDonnees[0]),
                        Montant = decimal.Parse(opDonnees[1]),
                        Date = DateTime.Parse(opDonnees[2]),
                        Description = opDonnees[3]
                    });
                }
            }

            _comptes.Add(compte);
        }
    }

    private void SauvegarderComptes()
    {
        var lignes = new List<string>();
        foreach (var compte in _comptes)
        {
            var operations = string.Join("|", compte.Operations.Select(op =>
                $"{op.Type},{op.Montant},{op.Date},{op.Description}"));

            var ligne = $"{compte.Numero};{compte.IdProprietaire};{compte.Solde};" +
                       $"{compte.DateCreation};{compte.EstActif};{operations}";
            lignes.Add(ligne);
        }

        File.WriteAllLines(_fichierPath, lignes);
    }
}