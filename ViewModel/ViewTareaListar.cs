namespace tl2_tp10_2023_josepro752.Models;

public class ViewTareaListar {
    private List<ViewTarea> vTareas;
    public List<ViewTarea> VTareas { get => vTareas; set => vTareas = value; }
    public ViewTareaListar(List<Tarea> tareas, List<Usuario> usuarios, List<Tablero> tableros) {
        vTareas = new List<ViewTarea>();
        foreach (var tarea in tareas) {
            var usuario = usuarios.FirstOrDefault(u => u.Id == tarea.IdUsuarioAsignado);
            var tablero = tableros.FirstOrDefault(t => t.Id == tarea.IdTablero);
            vTareas.Add(new ViewTarea(tarea,usuario,tablero));
        }
    }
}