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
            services.AddScoped<ISeasonService, SeasonService>();
            services.AddScoped<ISeasonRepository, SeasonRepository>();
            services.AddScoped<ITeamStatService, TeamStatService>();
            services.AddScoped<ITeamStatRepository, TeamStatRepository>();
            services.AddScoped<ITeamSeasonAverageService, TeamSeasonAverageService>();
            services.AddScoped<ITeamSeasonAverageRepository, TeamSeasonAverageRepository>();
            services.AddScoped<IPlayerStatService, PlayerStatService>();
            services.AddScoped<IPlayerStatRepository, PlayerStatRepository>();
            services.AddScoped<IPlayerSeasonAverageService, PlayerSeasonAverageService>();
            services.AddScoped<IPlayerSeasonAverageRepository, PlayerSeasonAverageRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPreferenceService, UserPreferenceService>();
            services.AddScoped<IUserPreferencesRepository, UserPreferencesRepository>();
        }
    }
}