namespace BankCrud.Domain.Entities;

public class Branch : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    //public int BankId { get; set; }
}