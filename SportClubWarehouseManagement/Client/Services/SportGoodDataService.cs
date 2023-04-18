using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SportClubWMS.Services
{
    public class SportGoodDataService : ISportGoodDataService
    {
        private readonly HttpClient _httpClient;
        public SportGoodDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<SportGood>?> GetAllSportGoods(bool includeCustomers)
        {
            if (!includeCustomers)
                return await JsonSerializer.DeserializeAsync<IEnumerable<SportGood>>
                    (await _httpClient.GetStreamAsync($"api/sportgood"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<IEnumerable<SportGood>>
                    (await _httpClient.GetStreamAsync($"api/sportgood/includecustomers"),
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<SportGood?> GetSportGoodById(int sportGoodId, bool includeCustomers)
        {
            if (!includeCustomers)
                return await JsonSerializer.DeserializeAsync<SportGood>
                    (await _httpClient.GetStreamAsync($"api/sportgood/{sportGoodId}"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<SportGood>
                    (await _httpClient.GetStreamAsync($"api/sportgood/{sportGoodId}/includecustomers"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        public async Task<SportGood?> GetSportGoodByName(string sportGoodName, bool includeCustomers)
        {
            if (!includeCustomers)
                return await JsonSerializer.DeserializeAsync<SportGood>
                    (await _httpClient.GetStreamAsync($"api/sportgood/name({sportGoodName.Trim()})"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            else
                return await JsonSerializer.DeserializeAsync<SportGood>
                    (await _httpClient.GetStreamAsync($"api/sportgood/name({sportGoodName.Trim(' ')})/includecustomers"),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<SportGood?> AddSportGood(SportGood sportGood)
        {
            var sportGoodJson =
                new StringContent(JsonSerializer.Serialize(sportGood), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/sportgood", sportGoodJson);

            if (response.IsSuccessStatusCode)
                return await JsonSerializer.DeserializeAsync<SportGood>(await response.Content.ReadAsStreamAsync());

            return null;
        }

        public async Task DeleteSportGood(int sportGoodId)
        {
            await _httpClient.DeleteAsync($"api/sportgood/{sportGoodId}");
        }

        public async Task UpdateQuantitySportGood(int sportGoodId, uint quantity, bool isRemove)
        {
            await _httpClient.PutAsync($"api/sportgood/{sportGoodId}_quantity={quantity}_{isRemove}", null);
        }

        public async Task UpdateSportGood(SportGood sportGood)
        {
            var sportGoodJson =
                new StringContent(JsonSerializer.Serialize(sportGood), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync("api/sportgood", sportGoodJson);
        }
    }
}
