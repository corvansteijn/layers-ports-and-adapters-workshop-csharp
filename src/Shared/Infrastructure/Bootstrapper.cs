using Microsoft.Extensions.DependencyInjection;
using Shared.Application;
using Shared.Application.Command;
using Shared.Entity;
using Shared.Command;
using Shared.Infrastructure;

namespace Shared
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IMeetupRepository>(p => new MeetupRepository("//app//var//meetup.txt"));
            services.AddTransient<ScheduleMeetupProvider>();
            services.AddTransient<MeetupApplicationConfig>();
            services.AddTransient<ScheduleMeetupCommandHandler>();

            return services;
        }
    }
}