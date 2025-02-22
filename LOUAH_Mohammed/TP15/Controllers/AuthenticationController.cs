using TP15.Entities;
using TP15.Services;

namespace TP15.Controllers;

public class AuthentificationController
{
    private readonly AuthService _authService;
    private Utilisateur _utilisateurConnecte;

    public AuthentificationController(AuthService authService)
    {
        _authService = authService;
    }

    public void AfficherMenuConnexion()
    {
        bool connecte = false;
        do
        {
            Console.Clear();
            Console.WriteLine("=== AUTHENTIFICATION ===");
            Console.WriteLine();
            
            Console.Write("Login : ");
            string login = Console.ReadLine();
            
            Console.Write("Mot de passe : ");
            string password = Console.ReadLine();

            try
            {
                _utilisateurConnecte = _authService.Login(login, password);
                connecte = true;
                Console.WriteLine($"\nBienvenue {_utilisateurConnecte.Prenom} {_utilisateurConnecte.Nom}!");
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErreur : {ex.Message}");
                Console.WriteLine("Appuyez sur une touche pour réessayer...");
                Console.ReadKey();
            }
        } while (!connecte);
    }

    public void Deconnecter()
    {
        _authService.Logout();
        _utilisateurConnecte = null;
    }

    public bool EstAdmin()
    {
        return _authService.IsAdmin(_utilisateurConnecte);
    }

    public Utilisateur GetUtilisateurConnecte()
    {
        return _utilisateurConnecte;
    }
}