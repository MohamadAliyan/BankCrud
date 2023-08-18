using BankCrud.Domain.Entities;
using BankCrud.Service.Interfaces;
using BankCrud.Service.ServiceModels;

namespace BankCrud.Api.Controllers;

public class BranchController: ApiControllerBase<Branch,BranchServiceModel>
{
    private readonly IBranchService _Service;
    public BranchController(IBranchService service) : base(service)
    {
        _Service = service;
    }


}