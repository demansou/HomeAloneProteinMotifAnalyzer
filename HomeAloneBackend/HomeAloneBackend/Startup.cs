using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using HomeAloneBackend.Contexts;
using HomeAloneBackend.Lib;
using HomeAloneBackend.Services;

namespace HomeAloneBackend
{
    public class Startup
    {
        public const string MY_CORS_ORIGINS = "_myCorsOrigins";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors(config =>
            {
                config.AddPolicy(MY_CORS_ORIGINS, options => options.AllowAnyOrigin().AllowAnyHeader());
            });

            services.AddMyDbContext<AnalyzerDbContext>(Configuration);
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IFastaFileParser, FastaFileParser>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MY_CORS_ORIGINS);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
