namespace TP11;

public class Fichier
{
    private string nom;
    private string extension;
    private float taille;

    public Fichier(string nom, string extension, float taille)
    {
        this.nom = nom;
        this.extension = extension;
        this.taille = taille;
    }

    public string Nom
    {
        get { return nom; }
        set { nom = value; }
    }
    
    public string Extension
    {
        get { return extension; }
        set { extension = value; }
    }
    
    public float Taille
    {
        get { return taille; }
        set { taille = value; }
    }
    
}