namespace BankCrud.Domain.Entities;

public class Bank : BaseEntity
{
    public string Name { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    public string Logo { get; set; }
    public virtual List<Branch> Branches { get; set; }

}