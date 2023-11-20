namespace tl2_tp10_2023_josepro752.Models;

public class Usuario {
    private int id;
    private string nombreDeUsuario;
    private string contrasenia;
    private string rol;
    public int Id { get => id; set => id = value; }
    public string NombreDeUsuario { get => nombreDeUsuario; set => nombreDeUsuario = value; }
    public string Contrasenia { get => contrasenia; set => contrasenia = value; }
    public string Rol { get => rol; set => rol = value; }
}
