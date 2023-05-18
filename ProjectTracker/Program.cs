using System.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

using ProjectTracker.Data;
using ProjectTracker.Interfaces;
using ProjectTracker.Repository;

namespace ProjectTracker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        //Added Newtonsoftjson to handle infinite reference loop, and changed the enum converter to be able to chain them.
        builder.Services.AddControllers().
            AddJsonOptions(opts =>
        {
            opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
        /*
        builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
        }); 
        */
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // For mapping models to DTOs

        builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseMySQL(
                    builder.Configuration.GetConnectionString("myConnectionString")
                    ));
        builder.Services.AddScoped<IProjectItemRepository, ProjectItemRepository>();
        builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
        builder.Services.AddScoped<IPersonRepository, PersonRepository>();
        //Added Swagger for the API
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();

        app.Run();
    }
}
