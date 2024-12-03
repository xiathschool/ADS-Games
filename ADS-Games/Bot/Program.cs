using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using NetCord;
using NetCord.Hosting.Gateway;
using NetCord.Hosting.Services;
using NetCord.Hosting.Services.ApplicationCommands;
using NetCord.Services.ApplicationCommands;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services
    .AddDiscordGateway()
    .AddApplicationCommands<ApplicationCommandInteraction, ApplicationCommandContext>();

var host = builder.Build();

// Adding Discord command modules
host.AddModules(typeof(Program).Assembly);

await host.RunAsync();