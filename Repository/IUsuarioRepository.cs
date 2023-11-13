namespace tl2_tp10_2023_josepro752.Models;

public interface IUsuarioRepository {
    public void AddUsuario(Usuario usuario);
    public void UpdateUsuario(int id, Usuario usuario);
    public List<Usuario> GetAllUsuarios();
    public Usuario GetUsuario(int id);
    public void DeleteUsuario(int id);
}