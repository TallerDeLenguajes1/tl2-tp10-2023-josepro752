﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        return View(new UsuarioLogin());
    }
    public IActionResult Login(UsuarioLogin usuarioLogin)
    {
        if (ModelState.IsValid) {
            var usuario = _usuarioRepository.GetAllUsuarios().FirstOrDefault(u => u.NombreDeUsuario == usuarioLogin.Usuario && u.Contrasenia == usuarioLogin.Contrasenia);
            if (usuario == null) return RedirectToAction("Index");
            LogearUsuario(usuario);
            return RedirectToRoute(new {controller="Home", action="Index"});
        }
        return RedirectToAction("Index");
    }

    private void LogearUsuario(Usuario usuario) {
        HttpContext.Session.SetString("Usuario",usuario.NombreDeUsuario);
        HttpContext.Session.SetInt32("Id",usuario.Id);
        HttpContext.Session.SetString("Rol",usuario.Rol);
    } 
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
