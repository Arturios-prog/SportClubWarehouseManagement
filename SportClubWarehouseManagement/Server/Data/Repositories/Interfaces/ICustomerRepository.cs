using SportClubWMS.Shared;

namespace SportClubAPI.Data.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer AddCustomer(Customer customer);
        /*Customer AddCustomer(Customer customer);*/
        void DeleteCustomer(int id);
        void SaveChanges();
        IEnumerable<Customer> GetAllCustomers(bool includeGoods);
        Customer? GetCustomerById(int id, bool includeGoods);
        Customer? GetCustomerByName(string name, bool includeGoods);
        int? GetCustomerGoodsCount(int customerId);
        Customer? UpdateCustomer(Customer customer);
    }
}