using BankCrud.Domain.Entities;

namespace BankCrud.Service.ServiceModels;

public class BranchServiceModel : BaseServiceModel
{


    public string Name { get; set; }
    public string Code { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    public long  BankId { get; set; }



}
public class BankServiceModel : BaseServiceModel
{

    public string Name { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    public string Logo { get; set; }
    public  List<Branch> Branches { get; set; }



}
public class CreateServiceModel :BaseServiceModel
{

    public string Name { get; set; }
    public string Tel { get; set; }
    public string Address { get; set; }
    public string Logo { get; set; }
 


}