using System;
using System.Runtime.InteropServices.JavaScript;

namespace TP21
{
    internal class Program
    {
        static void Main(string[] arg)
        {
            GestionEmployes gestionEmployes = new GestionEmployes();
            Employee employee1 = new Employee("Amine Jebari", 9500, "Backend Developer", new DateOnly(2024, 02, 01));
            Employee employee2 = new Employee("Tarek Ahmadi", 8500, "Front Developer", new DateOnly(2024, 02, 01));
            Employee employee3 = new Employee("Soukaina Idrissi", 9000, "DevOps Engineer", new DateOnly(2025, 02, 01));

            gestionEmployes.ajouter(employee1);
            gestionEmployes.ajouter(employee2);
            gestionEmployes.ajouter(null);
            //gestionEmployes.ajouter(employee1);
            gestionEmployes.ajouter(employee3);
            /*Console.WriteLine("Liste des empoyes:");
            foreach (Employee emp in gestionEmployes.Employes)
            {
                Console.WriteLine($"{emp}");
            }
            Console.WriteLine($"Salaire total de l'entreprise: {gestionEmployes.SalaireTotal()}");
            Console.WriteLine($"Salaire moyen de chaque employee: {gestionEmployes.SalaireMoyen()}");

            gestionEmployes.supprimer(employee2);
            gestionEmployes.supprimer(employee2);
            Console.WriteLine("Apres suppeimer le 2eme employee:");
            Console.WriteLine("Liste des empoyes:");
            foreach (Employee emp in gestionEmployes.Employes)
            {
                Console.WriteLine($"{emp}");
            }*/

            Directeur directeur = Directeur.getInstance(gestionEmployes);
            Console.WriteLine("Liste des empoyes avant les modifications:");
            foreach (Employee emp in directeur.setGestionEmployes.Employes)
            {
                Console.WriteLine($"{emp}");
            }
            Console.WriteLine($"Le salaire total de l'entreprise: {directeur.SalaireTotal()}");
            Console.WriteLine($"Le salaire moyen de chaque employee: {directeur.SalaireMoyen()}");
            directeur.setGestionEmployes.ajouter(employee1);
            directeur.setGestionEmployes.supprimer(employee2);
            directeur.setGestionEmployes.Employes[1].Salaire = 12000;
            directeur.setGestionEmployes.Employes[0].Salaire = 12500;
            Console.WriteLine("Liste des empoyes apres les modifications:");
            foreach (Employee emp in directeur.setGestionEmployes.Employes)
            {
                Console.WriteLine($"{emp}");
            }
        }
    }
}

