using AutoAllocator.Logic.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AutoAllocator.Web;
using AutoAllocator.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddSingleton<FileService>()
    .AddSingleton<IParser, CsvParserFromString>()
    .AddSingleton<IOutputGenerator, CsvOutputGenerator>()
    .AddSingleton<IAllocator, SimpleAllocatorWithUtilisationPenalty>()
    ;
var app = builder.Build();

await builder.Build().RunAsync();