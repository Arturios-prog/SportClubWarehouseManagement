using SportClubWMS.Shared;

namespace SportClubAPI.Data.Repositories.Interfaces
{
    public interface ICustomerSportGoodRepository
    {
        CustomerSportGood? AddCustomerSportGood(CustomerSportGood csg);
        void DeleteCustomerSportGood(int customerId, int sportGoodId);
        IEnumerable<CustomerSportGood> GetAllCustomerSportGoods();
        IEnumerable<CustomerSportGood> GetAllCustomerSportGoodsById(int customerId);
        CustomerSportGood? GetCustomerSportGoodById(int customerid, int sportGoodid);
        CustomerSportGood? UpdateCustomerSportGood(CustomerSportGood customerSportGood);
        void UpdateCustomerSportGoodQuantity(int sportGoodId, uint quantity, string operation);
    }
}