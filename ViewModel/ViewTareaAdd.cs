using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewTareaAdd {
    public int Id {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public int IdTablero {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Nombre {get;set;}
    [StringLength(2000)]
    public string Descripcion {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Color {get;set;}
    [Required (ErrorMessage ="este campo es requerido")]
    public EstadoTarea Estado {get;set;}
    public int IdUsuarioAsignado {get;set;}
    public List<Tablero> vTableros {get;set;}
    public List<Usuario> vUsuarios {get;set;}
    public ViewTareaAdd() {}
    public ViewTareaAdd(List<Usuario> usuarios, List<Tablero> tableros) {
        vTableros = tableros;
        vUsuarios = usuarios;
    }
}