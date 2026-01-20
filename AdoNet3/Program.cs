/*
Sujet
Vous devez creer une application de gestion des commandes qui permet de visualiser et de modifier les informations des commandes et des clients. La base de donnees contient deux tables :

Table Client :

ID : identifiant unique du client (entier)
Nom : nom du client (chaine de caracteres)
Prenom : prenom du client (chaine de caracteres)
Adresse : adresse du client (chaine de caracteres)
Code postal : code postal du client (chaine de caracteres)
Ville : ville du client (chaine de caracteres)
Telephone : numero de telephone du client (chaine de caracteres)
Table Commandes :

ID : identifiant unique de la commande (entier)
Client ID : identifiant du client associee a la commande (entier)
Date : date de la commande (date)
Total : montant total de la commande (nombre decimal)
Creer une methode qui permet d'afficher tous les clients

Creer une methode qui permet d'ajouter un client
Creer une methode qui permet d'editer un client
Creer une methode qui permet de supprimer un client (et ses commandes)
Creer une methode pour afficher le detail d'un client avec ses commandes
Creer une methode qui permet d'ajouter une commande a un client
Creer une Interface Homme Machine pour tester votre application

   ____                                          _
  / ___|___  _ __ ___  _ __ ___   __ _ _ __   __| | ___  ___
 | |   / _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` |/ _ \/ __|
 | |__| (_) | | | | | | | | | | | (_| | | | | (_| |  __/\__ \
  \____\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_|\___||___/

1. Afficher les clients
2. Creer un client
3. Modifier un client
4. Supprimer un client
5. Afficher les details d'un client
6. Ajouter une commande
7. Modifier une commande
8. Supprimer une commande
0. Quitter

*/

using MySql.Data.MySqlClient;
using AdoNet3.Classes;
using System;

