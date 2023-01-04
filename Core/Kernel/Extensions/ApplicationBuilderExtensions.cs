using MarketplaceSI.Core.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using MarketplaceSI.Core.Domain.Settings;

namespace Kernel.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureRequestPipeline(this IApplicationBuilder app, IWebHostEnvironment env, SecuritySettings securitySettings, CorsSettings corsSettings)
        {
            app.UseMiddleware<ExceptionMiddleware>()
                .UseApplicationSecurity(securitySettings, corsSettings)
                .UseRouting()
                .UseWebSockets();

            return app;
        }
        public static IApplicationBuilder UseApplicationSecurity(this IApplicationBuilder app,
            SecuritySettings securitySettings, CorsSettings corsSettings)
        {
            app
                .UseCors(CorsPolicyBuilder(corsSettings))
                .UseAuthentication()
                .UseAuthorization();

            if (securitySettings.EnforceHttps)
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }
            return app;
        }
        private static Action<CorsPolicyBuilder> CorsPolicyBuilder(CorsSettings config)
        {
            //TODO implement an url based cors policy rather than global or per controller
            return builder =>
            {
                if (!config.AllowedOrigins.Equals("*"))
                {
                    if (config.AllowCredentials)
                    {
                        builder.AllowCredentials();
                    }
                    else
                    {
                        builder.DisallowCredentials();
                    }
                }

                builder.WithOrigins(config.AllowedOrigins)
                    .WithMethods(config.AllowedMethods)
                    .WithHeaders(config.AllowedHeaders)
                    .WithExposedHeaders(config.ExposedHeaders)
                    .SetPreflightMaxAge(TimeSpan.FromSeconds(config.MaxAge));
            };
        }
    }
}
