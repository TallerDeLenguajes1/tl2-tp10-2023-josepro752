using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private ITableroRepository tableroRepository;

    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        tableroRepository = new TableroRepository();
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            List<Tablero> tableros = tableroRepository.GetAllTableros();
            return View(tableros);
        } else {
            List<Tablero> tableros = tableroRepository.GetAllTableros().FindAll(t => t.Id == HttpContext.Session.GetInt32("Id"));            
            return View("TableroOperador",tableros);
        }
    }

    [HttpGet]
    public IActionResult AddTablero() {
        if (isAdmin()) {
            return View(new Tablero ());
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult AddTablero(Tablero tablero) {
        if (isAdmin()) {
            tableroRepository.AddTablero(tablero);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult UpdateTablero(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            Tablero tablero = tableroRepository.GetTablero(id);
            return View(tablero);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                Tablero tablero = tableroRepository.GetTablero(id);
                return View("UpdateTableroOperador",tablero);
            } else {
                return RedirectToAction("Index");
            }
        }
    }

    [HttpPost]
    public IActionResult UpdateTablero(int id, Tablero tablero) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            tableroRepository.UpdateTablero(id,tablero);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                tableroRepository.UpdateTablero(id,tablero);
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTablero(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            tableroRepository.DeleteTablero(id);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                tableroRepository.DeleteTablero(id);
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
