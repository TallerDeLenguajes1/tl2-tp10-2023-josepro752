using System.Data.SQLite;

namespace tl2_tp10_2023_josepro752.Models;

public class UsuarioRepository : IUsuarioRepository{

    private string[] roles = {
        "Administrador","Operador"
    };
    private string cadenaDeConexion = "Data Source=DataBase/kanban.db;Cache=Shared";
    public void AddUsuario(Usuario usuario) {
        var query = @"INSERT INTO Usuario (nombre_de_usuario,rol,contrasenia) VALUES (@nombre_de_usuario,@rol,@contrasenia);"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            var command = new SQLiteCommand(query,connection); // Crea el comando que se ejecutara en la base de datos
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario",usuario.NombreDeUsuario));
            if (usuario.Rol == roles.FirstOrDefault(rol => rol == usuario.Rol)) {
                command.Parameters.Add(new SQLiteParameter("@rol",usuario.Rol));
            } else {
                command.Parameters.Add(new SQLiteParameter("@rol","Operador"));
            }
            command.Parameters.Add(new SQLiteParameter("@contrasenia",usuario.Contrasenia));
            connection.Open();
            command.ExecuteNonQuery(); // Se usa ExecuteNonQuery, cuando es una modificacion (ALTA, BAJA, ACTUALIZACION)
            connection.Close();
        }
    }
    public void UpdateUsuario(int id, Usuario usuario) {
        var query = @"UPDATE Usuario SET nombre_de_usuario = @nombre_de_usuario, rol = @rol, contrasenia = @contrasenia WHERE id=@id;"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            var command = new SQLiteCommand(query,connection); // Crea el comando que se ejecutara en la base de datos
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario",usuario.NombreDeUsuario));
            if (usuario.Rol == roles.FirstOrDefault(rol => rol == usuario.Rol)) {
                command.Parameters.Add(new SQLiteParameter("@rol",usuario.Rol));
            } else {
                command.Parameters.Add(new SQLiteParameter("@rol","Operador"));
            }
            command.Parameters.Add(new SQLiteParameter("@contrasenia",usuario.Contrasenia));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery(); // Se usa ExecuteNonQuery, cuando es una modificacion (ALTA, BAJA, ACTUALIZACION)
            connection.Close();
        }
    }
    public List<Usuario> GetAllUsuarios() {
        List<Usuario> usuarios = new List<Usuario>();
        var query = @"SELECT * FROM Usuario;"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            using (SQLiteCommand command = new SQLiteCommand(query,connection)) {
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var usuario = new Usuario();
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString(); // Lo que va entre corchetes en el Reader es como se llama el campo en la base de datos
                        usuario.Contrasenia = reader["contrasenia"].ToString(); // Lo que va entre corchetes en el Reader es como se llama el campo en la base de datos
                        usuario.Rol = reader["rol"].ToString(); // Lo que va entre corchetes en el Reader es como se llama el campo en la base de datos
                        usuario.Id = Convert.ToInt32(reader["id"]);
                        usuarios.Add(usuario);
                    }
                }
                connection.Close();
            }
        }
        return usuarios;
    }
    public Usuario GetUsuario(int id) {
        Usuario usuario = GetAllUsuarios().FirstOrDefault(usuario => usuario.Id==id);
        return usuario;
    }
    public void DeleteUsuario(int id) {
        var query = @"DELETE FROM Usuario WHERE id=@id;"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            var command = new SQLiteCommand(query,connection); // Crea el comando que se ejecutara en la base de datos
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery(); // Se usa ExecuteNonQuery, cuando es una modificacion (ALTA, BAJA, ACTUALIZACION)
            connection.Close();
        }   
    }
}