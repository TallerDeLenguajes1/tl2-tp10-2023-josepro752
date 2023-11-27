namespace tl2_tp10_2023_josepro752.Models;

public class ViewTareaAdd {
    public int Id {get;set;}
    public int IdTablero {get;set;}
    public string Nombre {get;set;}
    public string Descripcion {get;set;}
    public string Color {get;set;}
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