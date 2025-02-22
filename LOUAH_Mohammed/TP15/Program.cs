using TP15.Controllers;
using TP15.Repositories;
using TP15.Services;

namespace TP15;

public class Program
{
    static void Main(string[] args)
    {
        // Configuration de l'encodage pour les caractères spéciaux
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        try
        {
            // Initialisation des repositories
            var utilisateurRepository = new UtilisateurRepository();
            var compteRepository = new CompteRepository(utilisateurRepository);

            // Initialisation des services
            var authService = new AuthService(utilisateurRepository);
            var compteService = new CompteService(compteRepository, utilisateurRepository);

            // Initialisation des controllers
            var authController = new AuthentificationController(authService);
            var compteController = new CompteController(compteService, authController);

            // Tests des fonctionnalités
            ExecuterTests(authController, compteController);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Une erreur est survenue : {ex.Message}");
            Console.WriteLine("Appuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }

    private static void ExecuterTests(AuthentificationController authController, CompteController compteController)
    {
        bool continuer = true;
        while (continuer)
        {
            Console.Clear();
            Console.WriteLine("=== APPLICATION BANCAIRE ===\n");

            // Authentification
            authController.AfficherMenuConnexion();

            // Menu principal après connexion réussie
            compteController.AfficherMenuPrincipal();

            // Déconnexion
            authController.Deconnecter();

            // Demander si l'utilisateur veut continuer
            Console.Clear();
            Console.WriteLine("Voulez-vous démarrer une nouvelle session ? (O/N)");
            continuer = Console.ReadLine()?.ToUpper() == "O";
        }

        Console.WriteLine("\nMerci d'avoir utilisé notre application bancaire!");
        Console.WriteLine("Appuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}