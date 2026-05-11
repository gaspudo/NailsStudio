using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsStudio.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace NailsStudio.Data
{
    public class SeedData
    {
        public static async Task InicializarAsync (IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<Usuario>>();

            string [] roles = ["Admin", "Ciente"];
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role)) 
                    await roleManager.CreateAsync(new IdentityRole (role));
            }

            string adminEmail = Environment.GetEnvironmentVariable("emailAdmin")!;
            string adminSenha = Environment.GetEnvironmentVariable("senhaAdmin")!;

            var adminExistente = await userManager.FindByEmailAsync(adminEmail);
            if(adminExistente == null)
            {
                var admin = new Usuario
                {
                    NomeCompleto = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var resultado = await userManager.CreateAsync(admin, adminSenha);
                if (!resultado.Succeeded)
            throw new Exception($"Erro ao criar admin: {string.Join(", ", resultado.Errors.Select(e => e.Description))}");
                

                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}