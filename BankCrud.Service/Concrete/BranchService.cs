using BankCrud.Domain.Entities;
using BankCrud.Repository;
using BankCrud.Service.Interfaces;
using BankCrud.Service.ServiceModels;

namespace BankCrud.Service.Concrete;

public class BranchService : BaseService<Branch, BranchServiceModel>, IBranchService
{
    private readonly IBranchRepository _BranchRepository;


    public BranchService(IBranchRepository BranchRepository,ApplicationDbContext context) : base(BranchRepository,context)
    {
        _BranchRepository = BranchRepository;

    }



}