namespace tl2_tp10_2023_josepro752.Models;

public class Tablero {
    private int id;
    private int idUsuarioPropietario;
    private string nombre;
    private string descripcion;
    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public Tablero() {}
    public Tablero(ViewTableroAdd viewTableroAdd) {
        id = viewTableroAdd.Id;
        idUsuarioPropietario = viewTableroAdd.IdUsuarioPropietario;
        nombre = viewTableroAdd.Nombre;
        descripcion = viewTableroAdd.Descripcion;
    }
    public Tablero(ViewTableroUpdate viewTableroUpdate, Usuario u) {
        id = viewTableroUpdate.Id;
        idUsuarioPropietario = viewTableroUpdate.IdUsuarioPropietario;
        nombre = viewTableroUpdate.Nombre;
        descripcion = viewTableroUpdate.Descripcion;
    }
}
