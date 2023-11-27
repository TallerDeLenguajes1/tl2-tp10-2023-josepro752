using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewTarea {
    private int id;
    private string nombreTablero;
    private string nombre;
    private string descripcion;
    private string color;
    private EstadoTarea estado;
    private string usuarioAsignado;
    public int Id { get => id; set => id = value; }
    public string NombreTablero { get => nombreTablero; set => nombreTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }
    public string UsuarioAsignado { get => usuarioAsignado; set => usuarioAsignado = value; }
    public ViewTarea(Tarea tarea, Usuario usuario, Tablero tablero) {
        this.id = tarea.Id;
        if (tablero == null) {
            this.nombreTablero = "Sin Tablero";
        } else {
            this.nombreTablero = tablero.Nombre;
        }
        this.nombre = tarea.Nombre;
        this.descripcion = tarea.Descripcion;
        this.color = tarea.Color;
        this.estado = tarea.Estado;
        if (tablero == null) {
            this.usuarioAsignado = "Sin Asignar";
        } else {
            this.usuarioAsignado = usuario.NombreDeUsuario;
        }
    }
}