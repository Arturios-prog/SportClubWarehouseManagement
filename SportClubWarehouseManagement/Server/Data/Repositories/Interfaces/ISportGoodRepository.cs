using SportClubWMS.Shared;

namespace SportClubAPI.Data.Repositories.Interfaces
{
    public interface ISportGoodRepository
    {
        SportGood AddSportGood(SportGood SportGood);
        void DeleteSportGood(int id);
        IEnumerable<SportGood> GetAllSportGoods(bool includeCustomers);
        SportGood? GetSportGoodById(int id, bool includeCustomers);
        SportGood? GetSportGoodByName(string name, bool includeCustomers);
        void UpdateQuantitySportGood(int id, uint quantity, string operation);
        SportGood? UpdateSportGood(SportGood sportGood);
        bool ContainsSportGood(SportGood sportGood);
    }
}