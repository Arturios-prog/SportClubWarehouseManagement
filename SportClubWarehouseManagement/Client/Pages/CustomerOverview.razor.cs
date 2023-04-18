using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Radzen;
using Radzen.Blazor;
using SportClubWMS.Components;
using SportClubWMS.Services;
using SportClubWMS.Shared;

namespace SportClubWMS.Pages
{
    public partial class CustomerOverview
    {

        public List<Customer> customers;

        [Inject]
        public ICustomerDataService CustomerDataService { get; set; }
        [Inject]
        public ISportGoodDataService SportGoodDataService { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }
        [Inject]
        public IRefreshService RefreshService { get; set; }

        RadzenDataGrid<Customer> dataGrid { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadCustomersAsync();
            }
			catch (AccessTokenNotAvailableException exception)
			{
				exception.Redirect();
			}
		}

        private async Task LoadCustomersAsync()
        {
            customers = (await CustomerDataService.GetAllCustomers(true)).ToList();
        }
        public async Task OpenCustomerInfo(int id)
        {
            await DialogService.OpenAsync<DialogCustomerPage>($"customer {id}",
               new Dictionary<string, object>() { { "CustomerId", id } },
               new DialogOptions() { Width = "700px", Height = "570px" });
        }

        public async Task OpenCustomerEdit(int id)
        {
            await DialogService.OpenAsync<DialogEditCustomerPage>($"customer {id}",
               new Dictionary<string, object>() {
                   { "CustomerId", id },
                   {"dataGrid", dataGrid } },
               new DialogOptions() { Width = "1000px", Height = "600px" });

        }

        public async Task AddNewCustomer()
        {
            await DialogService.OpenAsync<DialogEditCustomerPage>("Add new Customer",
                new Dictionary<string, object>() { { "dataGrid", dataGrid } },
                new DialogOptions() { Width = "1000px", Height = "600px" });
        }

        protected async Task DeleteCustomer(int id)
        {
            var confirmResult = await DialogService.Confirm("Are you sure you want to delete this cutomer? \n All the goods will be returned to Sport Good's Database", "Warning");
            if (confirmResult.HasValue && confirmResult.Value)
            {
                try
                {
                    var customer = await CustomerDataService.GetCustomerById(id, true);
                    if (customer != null)
                    {
                        //return all the goods to Sport Goods db
                        foreach (var csg in customer.CustomerSportGoods)
                        {
                            await SportGoodDataService.UpdateQuantitySportGood(csg.SportGoodId, csg.Quantity, false);
                        }
                    }

                    await CustomerDataService.DeleteCustomer(id);
                    await LoadCustomersAsync();

                    StateHasChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


        }

        void OnRender(DataGridRenderEventArgs<Customer> args)
        {
            if (args.FirstRender)
            {
                StateHasChanged();
            }
        }

        private void RefreshMe()
        {
            StateHasChanged();
        }

        void OnGroupRowRender(GroupRowRenderEventArgs args)
        {
            /*if (args.FirstRender && args.Group.Data.Key == "Vice President, Sales")
            {
                args.Expanded = false;
            }*/
        }

        void OnGroupRowExpand(Group group)
        {
            Console.WriteLine($"Group row with key: {group.Data.Key} expanded");
        }

        void OnGroupRowCollapse(Group group)
        {
            Console.WriteLine($"Group row with key: {group.Data.Key} collapsed");
        }

        void OnGroup(DataGridColumnGroupEventArgs<Customer> args)
        {
            Console.WriteLine($"DataGrid {(args.GroupDescriptor != null ? "grouped" : "ungrouped")} by {args.Column.GetGroupProperty()}");
        }
    }

}

