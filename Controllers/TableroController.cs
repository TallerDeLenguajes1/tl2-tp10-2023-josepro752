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
        var tableros = tableroRepository.GetAllTableros();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult AddTablero() {
        return View(new Tablero ());
    }

    [HttpPost]
    public IActionResult AddTablero(Tablero tablero) {
        tableroRepository.AddTablero(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateTablero(int id) {
        var tablero = tableroRepository.GetTablero(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult UpdateTablero(int id, Tablero tablero) {
        tableroRepository.UpdateTablero(id,tablero);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteTablero(int id) {
        tableroRepository.DeleteTablero(id);
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
