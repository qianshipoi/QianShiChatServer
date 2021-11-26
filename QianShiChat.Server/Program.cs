using Microsoft.EntityFrameworkCore;

using QianShiChat.Server.Configs;
using QianShiChat.Server.Extensions;
using QianShiChat.Server.Hubs;
using QianShiChat.Server.Models.Entities;

namespace QianShiChat.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string SignalRCorsName = "SignalRCorsName";
            var builder = WebApplication.CreateBuilder(args).Inject();

            builder.Services.AddCors(options => options.AddPolicy(SignalRCorsName, policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            builder.Services.AddJWTService();

            builder.Services.AddSignalR();
            builder.Services.AddControllers()
                .AddInjectWithUnifyResult();

            builder.Services.Configure<ProjectConfig>(builder.Configuration.GetSection("ProjectConfig"));

            builder.Services.AddDbContext<ChatDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();

                var connectionStr = builder.Configuration.GetConnectionString("ChatDbContext");
                connectionStr = builder.Configuration["mysqlConnectionString"]; // Ê¹ÓÃSecret ManagerÅäÖÃ
                options.UseMySql(connectionStr, ServerVersion.Parse("8.0.22"));
            });

            var app = builder.Build();

            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseInject(string.Empty);
            //}
            app.UseInject(string.Empty);

            app.UseStaticFiles();

            app.UseCors(SignalRCorsName);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<ChatHub>("/chathub").RequireAuthorization();
                endpoint.MapControllers();
            });

            app.Run();
        }
    }
}
