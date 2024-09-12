using BLL.Services.Implementations;
using BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BLL;

public class BLLOptions
{    
    //Here you can add your custom options
}

public static class BLLExtension
{
    public static IServiceCollection AddBLL(this IServiceCollection services, Action<BLLOptions>? configure = null)
    {
        BLLOptions options = new(); 
        configure?.Invoke(options); // Invoke the configuration method if not null 


        //Here you can add your BLL services
        services.AddTransient<IBookStoreService, BookStoreService>();

        return services;
    }
}
