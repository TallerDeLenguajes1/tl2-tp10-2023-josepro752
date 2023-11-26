namespace tl2_tp10_2023_josepro752.Models;

public class ViewTableroUpdate { //Quito una capa de abstraccion
    public int Id {get;set;}
    public int IdUsuarioPropietario {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public ViewTableroUpdate(Tablero tablero, Usuario usuario)
    {
        Id = tablero.Id;
        IdUsuarioPropietario = tablero.IdUsuarioPropietario;
        Nombre = tablero.Nombre;
        Descripcion = tablero.Descripcion;
    }
    public ViewTableroUpdate () {}
}