using Microsoft.AspNetCore.Components;
using Radzen;
using SportClubWMS.Services;
using SportClubWMS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportClubWMS.Components
{
    public partial class DialogSportGoodPage
    {
        [Parameter]
        public int SportGoodId { get; set; }
        public SportGood SportGood { get; set; } = new SportGood();
        [Inject]
        public ICustomerDataService CustomerDataService { get; set; }
        [Inject]
        public ISportGoodDataService SportGoodDataService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SportGood = await SportGoodDataService.GetSportGoodById(SportGoodId, true);
        }

        public async Task OpenCustomerInfo(int id)
        {
            await DialogService.OpenAsync<DialogCustomerPage>($"customer {id}",
               new Dictionary<string, object>() { { "CustomerId", id } },
               new DialogOptions() { Width = "700px", Height = "570px" });
            
        }
    }
}
