
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WalletApi.Services.Interfaces;
using WalletApi.Services;
using WalletApi.Data;
using WalletApi.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WalletApi.Helpers;
using System.Security.Claims;


namespace WalletApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var conStr = builder.Configuration.GetConnectionString("DefaultConnection");    
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conStr)); 
            
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<ICurrencyApiService, CurrencyApiService>();
            builder.Services.AddScoped<IAdminService, AdminService>();  
            builder.Services.AddScoped<IWalletService, WalletService>();
            builder.Services.AddScoped<IRepositoryService, RepositoryService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAutoMapper(typeof(Program));
          

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Fast api",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[]{}
                 }
                    });
                 });

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value);
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

          

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                await SeedData(userManager, roleManager);
            }

            app.MapControllers();

            app.Run();
        }

        private static async Task SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Seed Role
            string roleName = "Admin";
         
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
            
            // Seed User
            var adminUser = new AppUser
            {
                UserName = "admin@example.com",
                Email = "admin@example.com"

            };
            await userManager.CreateAsync(adminUser, "Admin123!");
            await userManager.AddToRoleAsync(adminUser, "Admin");

            

        }
    }
}
