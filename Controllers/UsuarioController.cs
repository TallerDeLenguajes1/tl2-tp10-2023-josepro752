using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewUsuarioListar usuarios = new ViewUsuarioListar(usuarioRepository.GetAllUsuarios());
            return View(usuarios);
        } else {
            ViewUsuarioListar usuario = new ViewUsuarioListar(usuarioRepository.GetAllUsuarios().FindAll(u => u.Id == HttpContext.Session.GetInt32("Id")));
            return View(usuario);
        }
    }

    [HttpGet]
    public IActionResult AddUsuario() {
        if (isAdmin()) {
            return View(new ViewUsuarioAdd());
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }
    
    [HttpPost]
    public IActionResult AddUsuario(ViewUsuarioAdd viewUsuarioAdd) {
        if (isAdmin()) {
            var usuario = new Usuario(viewUsuarioAdd);
            usuarioRepository.AddUsuario(usuario);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult UpdateUsuario(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewUsuarioUpdate viewUsuarioUpdate = new ViewUsuarioUpdate(usuarioRepository.GetUsuario(id));
            return View(viewUsuarioUpdate);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                ViewUsuarioUpdate viewUsuarioUpdate = new ViewUsuarioUpdate(usuarioRepository.GetUsuario(id));
                return View("UpdateUsuarioOperador",viewUsuarioUpdate);
            } else {
                return RedirectToAction("Index");
            }
        }
    }

    [HttpPost]
    public IActionResult UpdateUsuario(int id, ViewUsuarioUpdate viewUsuarioUpdate) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            var usuario = new Usuario(viewUsuarioUpdate);
            usuarioRepository.UpdateUsuario(id,usuario);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                var usuario = new Usuario(viewUsuarioUpdate);
                usuarioRepository.UpdateUsuario(id,usuario);
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult DeleteUsuario(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            usuarioRepository.DeleteUsuario(id);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id)  {
                usuarioRepository.DeleteUsuario(id);
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public bool isAdmin() {
        if (HttpContext.Session.GetString("Rol") != null && HttpContext.Session.GetString("Rol") == "Administrador") {
            return true;
        }
        return false;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
