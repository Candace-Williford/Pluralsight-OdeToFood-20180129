using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace Microsoft.AspNetCore.Builder //changing the namespace to this makes this middleware more accessible
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(
            this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules"; //only handle requests that start with the string
            options.FileProvider = fileProvider;
            
            app.UseStaticFiles(options);

            return app;
        }
    }
}