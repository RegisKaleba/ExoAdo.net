using MySql.Data.MySqlClient;
using AdoNet2.Classes;
using System;

Console.WriteLine("Exercice 2 – Gestion des livres");

// Chaîne de connexion
string connectionString = "Server=localhost;Database=bibliotheque;User ID=root;Password=root";


// ===================== AJOUT =====================
void AjouterLivre()
{
    try
    {
        Console.WriteLine("\nAjout d'un nouveau livre");

        Console.Write("Titre : ");
        string titre = Console.ReadLine();

        Console.Write("Auteur : ");
        string auteur = Console.ReadLine();

        Console.Write("Année de publication : ");
        if (!int.TryParse(Console.ReadLine(), out int annee))
        {
            Console.WriteLine("❌ Année invalide");
            return;
        }

        Console.Write("ISBN : ");
        string isbn = Console.ReadLine();

        Livre livre = new Livre(titre, auteur, annee, isbn);

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string query = @"INSERT INTO Livre (Titre, Auteur, AnneePublication, Isbn)
                         VALUES (@titre, @auteur, @annee, @isbn)";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@titre", livre.Titre);
        cmd.Parameters.AddWithValue("@auteur", livre.Auteur);
        cmd.Parameters.AddWithValue("@annee", livre.AnneePublication);
        cmd.Parameters.AddWithValue("@isbn", livre.Isbn);

        cmd.ExecuteNonQuery();
        Console.WriteLine("Livre ajouté avec succès");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}


// ===================== AFFICHER =====================
void AfficherLivres()
{
    try
    {
        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string query = "SELECT * FROM Livre";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        MySqlDataReader reader = cmd.ExecuteReader();

        Console.WriteLine("\nListe des livres");
        while (reader.Read())
        {
            Console.WriteLine($"{reader["Id"]} | {reader["Titre"]} | {reader["Auteur"]} | {reader["AnneePublication"]} | {reader["Isbn"]}");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}


// ===================== CONSULTER =====================
void ConsulterLivre()
{
    try
    {
        Console.Write("\nId du livre : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string query = "SELECT * FROM Livre WHERE Id=@id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"Titre : {reader["Titre"]}");
            Console.WriteLine($"Auteur : {reader["Auteur"]}");
            Console.WriteLine($"Année : {reader["AnneePublication"]}");
            Console.WriteLine($"ISBN : {reader["Isbn"]}");
        }
        else
        {
            Console.WriteLine("Livre introuvable");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}


// ===================== MODIFIER =====================
void ModifierLivre()
{
    try
    {
        Console.Write("\nId du livre à modifier : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        Console.Write("Nouveau titre : ");
        string titre = Console.ReadLine();

        Console.Write("Nouvel auteur : ");
        string auteur = Console.ReadLine();

        Console.Write("Nouvelle année : ");
        if (!int.TryParse(Console.ReadLine(), out int annee))
        {
            Console.WriteLine("Année invalide");
            return;
        }

        Console.Write("Nouvel ISBN : ");
        string isbn = Console.ReadLine();

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string query = @"UPDATE Livre 
                         SET Titre=@titre, Auteur=@auteur, AnneePublication=@annee, Isbn=@isbn
                         WHERE Id=@id";

        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@titre", titre);
        cmd.Parameters.AddWithValue("@auteur", auteur);
        cmd.Parameters.AddWithValue("@annee", annee);
        cmd.Parameters.AddWithValue("@isbn", isbn);
        cmd.Parameters.AddWithValue("@id", id);

        int rows = cmd.ExecuteNonQuery();
        Console.WriteLine(rows > 0 ? "Livre modifié" : "Livre introuvable");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}


// ===================== SUPPRIMER =====================
void SupprimerLivre()
{
    try
    {
        Console.Write("\nId du livre à supprimer : ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Id invalide");
            return;
        }

        using MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string query = "DELETE FROM Livre WHERE Id=@id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        int rows = cmd.ExecuteNonQuery();
        Console.WriteLine(rows > 0 ? "Livre supprimé" : "Livre introuvable");
    }
    catch (Exception e)
    {
        Console.WriteLine("Erreur : " + e.Message);
    }
}


// ===================== MENU =====================
while (true)
{
    Console.WriteLine("\n===== MENU =====");
    Console.WriteLine("1. Ajouter un livre");
    Console.WriteLine("2. Afficher tous les livres");
    Console.WriteLine("3. Consulter un livre par Id");
    Console.WriteLine("4. Modifier un livre");
    Console.WriteLine("5. Supprimer un livre");
    Console.WriteLine("0. Quitter");

    Console.Write("Choix : ");
    string choix = Console.ReadLine();

    switch (choix)
    {
        case "1": AjouterLivre(); break;
        case "2": AfficherLivres(); break;
        case "3": ConsulterLivre(); break;
        case "4": ModifierLivre(); break;
        case "5": SupprimerLivre(); break;
        case "0": return;
        default: Console.WriteLine("Choix invalide"); break;
    }
}
