using AutoMapper;
using Common.Helpers;
using Facades;
using Infrastructure.EFCore;
using Infrastructure.EFCore.AutoMapper;
using Infrastructure.EFCore.Models;
using Infrastructure.EFCore.Services;
using Interfaces.Facades;
using Interfaces.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DI
{
    public static class DependencyMapper
    {
        public static IServiceCollection AddDependencyModule(this IServiceCollection services, IConfiguration configuration)
        {
            //database
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //identity
            services.AddIdentity<User, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<Context>();

            //authentication && JWT options
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Sensitive:Token"]))
                };
            });


            //mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new LeagueProfile());
                mc.AddProfile(new MatchProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new TeamProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            //infrastructure
            services.AddScoped<ICoachService, CoachService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IUserSrv, UserSrv>();
            services.AddScoped<ITokenSrv, TokenSrv>();

            //fascades
            services.AddScoped<ICoachFcd, CoachFcd>();
            services.AddScoped<IMatchFcd, MatchFcd>();
            services.AddScoped<ITeamFcd, TeamFcd>();
            services.AddScoped<ILeagueFcd, LeagueFcd>();
            services.AddScoped<IUserFcd, UserFcd>();
            services.AddScoped<ITokenFcd, TokenFcd>();
            return services;
        }
    }
}
