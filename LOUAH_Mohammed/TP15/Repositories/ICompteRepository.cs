using TP15.Entities;

namespace TP15.Repositories;

public interface ICompteRepository
{
    void Ajouter(Compte compte);
    void Modifier(Compte compte);
    void Supprimer(string numero);
    Compte ObtenirParNumero(string numero);
    List<Compte> ObtenirTout();
}