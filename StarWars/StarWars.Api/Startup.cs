using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StarWars.Api.Models;
using StarWars.Core.Data;
using StarWars.Data.EntityFramework;
using StarWars.Data.EntityFramework.Seed;
using StarWars.Data.EntityFramework.Repositories;

namespace StarWars.Api
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddScoped<StarWarsQuery>();
            services.AddTransient<IDroidRepository, DroidRepository>();
            if (_environment.IsEnvironment("Test"))
            {
                services.AddDbContext<StarWarsContext>(options =>
                    options.UseSqlite("Data Source=StarWars.db"));
                //services.AddDbContext<StarWarsContext>(options =>
                //    options.UseInMemoryDatabase(databaseName: "StarWars"));
            }
            else
            {
                services.AddDbContext<StarWarsContext>(options => 
                    options.UseSqlServer(Configuration["ConnectionStrings:StarWarsDatabaseConnection"]));
            }
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            var sp = services.BuildServiceProvider();
            services.AddScoped<ISchema>(_ => new Schema {Query = sp.GetService<StarWarsQuery>()});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, StarWarsContext db)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            db.EnsureSeedData();
        }
    }
}
