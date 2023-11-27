namespace tl2_tp10_2023_josepro752.Models;

public class ViewTablero { //Quito una capa de abstraccion

    public int Id {get;set;}
    public string UsuarioPropietario {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public ViewTablero(Tablero tablero, Usuario usuario)
    {
        Id = tablero.Id;
        UsuarioPropietario = usuario.NombreDeUsuario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
}
