using SandboxCustomers.Api.Models;
using System.Linq;

namespace SandboxCustomers.Api.Services
{
    public interface ICustomerRepository
    {
        Customer Get(int id);
        IQueryable<Customer> GetAll();
        void Add(Customer dataModel);
        void Delete(int id);
        Customer Update(Customer dataModel);
        int Count();
        bool Save();
    }
}
