using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Logging;
using watchtower.Census;
using watchtower.Realtime;
using watchtower.Services;
using Newtonsoft.Json.Linq;
using watchtower.Models;
using System.IO;
using watchtower.Code.Constants;

namespace watchtower {

    public class Startup {

        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddRouting();

            services.AddCensusServices(options => {
                options.CensusServiceId = "asdf";
                options.CensusServiceNamespace = "ps2";
                //options.LogCensusErrors = true;
            });

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IRealtimeMonitor, RealtimeMonitor>();
            services.AddSingleton<IEventHandler, Realtime.EventHandler>();

            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddSingleton<ICommandBus, CommandBus>();

            services.AddSingleton<ICharacterCollection, CharacterCollection>();
            services.AddSingleton<IItemCollection, ItemCollection>();
            services.AddSingleton<IOutfitCollection, OutfitCollection>();
            services.AddSingleton<IMapRegionCollection, MapRegionCollection>();

            services.AddSingleton<IMatchManager, MatchManager>();
            services.AddSingleton<IEventBroadcastService, EventBroadcastService>();
            services.AddSingleton<IMatchMessageBroadcast, MatchMessageBroadcast>();
            services.AddSingleton<IAdminMessageBroadcast, AdminMessageBroadcast>();

            services.AddHostedService<HostedRealtimeMonitor>();
            services.AddHostedService<EventProcessService>();

            Console.WriteLine($"Services configured");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) { 

            if (env.IsDevelopment()) {
               app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) => {
                context.Response.Headers.Add("Content-Security-Policy",
                    "script-src 'self' * 'unsafe-inline' 'unsafe-eval';"
                    + "style-src 'self' 'unsafe-inline'"
                );
                await next();
            });

            //app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                endpoints.MapControllerRoute(
                    name: "Log Download",
                    pattern: "/logs",
                    defaults: new { }
                );

            });

            Console.WriteLine($"Request pipeline configured");
        }

    }
}
