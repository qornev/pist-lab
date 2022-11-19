using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using webapp.Models;
using webapp.Storage;

namespace webapp;

public class Startup
{
    public IConfiguration Config { get; }
    public Startup(IConfiguration config)
    {
        Config = config;
    }

    // This method called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        switch (Config["Storage:Type"].ToStorageEnum())
        {
            case StorageEnum.MemCache:
                services.AddSingleton<IStorage<Place>, MemCache>();
                break;
            case StorageEnum.FileStorage:
                services.AddSingleton<IStorage<Place>>(x => new FileStorage(Config["Storage:FileStorage:Filename"], int.Parse(Config["Storage:FileStorage:FlushPeriod"])));
                break;
            default:
                throw new IndexOutOfRangeException($"Storage type'{Config["Storage:Type"]}' is unknown");
        }
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseMvc();
    }
}