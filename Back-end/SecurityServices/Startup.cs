using SecurityServices.BusinessLogic;
using SecurityServices.Common;
using SecurityServices.IBusinessLogic;
using SecurityServices.Models;

namespace SecurityServices;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {

        EnvironmentHelper.LoadEnvVars();
        EnvironmentHelper.LoadCommonSecrets();
        services.AddControllers();
        services.AddFluentEmail(EnvironmentHelper.COMMON_EMAIL_ID).AddMailGunSender("securebiz.live", EnvironmentHelper.COMMON_EMAIL_APIKEY);

        services.AddDbContext<serviceContext>();
        services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
        services.AddScoped<IProductBusinessLogic, ProductBusinessLogic>();
        services.AddScoped<ITransactionBusinessLogic, TransactionBusinessLogic>();
        services.AddScoped<IContactBusinessLogic, ContactBusinessLogic>();

        services.AddCors(options =>
        {

            options.AddPolicy("cors", policy =>
            {
                policy.WithOrigins("*")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
           
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors("cors");

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}