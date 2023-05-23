using ClientManager.Services;
using ClientManager.Services.RepositoryService;
using ClientManager.Services.TokenServices;
using ClientProject.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenBankingCore;
using System.Text;

namespace ClientManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string myPolicy=String.Empty;
            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OpenBankingClientDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
                    mig => mig.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });
            builder.Services.AddIdentity<OpenBankingClient, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddScoped<IRepositoryService<OpenBankingClient>, RepositoryService<OpenBankingClient>>();
            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IEncryptionService, EncryptionService>();
            builder.Services.AddCors(policy=>policy.AddDefaultPolicy(def=>def.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            builder.Services.AddCors(policy=>policy.AddPolicy(myPolicy, def => def.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //opt.RequireAuthenticatedSignIn = true;

            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidAudience="http://localhost:5187",
                    ValidIssuer="http://localhost:5187",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("SuperSecretSigningKey"))
                };
            }
            );
            builder.Services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors(myPolicy);
            //app.UseSession();
            //app.UseMvc();

            //app.UseHttpsRedirection();

            /* app.Use(async (context, next) =>
             {
                 var token = context.Session.GetString("Token");
                 if (!string.IsNullOrEmpty(token))
                 {
                     context.Request.Headers.Add("Authorization", "Bearer " + token);
                 }
                 await next();
             });*/
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}