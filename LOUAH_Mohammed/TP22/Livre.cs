namespace TP22;

public class Livre : Document
{
    private string auteur;
    private int nbr_pages;

    public Livre(string titre, string auteur, int nbrPages) : base(titre)
    {
        this.auteur = auteur;
        nbr_pages = nbrPages;
    }

    public string Auteur
    {
        get => auteur;
        set => auteur = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int NbrPages
    {
        get => nbr_pages;
        set => nbr_pages = value;
    }
    
    public override string Description()
    {
        return base.Description() + $"Auteur: {auteur}, Nbre de pages: {nbr_pages}";
    }
}