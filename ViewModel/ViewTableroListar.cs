using System.ComponentModel.DataAnnotations;
namespace tl2_tp10_2023_josepro752.Models;

public class ViewTableroListar {
    private List<ViewTablero> viewTableros;
    private ViewUsuarioListar viewUsuarioListar;
    public ViewUsuarioListar ViewUsuarioListar { get => viewUsuarioListar; set => viewUsuarioListar = value; }
    public List<ViewTablero> ViewTableros { get => viewTableros; set => viewTableros = value; }

    public ViewTableroListar(List<Tablero> tableros, List<Usuario> usuarios)
    {
        viewTableros = new List<ViewTablero>();
        foreach (var t in tableros) {
            foreach (var u in usuarios) {
                if (t.IdUsuarioPropietario == u.Id) {
                    var viewTablero = new ViewTablero(t,u);
                    viewTableros.Add(viewTablero);
                }
            }
        }
    }
}