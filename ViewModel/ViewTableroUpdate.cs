using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewTableroUpdate { //Quito una capa de abstraccion
    public int Id {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public int IdUsuarioPropietario {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Nombre {get;set;}
    [StringLength(300)]
    public string Descripcion {get;set;}
    public List<Usuario> Usuarios {get;set;}
    public ViewTableroUpdate(Tablero tablero, List<Usuario> usuarios)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
        Usuarios = usuarios;
    }
    public ViewTableroUpdate () {}
}