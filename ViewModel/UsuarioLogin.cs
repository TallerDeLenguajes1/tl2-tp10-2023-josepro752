using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class UsuarioLogin {
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(100)]
    private string usuario;
    [Required (ErrorMessage ="este campo es requerido")]
    [StringLength(20)]
    private string contrasenia;
    public string Usuario { get => usuario; set => usuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
}