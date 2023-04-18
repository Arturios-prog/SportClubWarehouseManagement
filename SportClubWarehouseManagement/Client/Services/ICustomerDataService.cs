using SportClubWMS.Shared;

namespace SportClubWMS.Services
{
    public interface ICustomerDataService
    {
        Task<Customer?> AddCustomer(Customer customer);
        Task AddSportGoodToCustomer(int customerId, int sportGoodId, uint quantity);
        Task DeleteCustomer(int customerId);
        Task<IEnumerable<Customer>?> GetAllCustomers(bool includeGoods);
        Task<IEnumerable<CustomerSportGood>?> GetAllCustomerSportGoodsById(int customerId);
        Task<Customer?> GetCustomerById(int customerId, bool includeGoods);
        Task<uint> GetCustomerSportGoodsCount(int customerId);
        Task DeleteCustomerSportGood(int customerId, int sportGoodId);
        Task RemoveSportGoodFromCustomer(int customerId, int sportGoodId, uint quantity);
        Task UpdateCustomer(Customer customer);
    }
}