using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using purrfectpawsApi2.Models;
using System.Text;

namespace purrfectpawsApi2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var MyAlloSpecificationOrigins = "_myAllowSpecificationOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(MyAlloSpecificationOrigins,
                    policy =>
                    {
                        policy.WithOrigin("https://salmon-coast-085a7fd00.3.azurestaticapps.net").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<PurrfectpawsContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnString"));
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Purrfect Paws API", Version = "v1" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Purrfect Paws API * V1");
                    //options.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(MyAlloSpecificationOrigins);

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
