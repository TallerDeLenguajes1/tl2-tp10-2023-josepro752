namespace tl2_tp10_2023_josepro752.Models;

public enum EstadoTarea {
    Ideas,
    ToDo,
    Doing,
    Review,
    Done
}

public class Tarea {
    private int id;
    private int idTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private int idUsuarioAsignado;
    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public int IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public Tarea () {}
    public Tarea (ViewTareaAdd viewTareaAdd) {
        id = viewTareaAdd.Id;
        idTablero = viewTareaAdd.IdTablero;
        nombre = viewTareaAdd.Nombre;
        descripcion = viewTareaAdd.Descripcion;
        color = viewTareaAdd.Color;
        estado = viewTareaAdd.Estado;
        idUsuarioAsignado = viewTareaAdd.IdUsuarioAsignado;
    }
    public Tarea (ViewTareaUpdate viewTareaUpdate) {
        id = viewTareaUpdate.Id;
        idTablero = viewTareaUpdate.IdTablero;
        nombre = viewTareaUpdate.Nombre;
        descripcion = viewTareaUpdate.Descripcion;
        color = viewTareaUpdate.Color;
        estado = viewTareaUpdate.Estado;
        idUsuarioAsignado = viewTareaUpdate.IdUsuarioAsignado;
    }
}
