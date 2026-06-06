using Mango.Services.ShoppingCartAPI;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.Extension;
using Mango.Services.ShoppingCartAPI.Service;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Mango.Services.ShoppingCartAPI.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("Product", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ProductAPI"]))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();
builder.Services.AddHttpClient("Coupon", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:CouponAPI"]))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

builder.Services.Scan(scan => scan
    .FromAssemblyOf<Program>()
    // Find all public concrete classes...
    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
    // ...and register them matching their default interface (e.g., IUserService -> UserService)
    .AsImplementedInterfaces()
    .WithScopedLifetime());


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
