using TP15.Entities.Enums;

namespace TP15.Entities;

public class Operation
{
    public int Id { get; set; }
    public TypeOperation Type { get; set; }
    public decimal Montant { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public string NumeroCompteSource { get; set; }
    public string NumeroCompteDestination { get; set; }  // Pour les transferts

    public Operation()
    {
        Date = DateTime.Now;
    }
}