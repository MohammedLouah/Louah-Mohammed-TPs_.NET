using TP15.Entities;
using TP15.Services;

namespace TP15.Controllers;

public class CompteController
{
    private readonly CompteService _compteService;
    private readonly AuthentificationController _authController;

    public CompteController(CompteService compteService, AuthentificationController authController)
    {
        _compteService = compteService;
        _authController = authController;
    }

    public void AfficherMenuPrincipal()
    {
        bool continuer = true;
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1. Créer un compte");
            Console.WriteLine("2. Effectuer une opération sur un compte");
            Console.WriteLine("3. Afficher tous les comptes");
            Console.WriteLine("4. Rechercher un compte");
            if (_authController.EstAdmin())
            {
                Console.WriteLine("5. Supprimer un compte");
            }
            Console.WriteLine("0. Déconnexion");

            Console.Write("\nChoix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1": CreerCompte(); break;
                case "2": EffectuerOperation(); break;
                case "3": AfficherTousLesComptes(); break;
                case "4": RechercherCompte(); break;
                case "5" when _authController.EstAdmin(): SupprimerCompte(); break;
                case "0": continuer = false; break;
                default:
                    Console.WriteLine("Option invalide!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void CreerCompte()
    {
        Console.Clear();
        Console.WriteLine("=== CRÉATION D'UN COMPTE ===\n");

        Console.Write("Numéro du compte : ");
        string numero = Console.ReadLine();

        try
        {
            _compteService.CreerCompte(numero, _authController.GetUtilisateurConnecte().Id);
            Console.WriteLine("\nCompte créé avec succès!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur : {ex.Message}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void EffectuerOperation()
    {
        Console.Clear();
        Console.WriteLine("=== OPÉRATION SUR UN COMPTE ===\n");

        Console.Write("Numéro du compte : ");
        string numero = Console.ReadLine();

        try
        {
            var compte = _compteService.ObtenirCompte(numero);
            AfficherMenuOperation(compte);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur : {ex.Message}");
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
    }

    private void AfficherMenuOperation(Compte compte)
    {
        bool continuer = true;
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine($"=== COMPTE N° {compte.Numero} ===");
            Console.WriteLine($"Solde : {compte.Solde} €\n");
            
            Console.WriteLine("1. Créditer");
            Console.WriteLine("2. Débiter");
            Console.WriteLine("3. Voir l'historique");
            Console.WriteLine("4. Effectuer un transfert");
            Console.WriteLine("0. Retour au menu principal");

            Console.Write("\nChoix : ");
            string choix = Console.ReadLine();

            switch (choix)
            {
                case "1": Crediter(compte.Numero); break;
                case "2": Debiter(compte.Numero); break;
                case "3": AfficherHistorique(compte.Numero); break;
                case "4": EffectuerTransfert(compte.Numero); break;
                case "0": continuer = false; break;
                default:
                    Console.WriteLine("Option invalide!");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    private void Crediter(string numero)
    {
        Console.Clear();
        Console.WriteLine("=== CRÉDIT ===\n");

        Console.Write("Montant : ");
        if (decimal.TryParse(Console.ReadLine(), out decimal montant))
        {
            Console.Write("Description : ");
            string description = Console.ReadLine();

            try
            {
                _compteService.Crediter(numero, montant, description);
                Console.WriteLine("\nCrédit effectué avec succès!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErreur : {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nMontant invalide!");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void Debiter(string numero)
    {
        Console.Clear();
        Console.WriteLine("=== DÉBIT ===\n");

        Console.Write("Montant : ");
        if (decimal.TryParse(Console.ReadLine(), out decimal montant))
        {
            Console.Write("Description : ");
            string description = Console.ReadLine();

            try
            {
                _compteService.Debiter(numero, montant, description);
                Console.WriteLine("\nDébit effectué avec succès!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErreur : {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nMontant invalide!");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void EffectuerTransfert(string numeroSource)
    {
        Console.Clear();
        Console.WriteLine("=== TRANSFERT ===\n");

        Console.Write("Numéro du compte destinataire : ");
        string numeroDestination = Console.ReadLine();

        Console.Write("Montant : ");
        if (decimal.TryParse(Console.ReadLine(), out decimal montant))
        {
            Console.Write("Description : ");
            string description = Console.ReadLine();

            try
            {
                _compteService.Transferer(numeroSource, numeroDestination, montant, description);
                Console.WriteLine("\nTransfert effectué avec succès!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErreur : {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("\nMontant invalide!");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void AfficherHistorique(string numero)
    {
        Console.Clear();
        Console.WriteLine("=== HISTORIQUE DES OPÉRATIONS ===\n");

        try
        {
            var operations = _compteService.ObtenirHistorique(numero);
            if (!operations.Any())
            {
                Console.WriteLine("Aucune opération trouvée.");
            }
            else
            {
                foreach (var op in operations)
                {
                    Console.WriteLine($"{op.Date:dd/MM/yyyy HH:mm} - {op.Type} - {op.Montant} € - {op.Description}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void AfficherTousLesComptes()
    {
        Console.Clear();
        Console.WriteLine("=== LISTE DES COMPTES ===\n");

        var comptes = _compteService.ObtenirTousLesComptes();
        if (!comptes.Any())
        {
            Console.WriteLine("Aucun compte trouvé.");
        }
        else
        {
            foreach (var compte in comptes)
            {
                Console.WriteLine($"N° {compte.Numero} - Solde: {compte.Solde} € - Créé le: {compte.DateCreation:dd/MM/yyyy}");
            }
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void RechercherCompte()
    {
        Console.Clear();
        Console.WriteLine("=== RECHERCHE DE COMPTE ===\n");

        Console.Write("Numéro du compte : ");
        string numero = Console.ReadLine();

        try
        {
            var compte = _compteService.ObtenirCompte(numero);
            Console.WriteLine($"\nN° {compte.Numero}");
            Console.WriteLine($"Solde: {compte.Solde} €");
            Console.WriteLine($"Date de création: {compte.DateCreation:dd/MM/yyyy}");
            Console.WriteLine($"Statut: {(compte.EstActif ? "Actif" : "Inactif")}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur : {ex.Message}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }

    private void SupprimerCompte()
    {
        if (!_authController.EstAdmin())
        {
            Console.WriteLine("\nAccès refusé. Seul un administrateur peut supprimer des comptes.");
            Thread.Sleep(2000);
            return;
        }

        Console.Clear();
        Console.WriteLine("=== SUPPRESSION DE COMPTE ===\n");

        Console.Write("Numéro du compte à supprimer : ");
        string numero = Console.ReadLine();

        try
        {
            _compteService.SupprimerCompte(numero);
            Console.WriteLine("\nCompte supprimé avec succès!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur : {ex.Message}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour continuer...");
        Console.ReadKey();
    }
}