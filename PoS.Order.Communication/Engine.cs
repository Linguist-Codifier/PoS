using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using PoS.IoC.Services.PipeLines;

namespace PoS.Order.Communication
{
    /// <summary>
    /// Web application startup point.
    /// </summary>
    public sealed class Engine
    {
        #pragma warning disable CS1591
        public static void Main(String[] args)
        #pragma warning restore CS1591
        {
            WebApplicationBuilder system = WebApplication.CreateBuilder(args);

            system.Services.Initiate();

            WebApplication application = system.Build();

            /* HTTP request pipeline. */
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.UseHttpsRedirection();

            application.UseAuthorization();

            application.UseCors(config => config.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            application.MapControllers();

            application.Run();
        }
    }
}