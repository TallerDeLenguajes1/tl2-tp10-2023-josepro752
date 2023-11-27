using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewTableroAdd { //Quito una capa de abstraccion
    public int Id {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public int IdUsuarioPropietario {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Nombre {get;set;}
    [StringLength(300)]
    public string Descripcion {get;set;}
    public List<Usuario> Usuarios {get;set;}
    public ViewTableroAdd(List<Usuario> usuarios)
    {
        Usuarios = usuarios;
    }
    public ViewTableroAdd () {}
}