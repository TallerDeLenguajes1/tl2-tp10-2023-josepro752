using System.Diagnostics;
using System.Formats.Tar;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository _tareaRepository;
    private ITableroRepository _tableroRepository;
    private IUsuarioRepository _usuarioRepository;

    public TareaController(ILogger<TareaController> logger, ITareaRepository tareaRepository, ITableroRepository tableroRepository, IUsuarioRepository usuarioRepository)
    {
        _logger = logger;
        _tareaRepository = tareaRepository;
        _tableroRepository = tableroRepository;
        _usuarioRepository = usuarioRepository;
    }

    public IActionResult Index()
    {
        try {
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if (isAdmin()) {
                ViewTareaListar tareas = new ViewTareaListar(_tareaRepository.GetAllTareas(),_usuarioRepository.GetAllUsuarios(),_tableroRepository.GetAllTableros());
                return View(tareas);
            } else {
                ViewTareaListar tareas = new ViewTareaListar(_tareaRepository.GetAllTareas().FindAll(t => t.IdUsuarioAsignado == HttpContext.Session.GetInt32("Id")),_usuarioRepository.GetAllUsuarios(),_tableroRepository.GetAllTableros());
                return View("IndexOperador",tareas);
            }
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult AddTarea() {
        try {
            if (isAdmin()) {
                return View(new ViewTareaAdd());
            }
            return RedirectToRoute(new {controller = "Login", action = "Index"});
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult AddTarea(ViewTareaAdd viewTareaAdd) {
        try {
            if(!ModelState.IsValid) {
                if (isAdmin()) {
                    var tarea = new Tarea(viewTareaAdd);
                    _tareaRepository.AddTarea(tarea);
                    return RedirectToAction("Index");
                }
                return RedirectToRoute(new {controller = "Login", action = "Index"});
            }
            return RedirectToAction("AddTarea");
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id) {
        try {
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if (isAdmin()) {
                ViewTareaUpdate viewTareaUpdate = new ViewTareaUpdate(_tareaRepository.GetTarea(id),_tableroRepository.GetAllTableros(),_usuarioRepository.GetAllUsuarios());
                return View(viewTareaUpdate);
            } else {
                var tarea = _tareaRepository.GetTarea(id);
                if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado) {
                    ViewTareaUpdate viewTareaUpdate = new ViewTareaUpdate(_tareaRepository.GetTarea(id),_tableroRepository.GetAllTableros(),_usuarioRepository.GetAllUsuarios());
                    return View("UpdateTareaOperador",viewTareaUpdate);
                } else {
                    return RedirectToAction("Index");
                }
            }
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    [HttpPost]
    public IActionResult UpdateTarea(int id, ViewTareaUpdate viewTareaUpdate) {
        try {
            if (ModelState.IsValid) {
                if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
                if (isAdmin()) {
                    Tarea tarea = new Tarea(viewTareaUpdate);
                    _tareaRepository.UpdateTarea(id,tarea);
                } else {
                    var tarea = _tareaRepository.GetTarea(id);
                    if (HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado) {
                        tarea = new Tarea(viewTareaUpdate);
                        _tareaRepository.UpdateTarea(id,tarea);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("UpdateTarea");
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
    }

    public IActionResult DeleteTarea(int id) {
        try {
            if (HttpContext.Session.GetString("Rol") == null) return RedirectToRoute(new {controller = "Login", action = "Index"});
            if (isAdmin()) {
                _tareaRepository.DeleteTarea(id);
            } else {
                var tarea = _tareaRepository.GetTarea(id);
                if (tarea != null && HttpContext.Session.GetInt32("Id") == tarea.IdUsuarioAsignado) {
                    _tareaRepository.DeleteTarea(id);    
                }
            }
            return RedirectToAction("Index");
        } catch (Exception ex) {
            _logger.LogError(ex.ToString());
            return RedirectToAction("Error");
        }
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
