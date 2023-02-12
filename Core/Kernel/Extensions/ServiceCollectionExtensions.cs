using Infrastructure.Data;
using MarketplaceSI.Core.Domain.Settings;
using MarketplaceSI.Core.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scrutor;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using MarketplaceSI.Core.Infrastructure.Repositories;
using MarketplaceSI.Core.Domain.Security.Interfaces;
using MarketplaceSI.Core.Infrastructure.Security;
using MarketplaceSI.Core.Infrastructure.Services;
using HotChocolate.Execution.Configuration;
using FluentValidation;
using Domain.Repositories.DataLoaders;
using MarketplaceSI.Core.Infrastructure.Repositories.DataLoaders;
using Domain.Settings;

namespace Kernel.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public const string UserNameClaimType = JwtRegisteredClaimNames.Email;
        public const string UserIdClaimType = JwtRegisteredClaimNames.Sub;

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IHostEnvironment env) => services
                .AddMediatR(typeof(ServiceCollectionExtensions))
                .AddAutoMapper(typeof(ServiceCollectionExtensions))
                .AddCors()
                .AddAppSettings(configuration)
                .AddHttpContextAccessor()
                .AddDatabase(configuration)
                .AddSecurity()
                .AddRepositories()
                .AddServices()
                .AddScoped<ExceptionMiddleware>()
                .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddValidatorsFromAssembly(Assembly.GetEntryAssembly());

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration configuration) => services
            .Configure<SecuritySettings>(configuration.GetSection(nameof(SecuritySettings)))
            .Configure<ExceptionSettings>(configuration.GetSection(nameof(ExceptionSettings)))
            .Configure<CorsSettings>(configuration.GetSection(nameof(CorsSettings)))
            .Configure<HashingSettings>(configuration.GetSection(nameof(HashingSettings)))
            .Configure<UrlsSettings>(configuration.GetSection(nameof(UrlsSettings)))
            .Configure<SendGridSettings>(configuration.GetSection(nameof(SendGridSettings)))
            .Configure<AmazonSettings>(configuration.GetSection(nameof(AmazonSettings)));

        public static IServiceCollection AddAutomapper(this IServiceCollection services) => services.AddAutoMapper(typeof(ServiceCollectionExtensions));
        //TODO: Different type of databses
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //Http context must be added befor 
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            var connection = configuration.GetConnectionString("Default");

            services.AddPooledDbContextFactory<AppDbContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(connection, b => b.MigrationsAssembly("Migrations")));


            services.AddScoped<AppDbContext>(p => p.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());

            return services;
        }
        public static IRequestExecutorBuilder AddDataLoaders(this IRequestExecutorBuilder builder)
        {
            builder
            .AddDataLoader<IUserByIdDataLoader, UserByIdDataLoader>();
            //.AddDataLoader<IProductByIdDataLoader, ProductByIdDataLoader>()
            //.AddDataLoader<ICategoryByIdDataLoader, CategoryByIdDataLoader>();

            return builder;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IUnitOfWork), typeof(UnitOfWork))
                    // Register repository interfaces using the I prefix convention for interfaces to match interface/class
                    .AddClasses(classes => classes.InNamespaces("MarketplaceSI.Core.Infrastructure.Repositories"))
                        .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                        .AsMatchingInterface()
                        .WithScopedLifetime()

                    // Now find repositories that has class name ending with "ExtendedRepository" and register interfaces it implements with priority.
                    // For example: if JobExtendedRepository class is present and implements IJobRepository interface, take precedence over
                    // existing registrations.
                    .AddClasses(classes => classes.Where(type => type.Namespace != null && type.Namespace.Equals("MarketplaceSI.Core.Infrastructure.Repositories") &&
                                                                 type.Name.EndsWith("ExtendedRepository")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
            );

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IUserService), typeof(UserService))
                    // Find services and register its matching interfaces/implementations.
                    // For example: JobService matches IJobService, EmployeeService matches IEmployeeService, etc...
                    .AddClasses(classes => classes.InNamespaces("MarketplaceSI.Core.Infrastructure.Services"))
                        .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                        .AsMatchingInterface()
                        .WithScopedLifetime()

                    // Now find services with class name ending with 'ExtendedService' and register it to interfaces
                    // it implements.
                    // For example: if JobExtendedService class is present and implements IJobService, then register
                    // it as the implementation for IJobService, replacing the generated service class (JobService).
                    .AddClasses(classes => classes.Where(type => type.Namespace != null && type.Namespace.Equals("MarketplaceSI.Core.Infrastructure.Services") &&
                                                                type.Name.EndsWith("ExtendedService")))
                        .UsingRegistrationStrategy(RegistrationStrategy.Replace(ReplacementBehavior.ServiceType))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime()
            );
            return services;
        }


        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            var opt = services.BuildServiceProvider().GetRequiredService<IOptions<SecuritySettings>>();
            var settings = opt.Value;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services.AddIdentity<User, Role>(o =>
            {
                o.SignIn.RequireConfirmedEmail = true;
                o.ClaimsIdentity.UserNameClaimType = UserNameClaimType;
                o.ClaimsIdentity.UserIdClaimType = UserIdClaimType;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                //o.SignIn.RequireConfirmedEmail = true;
                o.User.RequireUniqueEmail = true;
            })
            .AddUserStore<UserStore<User, Role, AppDbContext, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>>()
            .AddRoleStore<RoleStore<Role, AppDbContext, Guid, UserRole, IdentityRoleClaim<Guid>>>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();

            services
                .AddAuthorization()
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = settings.Authentication.Jwt.Issuer,
                        ValidAudience = settings.Authentication.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(settings.Authentication.Jwt.Base64Secret)),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true,
                        NameClaimType = UserNameClaimType
                    };
                });

            services.AddScoped<IPasswordHasher<User>, PasswordHasher>();
            services.AddScoped<IClaimsTransformation, RoleClaimsTransformation>();
            services.AddScoped<ITokenProvider, JwtTokenProvider>();


            return services;
        }
    }

}
