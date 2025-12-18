using Broker.Services;
using Broker.Services.Interfaces;

namespace Broker
{
     public class Startup
     {
          public void ConfigureServices(IServiceCollection services)
          {
               services.AddGrpc();

               services.AddSingleton<IMessageStorageService, MessageStorageService>();
               services.AddSingleton<IConnectionStorageService, ConnectionStorageService>();
               services.AddHostedService<SenderWorker>();
          }

          public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
          {
               if (env.IsDevelopment())
               { 
                    app.UseDeveloperExceptionPage();
               }
              
               app.UseRouting();

               app.UseEndpoints(endpoints =>
               {
                    endpoints.MapGrpcService<PublisherService>();
                    endpoints.MapGrpcService<SubscriberService>();

                    endpoints.MapGet("/", async context =>
                    {
                         await context.Response.WriteAsync("Communication with gRPC endpoints must made through a gRPC client");

                    });
               });
          }
     }
}
