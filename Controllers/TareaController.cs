using System.Diagnostics;
using System.Formats.Tar;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_josepro752.Models;

namespace tl2_tp10_2023_josepro752.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private ITareaRepository tareaRepository;

    public TareaController(ILogger<TareaController> logger)
    {
        _logger = logger;
        tareaRepository = new TareaRepository();
    }

    public IActionResult Index()
    {
        var tareas = tareaRepository.GetAllTareas();
        return View(tareas);
    }

    [HttpGet]
    public IActionResult AddTarea() {
        return View(new Tarea());
    }

    [HttpPost]
    public IActionResult AddTarea(Tarea tarea) {
        tareaRepository.AddTarea(tarea);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateTarea(int id) {
        var tarea = tareaRepository.GetTarea(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult UpdateTarea(int id, Tarea tarea) {
        tareaRepository.UpdateTarea(id,tarea);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTarea(int id) {
        tareaRepository.DeleteTarea(id);
        return RedirectToAction("Index");
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
