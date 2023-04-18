using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubWMS.Services
{
    public class RefreshService : IRefreshService
    {
        public event Action RefreshRequested;
        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
