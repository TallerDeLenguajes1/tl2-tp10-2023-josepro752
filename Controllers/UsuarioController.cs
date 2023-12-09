using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _usuarioRepository;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewUsuarioListar usuarios = new ViewUsuarioListar(_usuarioRepository.GetAllUsuarios());
            return View(usuarios);
        } else {
            ViewUsuarioListar usuario = new ViewUsuarioListar(_usuarioRepository.GetAllUsuarios().FindAll(u => u.Id == HttpContext.Session.GetInt32("Id")));
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
        if (ModelState.IsValid) {     
            if (isAdmin()) {
                var usuario = new Usuario(viewUsuarioAdd);
                _usuarioRepository.AddUsuario(usuario);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"});
        }
        return RedirectToAction("AddUsuario");
    }

    [HttpGet]
    public IActionResult UpdateUsuario(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewUsuarioUpdate viewUsuarioUpdate = new ViewUsuarioUpdate(_usuarioRepository.GetUsuario(id));
            return View(viewUsuarioUpdate);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                ViewUsuarioUpdate viewUsuarioUpdate = new ViewUsuarioUpdate(_usuarioRepository.GetUsuario(id));
                return View("UpdateUsuarioOperador",viewUsuarioUpdate);
            } else {
                return RedirectToAction("Index");
            }
        }
    }

    [HttpPost]
    public IActionResult UpdateUsuario(int id, ViewUsuarioUpdate viewUsuarioUpdate) {
        if (ModelState.IsValid) {
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if (isAdmin()) {
                var usuario = new Usuario(viewUsuarioUpdate);
                _usuarioRepository.UpdateUsuario(id,usuario);
            } else {
                if (HttpContext.Session.GetInt32("Id") == id) {
                    viewUsuarioUpdate.Rol = "Operador"; //triquiñuela porque me llega el rol en null
                    var usuario = new Usuario(viewUsuarioUpdate);
                    _usuarioRepository.UpdateUsuario(id,usuario);
                }
            }
            return RedirectToAction("Index");
        }
        return RedirectToAction("UpdateUsuario");
    }

    public IActionResult DeleteUsuario(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            _usuarioRepository.DeleteUsuario(id);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id)  {
                _usuarioRepository.DeleteUsuario(id);
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
