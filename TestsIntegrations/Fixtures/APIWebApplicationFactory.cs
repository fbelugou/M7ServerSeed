using Microsoft.AspNetCore.Mvc.Testing;
using API;
using Microsoft.AspNetCore.Hosting;
using System.Configuration;
using Microsoft.Extensions.Configuration;
namespace TestsIntegrations.Fixtures;
public class APIWebApplicationFactory : WebApplicationFactory<Program>
{

    public IConfiguration Configuration { get; set; }


    public APIWebApplicationFactory() : base()
    {
        //Creer un base de donnée
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        //appsettings.integration.json
        builder.ConfigureAppConfiguration( config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Integrations.json")
                .Build();
            config.AddConfiguration(Configuration);

        });



    }

    protected override void Dispose(bool disposing)
    {
        //Clean la base de donnée
        base.Dispose(disposing);
    }
}
