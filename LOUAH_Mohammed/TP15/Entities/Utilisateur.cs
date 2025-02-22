using TP15.Entities.Enums;

namespace TP15.Entities;

public class Utilisateur
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }  // Dans un vrai système, il faudrait hasher le mot de passe
    public TypeUtilisateur Role { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public DateTime DateCreation { get; set; }

    public Utilisateur()
    {
        DateCreation = DateTime.Now;
    }
}