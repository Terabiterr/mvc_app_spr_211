using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc_app.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //Add service products DI container
        builder.Services.AddScoped<IServiceProducts, ServiceProducts>();
        builder.Services.AddDbContext<ProductContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });
        //Identity context
        builder.Services.AddDbContext<UserContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            //confirmed email
            options.SignIn.RequireConfirmedEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredUniqueChars = 0;
        })
            .AddRoles<IdentityRole>() //Important include first
            .AddEntityFrameworkStores<UserContext>(); //Next

        builder.Services.AddControllersWithViews();
        var app = builder.Build();
        
        app.UseRouting(); //Important include first
        //identity
        app.UseAuthentication(); //Next
        app.UseAuthorization(); //Next

        app.UseStaticFiles();
        //https://localhost:[port]/
        app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

        app.Run();
    }
}
