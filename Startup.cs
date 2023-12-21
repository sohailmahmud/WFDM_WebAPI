using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WFDM.Common;
using Newtonsoft.Json;
using WFDM.IServices;
using WFDM.Services;

namespace WFDM;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddControllers()
            .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

        services.AddSingleton<IConfiguration>(Configuration);
        Global.ConnectionsString = Configuration.GetConnectionString("WFDM");
        services.AddScoped<IUnitOfWorks, UnitOfWorks>();
        services.AddScoped<IPostAPIServices, PostAPIServices>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

}
