using SandboxCustomers.Api.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SandboxCustomers.Api.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ConcurrentDictionary<int, Customer> _storage = new ConcurrentDictionary<int, Customer>();
 
        public Customer Get(int id)
        {
            Customer dataModel;
            return _storage.TryGetValue(id, out dataModel) ? dataModel : null;
        }
 
        public IQueryable<Customer> GetAll()
        {
            IQueryable<Customer> _allItems = _storage.Values.AsQueryable();
            return _allItems;
        }

        public void Add(Customer dataModel)
        {
            dataModel.Id = !_storage.Values.Any() ? 1 : _storage.Values.Max(x => x.Id) + 1;
 
            if (!_storage.TryAdd(dataModel.Id, dataModel))
            {
                throw new Exception("Item could not be added");
            }
        }
 
        public void Delete(int id)
        {
            Customer dataModel;
            if (!_storage.TryRemove(id, out dataModel))
            {
                throw new Exception("Item could not be removed");
            }
        }
 
        public Customer Update(Customer dataModel)
        {
            _storage.TryUpdate(dataModel.Id, dataModel, Get(dataModel.Id));
            return dataModel;
        }
 
        public int Count()
        {
            return _storage.Count;
        }
 
        public bool Save()
        {
            // To keep interface consistent with Controllers, Tests & EF Interfaces
            return true;
        }

    }
}
