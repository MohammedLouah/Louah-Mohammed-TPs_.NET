namespace TP22;

public class Biblio
{
    private List<Document> documents;

    public Biblio()
    {
        documents = new List<Document>();
    }

    public void Ajouter(Document document)
    {
        if (document == null)
            return;
        foreach (Document doc in documents)
        {
            if (doc.Equals(document))
            {
                Console.WriteLine("Cet document existe deja!");
                return;
            }
        }
        documents.Add(document);
    }

    public int Nbr_livres()
    {
        int nbr = 0;
        foreach (Document doc in documents)
        {
            if (doc is Livre)
                nbr++;
        }
        return nbr;
    }

    public void Afficher_Dictionnaires()
    {
        Console.WriteLine("Liste des dictionnaires:");
        foreach (Document doc in documents)
        {
            if (doc is Dictionnaire)
            {
                Dictionnaire dict = (Dictionnaire)doc;
                Console.WriteLine($"Titre: {doc.Titre}, Langue: {dict.Langue}, Nbre de Definitions: {dict.NbrDef}");
            }
        }
    }

    public void tousLesAuteurs()
    {
        Console.WriteLine("Num de document \t Type \t\t Auteur");
        foreach (Document doc in documents)
        {
            Console.Write($"{doc.Num} \t\t\t ");
            if (doc is Dictionnaire)
            {
                Dictionnaire dict = (Dictionnaire) doc;
                Console.WriteLine($"dictionnaire \t  -");
            }
            else if (doc is Livre)
            {
                Livre livre = (Livre) doc;
                Console.WriteLine($"livre \t\t {livre.Auteur}");
            }
        }
    }

    public void tousLesDescriptions()
    {
        foreach (Document doc in documents)
        {
            Console.WriteLine(doc.Description());
        }
    }

    public List<Document> Documents
    {
        get => documents;
        set => documents = value;
    }
}