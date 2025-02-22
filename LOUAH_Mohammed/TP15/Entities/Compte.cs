namespace TP15.Entities;

public class Compte
{
    public string Numero { get; set; }
    public int IdProprietaire { get; set; }
    public decimal Solde { get; private set; }
    public DateTime DateCreation { get; set; }
    public List<Operation> Operations { get; set; }
    public bool EstActif { get; set; }

    public Compte()
    {
        DateCreation = DateTime.Now;
        Operations = new List<Operation>();
        EstActif = true;
        Solde = 0;
    }

    public void Crediter(decimal montant)
    {
        if (montant <= 0)
            throw new ArgumentException("Le montant doit être positif");

        Solde += montant;
    }

    public void Debiter(decimal montant)
    {
        if (montant <= 0)
            throw new ArgumentException("Le montant doit être positif");

        if (Solde < montant)
            throw new InvalidOperationException("Solde insuffisant");

        Solde -= montant;
    }
}