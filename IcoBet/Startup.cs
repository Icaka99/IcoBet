namespace IcoBet
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using IcoBet.Data;
    using IcoBet.Services;
    using IcoBet.Services.Data;
    using IcoBet.Services.Mapping;
    using IcoBet.Services.Data.Interfaces;

    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSingleton(this.configuration);

            //Data Repositories
            services.AddTransient<ApplicationDbContext>();

            //Application Services
            services.AddTransient<IPullingService, PullingService>();
            services.AddTransient<IParsingService, ParsingService>();
            services.AddTransient<ISavingService, SavingService>();
            services.AddTransient<IMappingService, MappingService>();
            services.AddTransient<ISportService, SportService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IMatchService, MatchService>();
            services.AddTransient<IBetService, BetService>();
            services.AddTransient<IOddService, OddService>();
            services.AddTransient<ICategoryService, CategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
