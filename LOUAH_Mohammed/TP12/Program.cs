using System;

namespace TP12
{
    internal class Program
    {
        static void Main(string[] arg)
        {
            Programmeur programmeur1 = new Programmeur(351, "Benmguirida", "Anas", 23);
            Programmeur programmeur2 = new Programmeur(352, "Louah", "Mohammed", 25);
            Programmeur programmeur3 = new Programmeur(353, "Al Azami", "Tarek", 21);

            Projet projet = new Projet("PRJ435", "Developpement d'un assistant AI", 15);
            projet.AjouterProgrammeur(programmeur1);
            projet.AjouterProgrammeur(programmeur2);
            projet.AjouterProgrammeur(programmeur1);
            projet.AjouterProgrammeur(programmeur3);

            Consommation consommation1 = new Consommation(8);
            Consommation consommation2 = new Consommation(9);
            Consommation consommation3 = new Consommation(7);
            
            projet.AjouterConsommation(programmeur1, consommation1, 1);
            projet.AjouterConsommation(programmeur2, consommation2, 1);
            projet.AjouterConsommation(programmeur3, consommation3, 1);
            
            projet.AjouterConsommation(programmeur1, consommation1, 2);
            projet.AjouterConsommation(programmeur2, consommation2, 2);
            projet.AjouterConsommation(programmeur3, consommation3, 2);
            
            projet.AjouterConsommation(programmeur1, consommation1, 3);
            projet.AjouterConsommation(programmeur2, consommation2, 3);
            projet.AjouterConsommation(programmeur3, consommation3, 3);
            
            projet.AfficherListe();
            projet.AfficherTotalCafeParSemaine(1);
        }
    }
}