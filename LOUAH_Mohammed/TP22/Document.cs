namespace TP22;

public class Document
{
    private int num;
    private string titre;
    private static int Nbr_Doc = 0;

    public Document(string titre)
    {
        num = ++Nbr_Doc;
        this.titre = titre;
    }
    
    public string Titre
    {
        get => titre;
        set => titre = value;
    }

    public int Num
    {
        get => num;
        set => num = value;
    }
    
    public virtual string Description()
    {
        return $"Numero: {num}, Titre: {titre}, ";
    }
}