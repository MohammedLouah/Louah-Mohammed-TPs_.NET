namespace TP22;

public class Dictionnaire : Document
{
    private string langue;
    private int nbr_def;

    public Dictionnaire(string titre, string langue, int nbrDef) : base(titre)
    {
        this.langue = langue;
        nbr_def = nbrDef;
    }

    public string Langue
    {
        get => langue;
        set => langue = value;
    }

    public int NbrDef
    {
        get => nbr_def;
        set => nbr_def = value;
    }

    public override string Description()
    {
        return base.Description() + $"Langue: {langue}, Nbre de definitions: {nbr_def}";
    }
}