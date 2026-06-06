
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Extension;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnectionString"));
            });
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.AddAppAuthetication();
            builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            ApplyMigration();

            app.Run();
            void ApplyMigration()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    if (dbContext.Database.GetPendingMigrations().Count() > 0)
                    {
                        dbContext.Database.Migrate();
                    }
                }
            }
        }
    }
}
