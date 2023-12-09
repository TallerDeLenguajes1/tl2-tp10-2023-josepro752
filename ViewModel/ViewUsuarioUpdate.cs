using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewUsuarioUpdate {
    private int id;
    private string nombreDeUsuario;
    private string contrasenia;
    private string rol;
    public int Id { get => id; set => id = value; }
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(50)]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(20)]
    public string Rol { get => rol; set => rol = value; }
    public ViewUsuarioUpdate(Usuario usuario)
    {
        id = usuario.Id;
        nombreDeUsuario = usuario.NombreDeUsuario;
        contrasenia = usuario.Contrasenia;
        rol = usuario.Rol;
    }
    public ViewUsuarioUpdate() {}
}