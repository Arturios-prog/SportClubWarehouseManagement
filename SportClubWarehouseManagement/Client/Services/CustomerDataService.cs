using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportClubWMS.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly HttpClient _httpClient;

        public CustomerDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Customer>?> GetAllCustomers(bool includeGoods)
        {
            if (!includeGoods)
                return await JsonSerializer.DeserializeAsync<IEnumerable<Customer>>
                    (await _httpClient.GetStreamAsync($"api/customer"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<IEnumerable<Customer>>
                    (await _httpClient.GetStreamAsync($"api/customer/includegoods"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Customer?> GetCustomerById(int customerId, bool includeGoods)
        {
            if (!includeGoods)
                return await JsonSerializer.DeserializeAsync<Customer>
                    (await _httpClient.GetStreamAsync($"api/customer/{customerId}"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<Customer>
                    (await _httpClient.GetStreamAsync($"api/customer/{customerId}/includegoods"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<IEnumerable<CustomerSportGood>?> GetAllCustomerSportGoodsById(int customerId)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<CustomerSportGood>>
                (await _httpClient.GetStreamAsync($"api/customer/{customerId}/onlygoods"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<uint> GetCustomerSportGoodsCount(int customerId)
        {
            return await JsonSerializer.DeserializeAsync<uint>(
                await _httpClient.GetStreamAsync($"api/customer/{customerId}/count"),
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Customer?> AddCustomer(Customer customer)
        {
            var customerJson =
                new StringContent(JsonSerializer.Serialize(customer), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/customer", customerJson);

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<Customer>(await response.Content.ReadAsStreamAsync());

            return null;
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var customerJson =
                new StringContent(JsonSerializer.Serialize(customer), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("api/customer", customerJson);
        }

        public async Task AddSportGoodToCustomer(int customerId, int sportGoodId, uint quantity)
        {
            await _httpClient.PutAsync($"api/customer/customer({customerId})_good({sportGoodId})_add({quantity})", null);
        }

        public async Task RemoveSportGoodFromCustomer(int customerId, int sportGoodId, uint quantity)
        {
            await _httpClient.PutAsync($"api/customer/customer({customerId})_good({sportGoodId})_remove({quantity})", null);
        }

        public async Task DeleteCustomerSportGood(int customerId, int sportGoodId)
		{
            await _httpClient.DeleteAsync($"api/customer/{customerId}/{sportGoodId}");
		}

        public async Task DeleteCustomer(int customerId)
        {
            await _httpClient.DeleteAsync($"api/customer/{customerId}");
        }

    }
}
