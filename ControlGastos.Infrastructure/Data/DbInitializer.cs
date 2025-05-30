using ControlGastos.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ControlGastos.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void SeedAdminUser(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Usuarios.Any(u => u.UserName == "admin"))
            {
                context.Usuarios.Add(new Usuario
                {
                    UserName = "admin",
                    Password = "admin",
                    Rol = "admin"
                });
                context.SaveChanges();
            }
        }
    }
}
