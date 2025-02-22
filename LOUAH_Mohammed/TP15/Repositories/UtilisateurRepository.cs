using TP15.Entities;
using TP15.Entities.Enums;

namespace TP15.Repositories;

public class UtilisateurRepository : IUtilisateurRepository
{
    private readonly string _fichierPath = "../../../Data/Utilisateurs.json";
    private List<Utilisateur> _utilisateurs;

    public UtilisateurRepository()
    {
        _utilisateurs = new List<Utilisateur>();
        ChargerUtilisateurs();
    }

    public void Ajouter(Utilisateur utilisateur)
    {
        if (Existe(utilisateur.Login))
            throw new Exception($"Un utilisateur avec le login {utilisateur.Login} existe déjà");

        // Attribution d'un nouvel ID
        utilisateur.Id = _utilisateurs.Any() ? _utilisateurs.Max(u => u.Id) + 1 : 1;
        utilisateur.DateCreation = DateTime.Now;

        _utilisateurs.Add(utilisateur);
        SauvegarderUtilisateurs();
    }

    public void Modifier(Utilisateur utilisateur)
    {
        var utilisateurExistant = ObtenirParId(utilisateur.Id);
        if (utilisateurExistant == null)
            throw new Exception($"Utilisateur avec l'ID {utilisateur.Id} non trouvé");

        // Vérifier si le nouveau login n'est pas déjà utilisé par un autre utilisateur
        var autreUtilisateurMemeLogin = _utilisateurs.FirstOrDefault(u => 
            u.Login == utilisateur.Login && u.Id != utilisateur.Id);
        if (autreUtilisateurMemeLogin != null)
            throw new Exception($"Le login {utilisateur.Login} est déjà utilisé");

        var index = _utilisateurs.IndexOf(utilisateurExistant);
        // Conserver la date de création originale
        utilisateur.DateCreation = utilisateurExistant.DateCreation;
        _utilisateurs[index] = utilisateur;
        
        SauvegarderUtilisateurs();
    }

    public void Supprimer(int id)
    {
        var utilisateur = ObtenirParId(id);
        if (utilisateur == null)
            throw new Exception($"Utilisateur avec l'ID {id} non trouvé");

        _utilisateurs.Remove(utilisateur);
        SauvegarderUtilisateurs();
    }

    public Utilisateur ObtenirParId(int id)
    {
        return _utilisateurs.FirstOrDefault(u => u.Id == id);
    }

    public Utilisateur ObtenirParLogin(string login)
    {
        return _utilisateurs.FirstOrDefault(u => u.Login.ToLower() == login.ToLower());
    }

    public List<Utilisateur> ObtenirTout()
    {
        return _utilisateurs.ToList();
    }

    public bool Existe(string login)
    {
        return _utilisateurs.Any(u => u.Login.ToLower() == login.ToLower());
    }

    private void ChargerUtilisateurs()
    {
        if (!File.Exists(_fichierPath))
        {
            CreerUtilisateursParDefaut();
            return;
        }

        try
        {
            var jsonContent = File.ReadAllText(_fichierPath);
            _utilisateurs = System.Text.Json.JsonSerializer.Deserialize<List<Utilisateur>>(jsonContent) 
                          ?? new List<Utilisateur>();

            if (!_utilisateurs.Any())
            {
                CreerUtilisateursParDefaut();
            }
        }
        catch (Exception)
        {
            // En cas d'erreur de lecture du fichier, créer les utilisateurs par défaut
            CreerUtilisateursParDefaut();
        }
    }

    private void SauvegarderUtilisateurs()
    {
        var options = new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true
        };
        
        var jsonContent = System.Text.Json.JsonSerializer.Serialize(_utilisateurs, options);
        File.WriteAllText(_fichierPath, jsonContent);
    }

    private void CreerUtilisateursParDefaut()
    {
        _utilisateurs = new List<Utilisateur>
        {
            new Utilisateur
            {
                Id = 1,
                Login = "admin",
                Password = "admin", // Dans un vrai système, utiliser un hash
                Role = TypeUtilisateur.Administrateur,
                Nom = "Admin",
                Prenom = "System",
                DateCreation = DateTime.Now
            },
            new Utilisateur
            {
                Id = 2,
                Login = "client",
                Password = "client", // Dans un vrai système, utiliser un hash
                Role = TypeUtilisateur.Client,
                Nom = "Client",
                Prenom = "Test",
                DateCreation = DateTime.Now
            }
        };

        SauvegarderUtilisateurs();
    }
}