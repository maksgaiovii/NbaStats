using Microsoft.Extensions.DependencyInjection;
using NbaStats.BLL.Interfaces;
using NbaStats.BLL.Services;
using NbaStats.DAL.Interfaces;
using NbaStats.DAL.Repositories;

namespace NbaStats.UAL
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
        }
    }
}