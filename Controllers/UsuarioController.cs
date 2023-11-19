using System.Diagnostics;
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
        var usuarios = usuarioRepository.GetAllUsuarios();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult AddUsuario() {
        return View(new Usuario());
    }
    
    [HttpPost]
    public IActionResult AddUsuario(Usuario usuario) {
        usuarioRepository.AddUsuario(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult UpdateUsuario(int id) {
        var usuario = usuarioRepository.GetUsuario(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult UpdateUsuario(int id, Usuario usuario) {
        usuarioRepository.UpdateUsuario(id,usuario);
        return RedirectToAction("Index");
    }

    public IActionResult DeleteUsuario(int id) {
        usuarioRepository.DeleteUsuario(id);
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
