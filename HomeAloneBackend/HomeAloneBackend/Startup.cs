using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using BackgroundWorker;
using HomeAloneBackend.Contexts;
using HomeAloneBackend.Lib;
using HomeAloneBackend.Services;

namespace HomeAloneBackend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();

            services.AddMyDbContext<AnalyzerDbContext>(Configuration);

            services.AddHostedService<QueuedHostingService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
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

            // DM 03/28/2020 This is not ideal...
            app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
