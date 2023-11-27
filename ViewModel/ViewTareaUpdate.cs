namespace tl2_tp10_2023_josepro752.Models;

public class ViewTareaUpdate {
    public int Id {get;set;}
    public int IdTablero {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public string Color {get;set;}
    public EstadoTarea Estado {get;set;}
    public int IdUsuarioAsignado {get;set;}
    public List<Tablero> vTableros {get;set;}
    public List<Usuario> vUsuarios {get;set;}
    public ViewTareaUpdate() {}

    public ViewTareaUpdate(Tarea tarea, List<Tablero> tableros, List<Usuario> usuarios)
    {
        Id = tarea.Id;
        IdTablero = tarea.IdTablero;
        Nombre = tarea.Nombre;
        Descripcion = tarea.Descripcion;
        Color = tarea.Color;
        Estado = tarea.Estado;
        IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        vTableros = tableros;
        vUsuarios = usuarios;
    }
}