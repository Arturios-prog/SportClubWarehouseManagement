using SportClubWMS.Shared;

namespace SportClubWMS.Services
{
    public interface ISportGoodDataService
    {
        Task DeleteSportGood(int sportGoodId);
        Task<IEnumerable<SportGood>?> GetAllSportGoods(bool includeCustomers);
        Task<SportGood?> GetSportGoodById(int sportGoodId, bool includeCustomers);
        Task<SportGood?> GetSportGoodByName(string sportGoodName, bool includeCustomers);
        Task<SportGood?> AddSportGood(SportGood sportGood);
        Task UpdateSportGood(SportGood sportGood);
        Task UpdateQuantitySportGood(int sportGoodId, uint quantity, bool isRemove);
    }
}