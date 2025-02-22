using TP15.Entities;
using TP15.Entities.Enums;
using TP15.Repositories;

namespace TP15.Services;

public class AuthService
{
    private readonly IUtilisateurRepository _utilisateurRepository;
    private Utilisateur _currentUser;

    public AuthService(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository;
    }

    public Utilisateur Login(string login, string password)
    {
        var utilisateur = _utilisateurRepository.ObtenirParLogin(login);
        if (utilisateur == null || utilisateur.Password != password)
            throw new Exception("Login ou mot de passe incorrect");

        _currentUser = utilisateur;
        return utilisateur;
    }

    public bool IsAdmin(Utilisateur utilisateur)
    {
        return utilisateur.Role == TypeUtilisateur.Administrateur;
    }

    public void Logout()
    {
        _currentUser = null;
    }
}
