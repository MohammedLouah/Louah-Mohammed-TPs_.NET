namespace TP11;

public class Repertoire
{
    private string nom;
    private int nbr_fichiers;
    private Fichier[] fichiers = new Fichier[30];

    public Repertoire(string nom)
    {
        this.nom = nom;
        nbr_fichiers = 0;
    }

    public void Afficher()
    {
        Console.WriteLine($"Le repertoire: {nom}\nLes fichiers:");
        for (int i = 0; i < nbr_fichiers; i++)
        {
            Console.WriteLine(fichiers[i].Nom+"."+fichiers[i].Extension);
        }
    }

    public int Rechercher(string nomFichier)
    {
        for (int i = 0; i < nbr_fichiers; i++)
        {
            if (fichiers[i] != null && fichiers[i].Nom == nomFichier)
                return i;
        }

        return -1;
    }

    public void Ajouter (Fichier fichier)
    {
        int test = Rechercher(fichier.Nom);
        if (test != -1)
        {
            Console.WriteLine("Ce fichier est deja existe!");
        }
        else
        {
            if (nbr_fichiers < 30)
            {
                Fichier[] newFichiers = new Fichier[30];
                for (int i = 0; i < nbr_fichiers; i++)
                {
                    newFichiers[i] = fichiers[i];
                }

                newFichiers[nbr_fichiers] = fichier;
                fichiers = newFichiers;
                nbr_fichiers++;
                Console.WriteLine("Le fichier a ete ajoute avec succee!");
            }
            else
            {
                Console.WriteLine("Le repertoire est plein!");
            }
        }
    }

    public int Rechercher()
    {
        Console.WriteLine($"Le repertoire: {nom}\nLes fichiers:");
        for (int i=0; i < nbr_fichiers; i++ )
        {
            if(fichiers[i].Extension=="pdf")
                Console.WriteLine(fichiers[i].Nom+"."+fichiers[i].Extension);
        }

        return 0;
    }

    public void Supprimer(Fichier fichier)
    {
        int i = Rechercher(fichier.Nom);
        if (i > -1)
        {
            Fichier[] newFichiers = new Fichier[30];
            int newIndex = 0;
            for (int j = 0; j < nbr_fichiers; j++)
            {
                if (j != i)
                {
                    newFichiers[newIndex++] = fichiers[j];
                }
            }

            fichiers = newFichiers;
            nbr_fichiers--;
            Console.WriteLine("le fichier "+fichier.Nom+" a ete supprime avec succe!");
        }
        else
        {
            Console.WriteLine("Ce fichier n'existe pas!");
        }
    }

    public void Renommer(Fichier fichier, string newName)
    {
        int i = Rechercher(fichier.Nom);
        if (i > -1)
        {
            fichiers[i].Nom = newName;
        }
    }

    public void Modifier(string nom, float newTaille)
    {
        int i = Rechercher(nom);
        if (i > -1)
        {
            fichiers[i].Taille = newTaille;
        }
    }

    public float getTaille()
    {
        float Taille = 0;
        for (int i = 0; i < nbr_fichiers; i++)
        {
            Taille += fichiers[i].Taille;
        }

        return Taille/1024;
    }
}