using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Radzen;
using SportClubWMS.Services;
using SportClubWMS.Shared;

namespace SportClubWMS.Components
{
    public partial class DialogCustomerPage
    {
        [Parameter]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = new Customer();
        public SportGood SportGood { get; set; } = new SportGood();
        [Inject]
        public ICustomerDataService CustomerDataService { get; set; }
        [Inject]
        public DialogService DialogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Customer = await CustomerDataService.GetCustomerById(CustomerId, true);
        }

        public async Task OpenSportGoodInfo(int id)
        {
            await DialogService.OpenAsync<DialogSportGoodPage>($"Sport Good {id}",
               new Dictionary<string, object>() { { "SportGoodId", id } },
               new DialogOptions() { Width = "700px", Height = "570px" });
        }

    }
}
