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
    public class CustomerSportGoodRepository : ICustomerSportGoodRepository
    {
        AppDbContext _appDbContext;
        public CustomerSportGoodRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CustomerSportGood> GetAllCustomerSportGoods()
        {
            return _appDbContext.CustomerSportGoods;
        }

        public IEnumerable<CustomerSportGood> GetAllCustomerSportGoodsById(int customerId)
        {
            return _appDbContext.CustomerSportGoods.Where(csg => csg.CustomerId == customerId);
        }

        public CustomerSportGood? GetCustomerSportGoodById(int customerid, int sportGoodid)
        {
            return _appDbContext.CustomerSportGoods
                .Where(csg => csg.CustomerId == customerid && csg.SportGoodId == sportGoodid)
                .FirstOrDefault();
        }

        public CustomerSportGood? AddCustomerSportGood(CustomerSportGood csg)
        {
            var addedEntity = _appDbContext.CustomerSportGoods.Add(csg);
            _appDbContext.SaveChanges();
            return addedEntity.Entity;
        }

        public void DeleteCustomerSportGood(int customerId, int sportGoodId)
        {
            var foundCsg = _appDbContext.CustomerSportGoods
                .Where(f => f.CustomerId == customerId && f.SportGoodId == sportGoodId)
                .FirstOrDefault();
            if (foundCsg == null) return;
            _appDbContext.CustomerSportGoods.Remove(foundCsg);
            _appDbContext.SaveChanges();
        }

        public CustomerSportGood? UpdateCustomerSportGood(CustomerSportGood customerSportGood)
        {
            var foundCustomerSportGood = _appDbContext.CustomerSportGoods.Where(csg => csg.CustomerId == customerSportGood.Id &&
            csg.SportGoodId == customerSportGood.SportGoodId).FirstOrDefault();

            if (foundCustomerSportGood != null)
            {
                foundCustomerSportGood.SportGoodId = customerSportGood.SportGoodId;
                foundCustomerSportGood.SportGoodName = customerSportGood.SportGoodName;
                foundCustomerSportGood.Quantity = customerSportGood.Quantity;

                _appDbContext.SaveChanges();
                return foundCustomerSportGood;
            }
            return null;
        }
        public void UpdateCustomerSportGoodQuantity(int sportGoodId, uint quantity, string operation)
        {
            var foundCsg = _appDbContext.CustomerSportGoods
                .Where(f => f.SportGoodId == sportGoodId).FirstOrDefault();
            if (foundCsg != null)
            {
                switch (operation)
                {
                    case Operands.SUM:
                        foundCsg.Quantity += quantity;
                        break;
                    case Operands.SUB:
                        foundCsg.Quantity -= quantity;
                        break;

                    default:
                        throw new ArgumentException("The given argument doesn't match any operands");
                }
                _appDbContext.SaveChanges();
            }
        }
    }
}
