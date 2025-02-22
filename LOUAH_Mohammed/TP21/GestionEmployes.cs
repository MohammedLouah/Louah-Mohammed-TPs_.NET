namespace TP21;

public class GestionEmployes
{
    private List<Employee> employes;

    public GestionEmployes()
    {
        employes = new List<Employee>();
    }

    public void ajouter(Employee employee)
    {
        if (employee == null)
        {
            return;
        }
        foreach (Employee e in employes)
        {
            if (e.Equals(employee))
            {
                Console.WriteLine("Cet employee existe deja!");
                return;
            }
        }
        employes.Add(employee);
    }

    public void supprimer(Employee employee)
    {
        if (employee == null)
        {
            return;
        }

        if (!employes.Contains(employee))
        {
            Console.WriteLine("Cet employee n'existe pas!");
            return;
        }
        employes.Remove(employee);
    }

    public double SalaireTotal()
    {
        double total=0;
        foreach (Employee emp in employes)
        {
            total += emp.Salaire;
        }
        return total;
    }

    public double SalaireMoyen()
    {
        return SalaireTotal() / employes.Count;
    }

    public List<Employee> Employes
    {
        get => employes;
        set => employes = value;
    }
}