using Ecommerce.api.Mapper;
using Ecommerce.api.Middlewares;
using Ecommerce.core.DTOs.Requests;
using Ecommerce.core.Entites.Product;
using Ecommerce.infrastructure;
using System.Threading.RateLimiting;
namespace Ecommerce.api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 30,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }
                    )
                );

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    await context.HttpContext.Response.WriteAsync("Too many requests, please try again later.");
                };
            });

            builder.Services.InfrastructureConfiguration(builder.Configuration);

            #region Auto Mapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            //builder.Services.AddAutoMapper(typeof(ConfMapping).Assembly);
            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "API v1");
                });
            }
            
            app.UseRateLimiter(); // ✅ كده بيطبق على كل الـ endpoints أوتوماتيك
            app.UseHttpsRedirection();
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
