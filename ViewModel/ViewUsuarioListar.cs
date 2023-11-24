namespace tl2_tp10_2023_josepro752.Models;

public class ViewUsuarioListar {
    private List<ViewUsuario> viewUsuarios;
    public List<ViewUsuario> ViewUsuarios { get => viewUsuarios; set => viewUsuarios = value; }
    public ViewUsuarioListar(List<Usuario> usuarios)
    {
        ViewUsuarios = new List<ViewUsuario>();
        foreach(var u in usuarios) {
            var viewUsuario = new ViewUsuario(u);
            viewUsuarios.Add(viewUsuario);
        }
    }
}