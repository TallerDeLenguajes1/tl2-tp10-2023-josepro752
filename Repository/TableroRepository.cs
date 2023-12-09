using System.Data.SQLite;

namespace tl2_tp10_2023_josepro752.Models;

public class TableroRepository : ITableroRepository {
    private string cadenaDeConexion;

    public TableroRepository(string cadenaDeConexion)
    {
        this.cadenaDeConexion = cadenaDeConexion;
    }

    public void AddTablero(Tablero tablero) {
        var query = @"INSERT INTO Tablero (id_usuario_propietario,nombre,descripcion) VALUES (@id_usuario_propietario,@nombre,@descripcion);"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            var command = new SQLiteCommand(query,connection); // Crea el comando que se ejecutara en la base de datos
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario",tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre",tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion",tablero.Descripcion));
            connection.Open();
            command.ExecuteNonQuery(); // Se usa ExecuteNonQuery, cuando es una modificacion (ALTA, BAJA, ACTUALIZACION)
            connection.Close();
        }
    }
    public void UpdateTablero(int id, Tablero tablero) {
        var query = @"UPDATE Tablero SET id_usuario_propietario = @id_usuario_propietario, nombre = @nombre, descripcion = @descripcion WHERE id=@id;"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            var command = new SQLiteCommand(query,connection); // Crea el comando que se ejecutara en la base de datos
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario",tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre",tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion",tablero.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery(); // Se usa ExecuteNonQuery, cuando es una modificacion (ALTA, BAJA, ACTUALIZACION)
            connection.Close();
        }
    }
    public Tablero GetTablero(int id) {
        var tablero = GetAllTableros().FirstOrDefault(tablero => tablero.Id == id);
        if (tablero==null) {
            throw new Exception("Tablero no creado.");
        }
        return tablero;
    }
    public List<Tablero> GetAllTableros() {
        List<Tablero> tableros = new List<Tablero>();
        var query = @"SELECT * FROM Tablero;"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            using (SQLiteCommand command = new SQLiteCommand(query,connection)) {
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
        }
        if (tableros==null) {
            throw new Exception("Tableros no creados.");
        }
        return tableros;
    }
    public List<Tablero> GetAllTablerosForUsuario(int idUsuario) {
        var tableros = GetAllTableros().FindAll(tablero => tablero.IdUsuarioPropietario == idUsuario);
        if (tableros==null) {
            throw new Exception("Tableros no creados.");
        }
        return tableros;
    }
    public void DeleteTablero(int id) {
        var query = @"DELETE FROM Tablero WHERE id=@id;"; // Esto se ejecutara en la base de datos
        using (SQLiteConnection connection = new SQLiteConnection(cadenaDeConexion)){ // Me crea la conexion
            var command = new SQLiteCommand(query,connection); // Crea el comando que se ejecutara en la base de datos
            command.Parameters.Add(new SQLiteParameter("@id",id));
            connection.Open();
            command.ExecuteNonQuery(); // Se usa ExecuteNonQuery, cuando es una modificacion (ALTA, BAJA, ACTUALIZACION)
            connection.Close();
        }   
    }
}