namespace TP21;

public class Directeur
{
    private static Directeur instance;
    private GestionEmployes gestionEmployes;

    private Directeur(GestionEmployes gestionEmployes)
    {
        this.gestionEmployes = gestionEmployes;
    }

    public static Directeur getInstance(GestionEmployes gestionEmployes)
    {
        if (instance == null)
        {
            instance = new Directeur(gestionEmployes);
        }

        return instance;
    }

    public double SalaireTotal()
    {
        return gestionEmployes.SalaireTotal();
    }
    
    public double SalaireMoyen()
    {
        return gestionEmployes.SalaireMoyen();
    }

    public GestionEmployes setGestionEmployes
    {
        get => gestionEmployes;
        set => gestionEmployes = value;
    }
}