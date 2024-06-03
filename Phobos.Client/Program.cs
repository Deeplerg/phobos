using System.Text.Json;
using ExpressionCalculator;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Phobos.Client;
using Phobos.Core.Drawing;
using Phobos.Core.Drawing.Components;
using Phobos.Core.Drawing.Configuration;
using Phobos.Core.Drawing.Pipeline;
using Phobos.Core.UnknownVariable;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var startupClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
await DownloadFile(startupClient, "config.json");
await DownloadFile(startupClient, "arial.ttf");

builder.Configuration.AddJsonFile("config.json");
builder.Services.Configure<DrawingConfigurationOptions>(
    builder.Configuration.GetSection(DrawingConfigurationOptions.Position));

builder.Services.AddExpressionCalculator();
builder.Services.AddDrawingServices();

builder.Services.AddScoped<JsInteropService>();

await builder.Build().RunAsync();

async Task DownloadFile(HttpClient httpClient, string filename)
{
    var response = await httpClient.GetAsync(filename);
    using (var fs = File.Create(filename))
    {
        await response.Content.CopyToAsync(fs);
    }
}