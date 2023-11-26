namespace tl2_tp10_2023_josepro752.Models;

public class ViewTableroAdd { //Quito una capa de abstraccion
    public int Id {get;set;}
    public int IdUsuarioPropietario {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public List<Usuario> Usuarios {get;set;}
    public ViewTableroAdd(List<Usuario> usuarios)
    {
        Usuarios = usuarios;
    }
    public ViewTableroAdd () {}
}