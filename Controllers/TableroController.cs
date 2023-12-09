using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TableroController(ILogger<TableroController> logger, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewTableroListar tableros = new ViewTableroListar(_tableroRepository.GetAllTableros(),_usuarioRepository.GetAllUsuarios());
            return View(tableros);
        } else {
            ViewTableroListar tableros = new ViewTableroListar(_tableroRepository.GetAllTableros().FindAll(t => t.IdUsuarioPropietario == HttpContext.Session.GetInt32("Id")),_usuarioRepository.GetAllUsuarios());
            return View("IndexOperador",tableros);
        }
    }

    [HttpGet]
    public IActionResult AddTablero() {
        if (isAdmin()) {
            return View(new ViewTableroAdd (_usuarioRepository.GetAllUsuarios()));
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult AddTablero(ViewTableroAdd viewTableroAdd) {
        if (ModelState.IsValid) {
            if (isAdmin()) {
                var tablero =  new Tablero(viewTableroAdd);
                _tableroRepository.AddTablero(tablero);
                return RedirectToAction("Index");
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"});
        }
        return RedirectToAction("AddTablero");
    }

    [HttpGet]
    public IActionResult UpdateTablero(int id) {
        var tablero = _tableroRepository.GetTablero(id);
        if (tablero != null) {
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if (isAdmin()) {
                ViewTableroUpdate viewUsuarioUpdate = new ViewTableroUpdate(_tableroRepository.GetTablero(id),_usuarioRepository.GetAllUsuarios());
                return View(viewUsuarioUpdate);
            } else {
                if (HttpContext.Session.GetInt32("Id") == tablero.IdUsuarioPropietario) {
                    ViewTableroUpdate viewUsuarioUpdate = new ViewTableroUpdate(_tableroRepository.GetTablero(id),_usuarioRepository.GetAllUsuarios());
                    return View("UpdateTableroOperador",viewUsuarioUpdate);
                }
            }
        }                   
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateTablero(int id, ViewTableroUpdate viewTableroUpdate) {
        if (ModelState.IsValid) {
            var t = _tableroRepository.GetTablero(id);
            if (t != null) {
                if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
                if (isAdmin()) {
                    Tablero tablero = new Tablero(viewTableroUpdate);
                    _tableroRepository.UpdateTablero(id,tablero);
                } else {
                    if (HttpContext.Session.GetInt32("Id") == t.IdUsuarioPropietario) {
                        Tablero tablero = new Tablero(viewTableroUpdate);
                        _tableroRepository.UpdateTablero(id,tablero);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        return RedirectToAction("UpdateTablero");
    }

    public IActionResult DeleteTablero(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            _tableroRepository.DeleteTablero(id);
        } else {
            var tablero = _tableroRepository.GetTablero(id);
            if (tablero != null && HttpContext.Session.GetInt32("Id") == tablero.IdUsuarioPropietario) {
                _tableroRepository.DeleteTablero(id);
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
