using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BeautyLuxe.Areas.Identity.Data;
using BeautyLuxe.Repository;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBeautyTipsRepository, BeautyTipsRepository>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create the "admin" role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("admin"))
    {
        var adminRole = new IdentityRole { Name = "admin" };
        await roleManager.CreateAsync(adminRole);
    }

    // Create the "client" role if it doesn't exist
    if (!await roleManager.RoleExistsAsync("client"))
    {
        var clientRole = new IdentityRole { Name = "client" };
        await roleManager.CreateAsync(clientRole);
    }
}

// Create the admin user
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var adminUser = await userManager.FindByNameAsync("admin@gmail.com");

    if (adminUser == null)
    {
        var admin = new User
        {
            FullName = "admin",
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com"
        };

        string password = "Admin123**";
        var result = await userManager.CreateAsync(admin, password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(admin, "admin");
        }
        else
        {
            foreach (var error in result.Errors)
            {
                Console.WriteLine($"Error: {error.Description}");
            }
        }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Temp}/{id?}");


app.MapRazorPages();
app.Run();
