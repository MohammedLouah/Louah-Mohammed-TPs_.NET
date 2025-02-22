using System;

namespace TP22
{
    internal class Program()
    {
        static void Main(string[] arg)
        {
            Livre l1 = new Livre("Algebre lineaire", "Adil Amine", 250);
            Dictionnaire d1 = new Dictionnaire("Oxford", "Anglais", 2000);
            Biblio biblio = new Biblio();
            biblio.Ajouter(l1);
            biblio.Ajouter(l1);
            biblio.Ajouter(d1);
            Console.WriteLine($"Le Nbre de livres: {biblio.Nbr_livres()}");
            biblio.Afficher_Dictionnaires();
            biblio.tousLesAuteurs();
            biblio.tousLesDescriptions();
        }
    }
}

