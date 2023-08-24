using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace SubtleBlazor
{
    public static class RegisterSubtleBlazor
    {
        public static IServiceCollection AddSubtleBlazor(this IServiceCollection services)
        {
            services.AddScoped<SubtleBlazorService>();
            return services;
        }
    }
}