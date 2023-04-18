
namespace SportClubWMS.Services
{
    public interface IRefreshService
    {
        event Action RefreshRequested;
        void CallRequestRefresh();
    }
}