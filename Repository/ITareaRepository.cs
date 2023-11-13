namespace tl2_tp10_2023_josepro752.Models;

public interface ITareaRepository {
    public void AddTarea(Tarea Tarea);
    public void UpdateTarea(int id, Tarea Tarea);
    public Tarea GetTarea(int id);
    public List<Tarea> GetAllTareas(); // Extra
    public List<Tarea> GetAllTareasForUsuario(int idUsuario);
    public List<Tarea> GetAllTareasForTablero(int idTablero);
    public void DeleteTarea(int id);
    public void AssignTarea(int idUsuario, int idTarea);
}