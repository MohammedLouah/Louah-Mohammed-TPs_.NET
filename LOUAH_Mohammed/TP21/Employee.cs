using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace TP21;

public class Employee
{
    private string nom;
    private float salaire;
    private string poste;
    private DateOnly date_embauche;

    public Employee(string nom, float salaire, string poste, DateOnly dateEmbauche)
    {
        this.nom = nom;
        this.salaire = salaire;
        this.poste = poste;
        date_embauche = dateEmbauche;
    }

    public string Nom
    {
        get => nom;
        set => nom = value;
    }

    public float Salaire
    {
        get => salaire;
        set => salaire = value;
    }

    public string Poste
    {
        get => poste;
        set => poste = value;
    }

    public DateOnly DateEmbauche
    {
        get => date_embauche;
        set => date_embauche = value;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        Employee emp = (Employee)obj;
        return nom == emp.Nom && poste == emp.Poste && date_embauche == emp.DateEmbauche;
    }

    public override string ToString()
    {
        return $"  {nom}, poste: {poste}, salaire: {salaire}, Date d'embauche: {date_embauche}";
    }
}