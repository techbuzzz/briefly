using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Briefly.Client.Services;

namespace Briefly.Client
{
    public class Program
    {
        public static async Task Main(string[]args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient for API calls
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            // Add Fluent UI Components
            builder.Services.AddFluentUIComponents();
            
            // Add application services
            builder.Services.AddScoped<NotesService>();
            builder.Services.AddScoped<NoteTypesService>();

            await builder.Build().RunAsync();
        }
    }
}
