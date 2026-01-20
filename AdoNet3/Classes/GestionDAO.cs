using MySql.Data.MySqlClient;
using AdoNet3.Classes;
using System;
using System.Collections.Generic;

namespace AdoNet3.DAO
{
    public class GestionDAO
    {
        private  string connectionString;

        public GestionDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // ----------- CLIENTS -----------
        public void AjouterClient(Client client)
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = @"INSERT INTO Client (Nom,Prenom,Adresse,CodePostal,Ville,Telephone)
                             VALUES (@Nom,@Prenom,@Adresse,@CP,@Ville,@Tel)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nom", client.Nom);
            cmd.Parameters.AddWithValue("@Prenom", client.Prenom);
            cmd.Parameters.AddWithValue("@Adresse", client.Adresse);
            cmd.Parameters.AddWithValue("@CP", client.CodePostal);
            cmd.Parameters.AddWithValue("@Ville", client.Ville);
            cmd.Parameters.AddWithValue("@Tel", client.Telephone);
            cmd.ExecuteNonQuery();
        }

        public List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT * FROM Client";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Nom = reader["Nom"].ToString(),
                    Prenom = reader["Prenom"].ToString(),
                    Adresse = reader["Adresse"].ToString(),
                    CodePostal = reader["CodePostal"].ToString(),
                    Ville = reader["Ville"].ToString(),
                    Telephone = reader["Telephone"].ToString()
                });
            }
            return clients;
        }

        public Client GetClientById(int id)
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT * FROM Client WHERE ID=@id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            using MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Client
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Nom = reader["Nom"].ToString(),
                    Prenom = reader["Prenom"].ToString(),
                    Adresse = reader["Adresse"].ToString(),
                    CodePostal = reader["CodePostal"].ToString(),
                    Ville = reader["Ville"].ToString(),
                    Telephone = reader["Telephone"].ToString()
                };
            }
            return null;
        }

        public void ModifierClient(Client client)
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = @"UPDATE Client 
                             SET Nom=@Nom,Prenom=@Prenom,Adresse=@Adresse,CodePostal=@CP,Ville=@Ville,Telephone=@Tel 
                             WHERE ID=@ID";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nom", client.Nom);
            cmd.Parameters.AddWithValue("@Prenom", client.Prenom);
            cmd.Parameters.AddWithValue("@Adresse", client.Adresse);
            cmd.Parameters.AddWithValue("@CP", client.CodePostal);
            cmd.Parameters.AddWithValue("@Ville", client.Ville);
            cmd.Parameters.AddWithValue("@Tel", client.Telephone);
            cmd.Parameters.AddWithValue("@ID", client.ID);
            cmd.ExecuteNonQuery();
        }

        public void SupprimerClient(int clientId)
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            // Supprimer commandes associées
            string delCommandes = "DELETE FROM Commande WHERE ClientID=@id";
            MySqlCommand cmd1 = new MySqlCommand(delCommandes, conn);
            cmd1.Parameters.AddWithValue("@id", clientId);
            cmd1.ExecuteNonQuery();
            // Supprimer client
            string delClient = "DELETE FROM Client WHERE ID=@id";
            MySqlCommand cmd2 = new MySqlCommand(delClient, conn);
            cmd2.Parameters.AddWithValue("@id", clientId);
            cmd2.ExecuteNonQuery();
        }

        // ----------- COMMANDES -----------
        public void AjouterCommande(Commande commande)
        {
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = "INSERT INTO Commande (ClientID, DateCommande, Total) VALUES (@ClientID,@Date,@Total)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ClientID", commande.ClientID);
            cmd.Parameters.AddWithValue("@Date", commande.DateCommande);
            cmd.Parameters.AddWithValue("@Total", commande.Total);
            cmd.ExecuteNonQuery();
        }

        public List<Commande> GetCommandesByClient(int clientId)
        {
            List<Commande> commandes = new List<Commande>();
            using MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string query = "SELECT * FROM Commande WHERE ClientID=@id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", clientId);
            using MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                commandes.Add(new Commande
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    ClientID = Convert.ToInt32(reader["ClientID"]),
                    DateCommande = Convert.ToDateTime(reader["DateCommande"]),
                    Total = Convert.ToDecimal(reader["Total"])
                });
            }
            return commandes;
        }
    }
}
