using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using SportClubWMS;
using SportClubWMS.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SportClubAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
	.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project


builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<IRefreshService, RefreshService>();


builder.Services.AddHttpClient<ICustomerDataService, CustomerDataService>
		("CutsomerClient", client => client.BaseAddress = new Uri("https://localhost:7258"))
		.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<ISportGoodDataService, SportGoodDataService>
	("SportClubClient", client => client.BaseAddress = new Uri("https://localhost:7258"))
	.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SportClubAPI"));

builder.Services.AddApiAuthorization();

await builder.Build().RunAsync();