void AfficherTitre()
{
    string title = @"
   ____                                          _           
  / ___|___  _ __ ___  _ __ ___   __ _ _ __   __| | ___  ___ 
 | |   / _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` |/ _ \/ __|
 | |__| (_) | | | | | | | | | | | (_| | | | | (_| |  __/\__ \
  \____\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_|\___||___/
";
    Console.WriteLine(title);
}

AfficherTitre();

// Chaîne de connexion
string connectionString = "Server=localhost;Database=GestionCommandes;User ID=root;Password=root";

// ===================== CLIENT =====================
void AjouterClient()
{
    try
    {
        Console.WriteLine("\nAjout d'un client");

        Console.Write("Nom : "); string nom = Console.ReadLine();
        Console.Write("Prenom : "); string prenom = Console.ReadLine();
        Console.Write("Adresse : "); string adresse = Console.ReadLine();
        Console.Write("Code Postal : "); string cp = Console.ReadLine();
        Console.Write("Ville : "); string ville = Console.ReadLine();
        Console.Write("Telephone : "); string tel = Console.ReadLine();

        Client client = new Client(nom, prenom, adresse, cp, ville, tel);

        using MySqlConnection connexion = new MySqlConnection(connectionString);
        connexion.Open();

        string query = @"INSERT INTO Client (Nom,Prenom,Adresse,CodePostal,Ville,Telephone)
                         VALUES (@Nom,@Prenom,@Adresse,@CP,@Ville,@Tel)";

        MySqlCommand cmd = new MySqlCommand(query, connexion);
        cmd.Parameters.AddWithValue("@Nom", client.Nom);
        cmd.Parameters.AddWithValue("@Prenom", client.Prenom);
        cmd.Parameters.AddWithValue("@Adresse", client.Adresse);
        cmd.Parameters.AddWithValue("@CP", client.CodePostal);
        cmd.Parameters.AddWithValue("@Ville", client.Ville);
        cmd.Parameters.AddWithValue("@Tel", client.Telephone);

        cmd.ExecuteNonQuery();
        Console.WriteLine("Client ajouté avec succès");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}

void AfficherClients()
{
    try
    {
        using MySqlConnection connexion = new MySqlConnection(connectionString);
        connexion.Open();

        string query = "SELECT * FROM Client";
        MySqlCommand cmd = new MySqlCommand(query, connexion);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("\nListe des clients :");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["ID"]} | {reader["Nom"]} {reader["Prenom"]} | {reader["Adresse"]} | {reader["Ville"]} | {reader["Telephone"]}");
        }
        reader.Close();
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}

void ConsulterClient()
{
    try
    {
        Console.Write("\nId du client : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        using MySqlConnection connexion = new MySqlConnection(connectionString);
        connexion.Open();


        string queryClient = "SELECT * FROM Client WHERE ID=@id";
        MySqlCommand cmd = new MySqlCommand(queryClient, connexion);
        cmd.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"Nom : {reader["Nom"]}");
            Console.WriteLine($"Prenom : {reader["Prenom"]}");
            Console.WriteLine($"Adresse : {reader["Adresse"]}");
            Console.WriteLine($"Code Postal : {reader["CodePostal"]}");
            Console.WriteLine($"Ville : {reader["Ville"]}");
            Console.WriteLine($"Telephone : {reader["Telephone"]}");
        }
        else
        {
            Console.WriteLine("Client introuvable");
        }
        reader.Close();


        string queryCommandes = "SELECT * FROM Commande WHERE ClientID=@id";
        MySqlCommand cmd2 = new MySqlCommand(queryCommandes, connexion);
        cmd2.Parameters.AddWithValue("@id", id);
        MySqlDataReader reader2 = cmd2.ExecuteReader();

        Console.WriteLine("Commandes :");
        if (!reader2.HasRows)
        {
            Console.WriteLine("Aucune commande pour ce client");
        }
        else
        {
            while (reader2.Read())
            {
                Console.WriteLine($"{reader2["ID"]} | {Convert.ToDateTime(reader2["DateCommande"]).ToShortDateString()} | {reader2["Total"]} Thunasse");
            }
            reader2.Close();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}

void ModifierClient()
{
    try
    {
        Console.Write("\nId du client à modifier : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        Console.Write("Nom : "); string nom = Console.ReadLine();
        Console.Write("Prenom : "); string prenom = Console.ReadLine();
        Console.Write("Adresse : "); string adresse = Console.ReadLine();
        Console.Write("Code Postal : "); string cp = Console.ReadLine();
        Console.Write("Ville : "); string ville = Console.ReadLine();
        Console.Write("Telephone : "); string tel = Console.ReadLine();

        using MySqlConnection connexion = new MySqlConnection(connectionString);
        connexion.Open();

        string query = @"UPDATE Client 
                         SET Nom=@Nom,Prenom=@Prenom,Adresse=@Adresse,CodePostal=@CP,Ville=@Ville,Telephone=@Tel 
                         WHERE ID=@ID";

        MySqlCommand cmd = new MySqlCommand(query, connexion);
        cmd.Parameters.AddWithValue("@Nom", nom);
        cmd.Parameters.AddWithValue("@Prenom", prenom);
        cmd.Parameters.AddWithValue("@Adresse", adresse);
        cmd.Parameters.AddWithValue("@CP", cp);
        cmd.Parameters.AddWithValue("@Ville", ville);
        cmd.Parameters.AddWithValue("@Tel", tel);
        cmd.Parameters.AddWithValue("@ID", id);

        int rows = cmd.ExecuteNonQuery();
        Console.WriteLine(rows > 0 ? "Client modifié" : "Client introuvable");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}

void SupprimerClient()
{
    try
    {
        Console.Write("\nId du client à supprimer : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        using MySqlConnection connexion = new MySqlConnection(connectionString);
        connexion.Open();

      
        string queryCommandes = "DELETE FROM Commande WHERE ClientID=@id";
        MySqlCommand cmd1 = new MySqlCommand(queryCommandes, connexion);
        cmd1.Parameters.AddWithValue("@id", id);
        cmd1.ExecuteNonQuery();

     
        string queryClient = "DELETE FROM Client WHERE ID=@id";
        MySqlCommand cmd2 = new MySqlCommand(queryClient, connexion);
        cmd2.Parameters.AddWithValue("@id", id);
        int rows = cmd2.ExecuteNonQuery();

        Console.WriteLine(rows > 0 ? "Client et commandes supprimés" : "Client introuvable");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}

// ===================== COMMANDES =====================
void AjouterCommande()
{
    try
    {
        Console.Write("\nId du client : ");
        if (!int.TryParse(Console.ReadLine(), out int clientId))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        Console.Write("Date (yyyy-MM-dd) : ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
        {
            Console.WriteLine("Date invalide");
            return;
        }

        Console.Write("Total : ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal total))
        {
            Console.WriteLine("Total invalide");
            return;
        }

        using MySqlConnection connexion = new MySqlConnection(connectionString);
        connexion.Open();

        string query = "INSERT INTO Commande (ClientID, DateCommande, Total) VALUES (@ClientID,@Date,@Total)";
        MySqlCommand cmd = new MySqlCommand(query, connexion);
        cmd.Parameters.AddWithValue("@ClientID", clientId);
        cmd.Parameters.AddWithValue("@Date", date);
        cmd.Parameters.AddWithValue("@Total", total);

        cmd.ExecuteNonQuery();
        Console.WriteLine("Commande ajoutée");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}

// ===================== MENU =====================

while (true)
{
    
    Console.WriteLine("\n<('-' <) (> '-')> MENU <('-' <) (> '-')>");
    Console.WriteLine("1. Ajouter un client");
    Console.WriteLine("2. Afficher tous les clients");
    Console.WriteLine("3. Consulter un client et ses commandes");
    Console.WriteLine("4. Modifier un client");
    Console.WriteLine("5. Supprimer un client");
    Console.WriteLine("6. Ajouter une commande");
    Console.WriteLine("0. Quitter");

    Console.Write("Choix : ");
    string choix = Console.ReadLine();

    switch (choix)
    {
        case "1": AjouterClient(); break;
        case "2": AfficherClients(); break;
        case "3": ConsulterClient(); break;
        case "4": ModifierClient(); break;
        case "5": SupprimerClient(); break;
        case "6": AjouterCommande(); break;
        case "0": return;
        default: Console.WriteLine("Choix invalide"); break;
    }
}
