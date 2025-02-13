using System.Collections;
using System.Security.AccessControl;

namespace TP12;

public class Projet
{
    private string code;
    private string sujet;
    private int duree;
    private List<Programmeur> programmeurs = new List<Programmeur>();

    public Projet(string code, string sujet, int duree)
    {
        this.code = code;
        this.sujet = sujet;
        this.duree = duree;
    }

    public void AjouterProgrammeur(Programmeur programmeur)
    {
        if (!programmeurs.Contains(programmeur))
        {
            programmeur.Consommations = new Consommation[duree];
            programmeurs.Add(programmeur);
        }
        else
        {
            Console.WriteLine("Ce programmeur est deja existe!");
        }
    }

    public Programmeur RechercherProgrammeur(int id)
    {
        foreach (Programmeur programmeur in programmeurs)
        {
            if (programmeur.Id == id)
                return programmeur;
        }

        return null;
    }
    
    public Programmeur RechercherProgrammeur(string nom)
    {
        foreach (Programmeur programmeur in programmeurs)
        {
            if (programmeur.Nom == nom)
                return programmeur;
        }

        return null;
    }

    public void AfficherProgrammeur(int id)
    {
        Programmeur programmeur = RechercherProgrammeur(id);
        if(programmeur!=null)
            Console.WriteLine(programmeur);
        else
        {
            Console.WriteLine("Ce programmeur n'existe pas dans ce projet");
        }
    }

    public void AfficherListe()
    {
        Console.WriteLine("Liste des programmeurs:");
        foreach (Programmeur programmeur in programmeurs)
        {
          Console.WriteLine(programmeur);
        }
    }

    public void Supprimer(int id)
    {
        Programmeur programmeur = RechercherProgrammeur(id);
        if (programmeur != null)
        {
            programmeurs.Remove(programmeur);
        }
        else
        {
            Console.WriteLine("Ce programmeur n'existe pas dans ce projet");
        }
    }

    public void AjouterConsommation(Programmeur programmeur, Consommation consommation, int numSemaine)
    {
        if (numSemaine <= this.duree)
        {
            programmeur.Consommations[numSemaine-1] = consommation;
        }
        else
        {
            Console.WriteLine("Vous avez depasser le nombre de semaines possible!");
        }
    }

    public void ChangerBureau(Programmeur programmeur, int newBureau)
    {
        programmeur.Bureau = newBureau;
    }

    public void AfficherTotalCafeParSemaine(int numSemaine)
    {
        int total = 0;
        foreach (Programmeur programmeur in programmeurs)
        {
            Consommation consommation = programmeur.Consommations[numSemaine - 1];
            if (consommation!=null) total += consommation.NbTasses1;
        }
        Console.WriteLine($"le nombre total de tasses de café consommé par la semaine {numSemaine} est: {total}");
    }
    
    
    
}