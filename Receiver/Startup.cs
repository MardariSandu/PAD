using Receiver.Services;

namespace Receiver
{
     public class Startup
     {
          public void ConfigureServices(IServiceCollection services)
          {
               services.AddGrpc();
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
                    endpoints.MapGrpcService<NotificationService>();

                    endpoints.MapGet("/", async context =>
                    {
                         await context.Response.WriteAsync("Communication with gRPC endpoints must made through a gRPC client");

                    });
               });
          }
     }
}
