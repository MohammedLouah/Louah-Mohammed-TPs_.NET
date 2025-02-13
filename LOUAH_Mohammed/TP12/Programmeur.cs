namespace TP12;

public class Programmeur
{
    private int id;
    private string nom;
    private string prenom;
    private int bureau;
    private Consommation[] consommations;
    private static int IdCount = 1;


    public Programmeur(int id, string nom, string prenom, int bureau)
    {
        this.id = IdCount++;
        this.nom = nom;
        this.prenom = prenom;
        this.bureau = bureau;
    }
    
    public Consommation[] Consommations
    {
        get => consommations;
        set => consommations = value;
    }

    public override string ToString()
    {
        return $"Programmeur: {nom} {prenom}, bureau: {bureau}";
    }

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Nom
    {
        get => nom;
        set => nom = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Prenom
    {
        get => prenom;
        set => prenom = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int Bureau
    {
        get => bureau;
        set => bureau = value;
    }
}