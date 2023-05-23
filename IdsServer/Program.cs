using Duende.IdentityServer.EntityFramework.DbContexts;
using Entity=Duende.IdentityServer.EntityFramework.Entities;
using Newtonsoft.Json;
using Duende.IdentityServer.Models;
using Secret=Duende.IdentityServer.Models.Secret;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel.Client;
using System.Linq;
using IdsServer.Model;
using System.Text;

namespace IdsServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
            });
            builder.Services.AddMvc();
            //builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddNewtonSoftJson();
            builder.Services.AddIdentityServer(opt =>
            {
                opt.EmitStaticAudienceClaim = true;
                opt.Events.RaiseSuccessEvents = true;
                opt.Events.RaiseFailureEvents = true;
                opt.Events.RaiseErrorEvents = true;
                opt.Events.RaiseInformationEvents = true;
                
                opt.AccessTokenJwtType = "jwt";
            }).AddConfigurationStore(opt =>
            {
                opt.ConfigureDbContext = op => op.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EndPointsSecurityServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                        mig => mig.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            }).AddOperationalStore(opt =>
            {
                opt.ConfigureDbContext = op => op.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EndPointsSecurityServer;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False",
                    mig => mig.MigrationsAssembly(typeof(Program).Assembly.GetName().Name));
            });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI( endpoint=>
                {
                    endpoint.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Server");
                    endpoint.RoutePrefix = "";
                });
                app.UseDeveloperExceptionPage();
            }
            
           // app.MapGet("/", () => "Hello World!");
            app.MapPost("/api/create/client", async ([FromBody]ClientDto clientDto) =>
            {
                var apiKey =Guid.NewGuid().ToString();
                Console.WriteLine(apiKey);
                Client client = new Client
                {
                    ClientId = clientDto.ClientId,
                    ClientName=clientDto.ClientName,
                    AllowedScopes= { "read" },
                    AllowedGrantTypes=GrantTypes.ClientCredentials,//clientDto.AllowedGtrantType,
                    ClientSecrets = { new Secret(apiKey.Sha256()) }
                };
                //ApiScope clientScope = new ApiScope("read");

                var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                await context!.Clients.AddAsync(client.ToEntity());
                context.SaveChanges();
                return Results.Ok(apiKey);
            });
            app.MapPost("/api/get/token", async ([FromBody]TokenRequestDto reqDto) =>
            {
                var httpClient = new HttpClient();
                var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateAsyncScope();
                var context = scope.ServiceProvider.GetService<ConfigurationDbContext>();
                var dbQuery = context.Clients.Where(x=>x.ClientId==reqDto.ClientId).ToList();
                Entity.Client client = dbQuery.FirstOrDefault();
                Client modelClient = client.ToModel();
                Console.WriteLine("Debug 1");
                var req = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5036/");
                Thread.Sleep(5000);
                //Console.WriteLine("Debug 2");
                Console.WriteLine($"Token endpoint:{req.TokenEndpoint} ClientId:{modelClient.ClientId} ClientSecret:{reqDto.ClientSecret}");
                var token = httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                {
                    Address=req.TokenEndpoint,
                    ClientId = modelClient.ClientId,
                    ClientSecret = reqDto.ClientSecret,
                    Scope ="read"
                });
                Console.WriteLine(token.ToString());
                return token.Result.AccessToken;
            });

            app.MapPost("/api/create/resource", ([FromBody]ResourceDto resourceDto) =>
            {
                var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var context=scope.ServiceProvider.GetService<ConfigurationDbContext>();
                ApiResource resource = new ApiResource(resourceDto.ResourceName)
                {
                    Scopes = new List<string>(),
                    UserClaims= new List<string>(),
                    ApiSecrets= new List<Secret>()

                };
                resourceDto.Scopes.ForEach(scope=> resource.Scopes.Add(scope));
                resourceDto.UserClaims.ForEach(claim=>resource.UserClaims.Add(claim));
                resourceDto.Secrets.ForEach(x => resource.ApiSecrets.Add(
                    new Secret(x.Sha256())));
                context.ApiResources.Add(resource.ToEntity());
                context.SaveChanges();

            /*{
                "resourceName": "OpenBankingCore",
                "scopes": [
                "read","write"
  ],
                "secrets": [
           "OpenBankingCoreSuperSecret"
                ],
                "userClaims": [
                "email","username"
                ]
                }*/
    });

            app.MapPost("/api/create/scope", ([FromBody] ScopeDto scopeDto) =>
            {
                var instanceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var instanceContext = instanceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                scopeDto.Scopes.ForEach(scope => instanceContext.ApiScopes.Add(new ApiScope(scope).ToEntity()));
                instanceContext.SaveChanges();
            });
            app.MapPost("/api/create/identity", () =>
            {
                var ids= new List<IdentityResource> {
                new IdentityResources.Profile(),
                new IdentityResources.OpenId(),
                new IdentityResource
                {
                    Name="creditor",
                    UserClaims=new[] {"email","clientId"}
                },
                new IdentityResource
                {
                    Name="user",
                    UserClaims=new[] {"user"}
                }
            };
                var instanceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                var instanceContext = instanceScope.ServiceProvider.GetService<ConfigurationDbContext>();
                ids.ForEach(id => instanceContext.IdentityResources.Add(id.ToEntity()));
                instanceContext.SaveChanges();
            });
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIdentityServer();
            app.Run();
        }
    }
}