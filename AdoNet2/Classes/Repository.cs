using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AdoNet2.Classes
{
    public class Repository
    {
        private string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ajouter(Livre livre)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"INSERT INTO Livre (Titre, Auteur, AnneePublication, Isbn)
                             VALUES (@titre, @auteur, @annee, @isbn)";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@titre", livre.Titre);
            cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
            cmd.Parameters.AddWithValue("@annee", livre.AnneePublication);
            cmd.Parameters.AddWithValue("@isbn", livre.Isbn);

            cmd.ExecuteNonQuery();
        }

        public List<Livre> GetAll()
        {
            List<Livre> livres = new();

            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Livre";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                livres.Add(new Livre
                {
                    Id = reader.GetInt32("Id"),
                    Titre = reader.GetString("Titre"),
                    Auteur = reader.GetString("Auteur"),
                    AnneePublication = reader.GetInt32("AnneePublication"),
                    Isbn = reader.GetString("Isbn")
                });
            }

            return livres;
        }

        public Livre? GetById(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Livre WHERE Id=@id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Livre
                {
                    Id = reader.GetInt32("Id"),
                    Titre = reader.GetString("Titre"),
                    Auteur = reader.GetString("Auteur"),
                    AnneePublication = reader.GetInt32("AnneePublication"),
                    Isbn = reader.GetString("Isbn")
                };
            }

            return null;
        }

        public bool Modifier(Livre livre)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE Livre 
                             SET Titre=@titre, Auteur=@auteur,
                                 AnneePublication=@annee, Isbn=@isbn
                             WHERE Id=@id";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@titre", livre.Titre);
            cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
            cmd.Parameters.AddWithValue("@annee", livre.AnneePublication);
            cmd.Parameters.AddWithValue("@isbn", livre.Isbn);
            cmd.Parameters.AddWithValue("@id", livre.Id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Supprimer(int id)
        {
            using MySqlConnection connection = new MySqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM Livre WHERE Id=@id";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
