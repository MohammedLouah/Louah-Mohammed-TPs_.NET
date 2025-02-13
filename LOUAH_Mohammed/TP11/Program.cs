using System;
namespace TP11
{
    internal class Program
    {
        static void Main(string[] arg)
        {
            Fichier fichier1 = new Fichier("Cours1", "pdf", 200);
            Fichier fichier2 = new Fichier("TP1", "pdf", 60);
            Fichier fichier3 = new Fichier("TP2", "docx", 90);

            Repertoire rep = new Repertoire("C##");
            rep.Ajouter(fichier1);
            rep.Ajouter(fichier2);
            rep.Ajouter(fichier1);
            rep.Ajouter(fichier3);
            
            rep.Afficher();
            rep.Rechercher();

            Console.WriteLine(rep.getTaille());
            
            rep.Supprimer(fichier2);
            
            rep.Supprimer(fichier2);

            Console.WriteLine(rep.getTaille());
        }
    }
}