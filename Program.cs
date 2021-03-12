﻿using System.Threading.Tasks;

using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using project_alfred.Services;

namespace project_alfred
{
    internal class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var services = GetServices();

            var boot = services.GetRequiredService<BootService>();

            await boot.Start();
            await Task.Delay(-1);
        }

        private ServiceProvider GetServices()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var services = new ServiceCollection()
                .AddSingleton<IConfigurationRoot>(config)
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<LoggerService>()
                .AddSingleton<BootService>();

            return services.BuildServiceProvider();
        }
    }
}