using ClientProject.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OpenBankingCore;
using OpenBankingCore.RepositoryService;

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
            builder.Services.AddCors(policy=>policy.AddDefaultPolicy(def=>def.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            builder.Services.AddCors(policy=>policy.AddPolicy(myPolicy, def => def.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }

            app.UseCors(myPolicy);
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}