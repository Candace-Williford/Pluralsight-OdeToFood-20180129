using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Data;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)//not an injectable method. use ctor
        {
            //whenever someone needs a service that implements IGreeter, create an instance of Greeter and pass that service along
            services.AddSingleton<IGreeter, Greeter>(); //any comp across entire of app, use same instance of I greeter
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>(); //create an instance of InMemoryRestaurantData for each request. standard behaviour
            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("OdeToFood")));
            services.AddScoped<IRestaurantData, SqlRestaurantData>(); //changed it to singleton because we want our changes to the list of restaurants to persist across different requests. This would likely cause errors in an environment with multiple users
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IHostingEnvironment env,
            IGreeter greeter,
            Microsoft.Extensions.Logging.ILogger<Startup> log)
        {

            //order of middleware matters. be careful. can alter some behaviour using options.
            // app.UseWelcomePage(new WelcomePageOptions
            // {
            //     Path="/wp"
            // });

            //app.Use( takes a function that takes a delegate and a task (usually an async method))
            //this function is only called ONCE by .NET Core
            // app.Use(next => //"next" allows the NEXT piece of middleware to process this request.
            // {
            //     return async context => //this middle function is the middleware and is called once per HTTP request
            //     {
            //         log.LogInformation("Request incoming"); //you can see these loggers on the ASP.NET core output
            //         if(context.Request.Path.StartsWithSegments("/mym"))
            //         {
            //             log.LogInformation("Request handled");
            //             await context.Response.WriteAsync("Hit!!");
            //         }
            //         else //if I can't handle this request, then go ahead and pass it on to the next piece of middleware
            //         {
            //             await next(context);
            //             log.LogInformation("Response outgoing");
            //         }
            //     };
            // });

            //using appsettings.environmentname.json will allow you to have different configurations depending on the environment you are currently in. This would be useful if you have different DB connection strings across different environments.
            if (env.IsDevelopment()) //set the ASPNETCORE_ENVIRONMENT = development to trigger this
            {
                //env.EnvironmentName
                app.UseDeveloperExceptionPage(); //gives an interface that shows exception details, stacktrace, request details etc. Very handy for dev debugging. Very big security issue to ever display this to a user
            }
            else
                app.UseExceptionHandler();

            //app.UseDefaultFiles();
            app.UseStaticFiles();
            //app.UseFileServer(); //does the work of both of the previous 2 middleware
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(ConfigureRoutes);


            app.Run(async (context) =>
            {
                var greeting = greeter.GetMessageOfTheDay();
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync($"Not found");
            });
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index/4 // use = to set default of param
            routeBuilder.MapRoute("Default","{controller=Home}/{action=Index}/{id?}"); // ? makes the corresponding param optional
            //MVC framework checks the routing data before the query string if both are provided
        }
    }
}
