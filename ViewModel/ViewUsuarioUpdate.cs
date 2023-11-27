using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewUsuarioUpdate {
    private int id;
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    private string nombreDeUsuario;
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(50)]
    private string contrasenia;
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(20)]
    private string rol;
    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
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