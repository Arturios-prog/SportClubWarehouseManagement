using Microsoft.EntityFrameworkCore;
using SportClubAPI.Data.Repositories.Interfaces;
using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubAPI.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;
        private List<SportGood> SportGoods = new List<SportGood>();
        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void SaveChanges()
        {
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAllCustomers(bool includeGoods)
        {
            if (includeGoods)
                return _appDbContext.Customers.Include(c => c.SportGoods)
                    .Include(c => c.CustomerSportGoods).OrderBy(c => c.FirstName);
            return _appDbContext.Customers;
        }
        public Customer? GetCustomerById(int id, bool includeGoods)
        {
            if (includeGoods)
            {
                return _appDbContext.Customers.Include(s => s.SportGoods)
                    .Include(csg => csg.CustomerSportGoods)
                    .Where(c => c.CustomerId == id).FirstOrDefault();
            }
            return _appDbContext.Customers.Where(c => c.CustomerId == id)
                .FirstOrDefault();
        }

        public Customer? GetCustomerByName(string name, bool includeGoods)
        {
            if (includeGoods)
            {
                return _appDbContext.Customers.Include(s => s.SportGoods)
                    .Include(csg => csg.CustomerSportGoods)
                    .Where(c => c.FirstName == name).FirstOrDefault();
            }
            return _appDbContext.Customers.Where(c => c.FirstName == name)
                .FirstOrDefault();
        }

        public int? GetCustomerGoodsCount(int customerId)
        {
            return _appDbContext.Customers.Where(c => c.CustomerId == customerId)
                .FirstOrDefault().CustomerSportGoods.Count;
        }

        public Customer AddCustomer(Customer customer)
        {
            SportGoods.AddRange(customer.SportGoods);
            customer.SportGoods.Clear();
            var addedCustomer = _appDbContext.Customers.Add(customer);

            _appDbContext.SaveChanges();
            foreach (var sportGood in SportGoods)
            {
                customer.SportGoods.Add(sportGood);
            }
            _appDbContext.SaveChanges();
            return addedCustomer.Entity;
        }

        /*public Customer AddCustomer(Customer customer)
        {
            var addedEntity = _appDbContext.Customers.Add(customer);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }*/

        public Customer? UpdateCustomer(Customer customer)
        {
            var foundCustomer = _appDbContext.Customers
                .Where(fc => fc.CustomerId == customer.CustomerId).FirstOrDefault();

            if (foundCustomer != null)
            {
                foundCustomer.FirstName = customer.FirstName;
                foundCustomer.SecondName = customer.SecondName;
                foundCustomer.Age = customer.Age;
                foundCustomer.Gender = customer.Gender;
                foundCustomer.CustomerSportGoods = customer.CustomerSportGoods;
                foundCustomer.Address = customer.Address;
                foundCustomer.Email = customer.Email;
                foundCustomer.SubscribeStatus = customer.SubscribeStatus;
                foundCustomer.RegistrationDate = customer.RegistrationDate;

                SportGoods.AddRange(customer.SportGoods);
                foundCustomer.SportGoods = new List<SportGood>();
                customer.SportGoods = new List<SportGood>();

                _appDbContext.SaveChanges();

                _appDbContext.Entry(foundCustomer).State = EntityState.Detached;

                if (SportGoods != null)
                {
                    foreach (var sportGood in SportGoods)
                    {
                        _appDbContext.SportGoods.Where(sg => sg.SportGoodId == sportGood.SportGoodId).FirstOrDefault().Customers.Add(customer);
                    }
                    _appDbContext.SaveChanges();
                }
                return foundCustomer;
            }

            return null;
        }

        public void DeleteCustomer(int id)
        {
            var foundCustomer = _appDbContext.Customers.FirstOrDefault(c => c.CustomerId == id);

            if (foundCustomer == null) return;

            _appDbContext.Customers.Remove(foundCustomer);
            _appDbContext.SaveChanges();
        }
    }
}
