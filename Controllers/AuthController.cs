using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NailsStudio.Models.Entities;
using NailsStudio.Models.ViewModels;

namespace NailsStudio.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager <Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public AuthController(UserManager<Usuario> user, SignInManager<Usuario> signIn)
        {
            _signInManager = signIn;
            _userManager = user;
        }

        // Get /Auth/Login
        public IActionResult Login() => View();

        //Post = /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var resultado = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Senha,
                model.LembrarMe,
                lockoutOnFailure: true);

            if (resultado.Succeeded)
            {
                var usuario = await _userManager.FindByEmailAsync(model.Email);
                var isAdmin = await _userManager.IsInRoleAsync(usuario!, "Admin");

                return isAdmin ? RedirectToAction("Index", "Dashboard", new {area = "Admin"})
                : RedirectToAction("Index", "Home");
            }

            if(resultado.IsLockedOut)
            {
                ModelState.AddModelError("", "Conta bloqueada por muitas tentativas.");
                return View(model);
            }

            ModelState.AddModelError("", "E-mail ou senha incorretos.");
            return View(model);
        }

        public IActionResult Registro() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro (RegistroViewModel model)
        {
            if(!ModelState.IsValid) return View(model);

            var usuario = new Usuario
            {
                NomeCompleto = model.NomeCompleto,
                UserName = model.Email,
                Email = model.Email
            };

            var resultado = await _userManager.CreateAsync(usuario, model.Senha);

            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, "Cliente");
                await _signInManager.SignInAsync(usuario, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var erro in resultado.Errors) {
                ModelState.AddModelError("",erro.Description);
                }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth", new { area = ""});
        }

        public IActionResult AcessoNegado() => View();
    }
}