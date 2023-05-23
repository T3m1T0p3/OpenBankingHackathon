using ApiResource.Model;
using ApiResource.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApiResource
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.e

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CreditScoreCore;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False",
                    mig => mig.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Seeder>();
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.Authority = "http://localhost:5036";
                opt.Audience = "OpenBankingCore";
                opt.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.IncludeErrorDetails = true;
            });
            builder.Services.AddAuthorization(opt =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                     JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                opt.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            var app = builder.Build();
            var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var seeder = new Seeder(context);
           //seeder.AddCustomer("Master", "Account", 124);
            //seeder.AddCustomer("Bros","S.0",33);
            //seeder.AddCustomer("Mrs Ameh", "Adeola", 29);
            //seeder.CreateTransaction("0000-0000-0000", "113200000014", 120000,"paired",DateTime.Now);
            //seeder.CreateTransaction("0000-0000-0000", "113200000021", 650000, "paired",DateTime.Now);
            //seeder.GenerateRandomTransactions();
            //CreditScore creditScore = new CreditScore(700000,context.AccountBalances.Where(acct=>acct.BankCustomerId== "113200000014").ToList());
            //Console.WriteLine(creditScore.GetCreditScore());
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            //app.UseHttpsRedirection();
            //app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(point =>
            {
                point.MapControllers();
            });

            app.Run();
        }
    }
}