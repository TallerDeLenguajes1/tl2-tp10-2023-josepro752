using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class UsuarioLogin {
    private string usuario;
    private string contrasenia;
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    public string Usuario { get => usuario; set => usuario = value; }
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(20)]
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
}