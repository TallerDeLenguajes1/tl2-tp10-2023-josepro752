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
            ViewTareaListar tareas = new ViewTareaListar(tareaRepository.GetAllTareas(),usuarioRepository.GetAllUsuarios(),tableroRepository.GetAllTableros());
            return View(tareas);
        } else {
            ViewTareaListar tareas = new ViewTareaListar(tareaRepository.GetAllTareas().FindAll(t => t.IdUsuarioAsignado == HttpContext.Session.GetInt32("Id")),usuarioRepository.GetAllUsuarios(),tableroRepository.GetAllTableros());
            return View("IndexOperador",tareas);
        }
    }

    [HttpGet]
    public IActionResult AddTarea() {
        if (isAdmin()) {
            return View(new ViewTareaAdd());
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpPost]
    public IActionResult AddTarea(ViewTareaAdd viewTareaAdd) {
        if (isAdmin()) {
            var tarea = new Tarea(viewTareaAdd);
            tareaRepository.AddTarea(tarea);
            return RedirectToAction("Index");
        }
        return RedirectToRoute(new {controller = "Login", action = "Index"});
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            ViewTareaUpdate viewTareaUpdate = new ViewTareaUpdate(tareaRepository.GetTarea(id),tableroRepository.GetAllTableros(),usuarioRepository.GetAllUsuarios());
            return View(viewTareaUpdate);
        } else {
            var tarea = tareaRepository.GetTarea(id);
            if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado) {
                ViewTareaUpdate viewTareaUpdate = new ViewTareaUpdate(tareaRepository.GetTarea(id),tableroRepository.GetAllTableros(),usuarioRepository.GetAllUsuarios());
                return View("UpdateTareaOperador",viewTareaUpdate);
            } else {
                return RedirectToAction("Index");
            }
        }
    }

    [HttpPost]
    public IActionResult UpdateTarea(int id, ViewTareaUpdate viewTareaUpdate) {
        if (ModelState.IsValid) {
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if (isAdmin()) {
                Tarea tarea = new Tarea(viewTareaUpdate);
                tareaRepository.UpdateTarea(id,tarea);
            } else {
                var tarea = tareaRepository.GetTarea(id);
                if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado) {
                    tarea = new Tarea(viewTareaUpdate);
                    tareaRepository.UpdateTarea(id,tarea);
                }
            }
            return RedirectToAction("Index");
        }
        return RedirectToAction("UpdateTarea");
    }

    public IActionResult DeleteTarea(int id) {
        if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
        if (isAdmin()) {
            tareaRepository.DeleteTarea(id);
        } else {
            var tarea = tareaRepository.GetTarea(id);
            if (tarea != null && HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado) {
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
