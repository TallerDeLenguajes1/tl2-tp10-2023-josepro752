using System.Diagnostics;
using System.Formats.Tar;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;
    private ITableroRepository tableroRepository;
    private IUsuarioRepository usuarioRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
        tableroRepository = new TableroRepository();
        usuarioRepository = new UsuarioRepository();
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewListarTareas tareas = new ViewListarTareas(tareaRepository.GetAllTareas(),usuarioRepository.GetAllUsuarios(),tableroRepository.GetAllTableros());
            return View(tareas);
        } else {
            ViewListarTareas tareas = new ViewListarTareas(tareaRepository.GetAllTareas().FindAll(t => t.Id == HttpContext.Session.GetInt32("Id")),usuarioRepository.GetAllUsuarios(),tableroRepository.GetAllTableros());
            return View("TareaOperador",tareas);
        }
    }

    [HttpGet]
    public IActionResult AddTarea() {
        if (isAdmin()) {
            return View(new Tarea());
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult AddTarea(Tarea tarea) {
        if (isAdmin()) {
            tareaRepository.AddTarea(tarea);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            Tarea tarea = tareaRepository.GetTarea(id);
            return View(tarea);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                Tarea tarea = tareaRepository.GetTarea(id);
                return View("UpdateTareaOperador",tarea);
            } else {
                return RedirectToAction("Index");
            }
        }
    }

    [HttpPost]
    public IActionResult UpdateTarea(int id, Tarea tarea) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            tareaRepository.UpdateTarea(id,tarea);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                tareaRepository.UpdateTarea(id,tarea);
            }
        }
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTarea(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            tareaRepository.DeleteTarea(id);
        } else {
            if (HttpContext.Session.GetInt32("Id") == id) {
                tareaRepository.DeleteTarea(id);    
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
