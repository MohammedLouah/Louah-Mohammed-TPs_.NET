using TP15.Entities;

namespace TP15.Repositories;

public interface IUtilisateurRepository
{
    void Ajouter(Utilisateur utilisateur);
    void Modifier(Utilisateur utilisateur);
    void Supprimer(int id);
    Utilisateur ObtenirParId(int id);
    Utilisateur ObtenirParLogin(string login);
    List<Utilisateur> ObtenirTout();
    bool Existe(string login);
}