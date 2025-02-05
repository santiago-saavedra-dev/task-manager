using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskManager.Data;
using TaskManager.Data.Repositories;
using TaskManager.Data.Repositories.Contracts;

namespace TaskManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagerConnection")));
            builder.Services.AddControllers();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            WebApplication app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("AllowRazorPages");
            app.Run();
        }
    }
}
