using System;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
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
            services.AddTransient<NotifyMeetupCreated>(p =>
            {
                return id =>
                {
                    ConnectionFactory factory = new ConnectionFactory();
                    factory.Uri = "amqp://guest:guest@rabbitmq:5672";

                    IConnection conn = factory.CreateConnection();
                    IModel channel = conn.CreateModel();
                    string exchangeName = "dsfds";
                    channel.QueueDeclare("MyQueue", exclusive:false);

                    byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(id.ToString());
                    channel.BasicPublish("", "MyQueue", null, messageBodyBytes);

                    //conn.Close();
                };
            });
            services.AddTransient<MeetupApplicationConfig>();
            services.AddTransient<ScheduleMeetupCommandHandler>();

            return services;
        }
    }
}