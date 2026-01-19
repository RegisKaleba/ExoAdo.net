
using ADONET;
using MySql.Data.MySqlClient;

Console.WriteLine("Demo ADO.NET");


string connectionString = "Server=localhost;Database=demo_ado ;User ID=root;Password=root";



void AjouterPersonne()
{
    Console.WriteLine("Ajout d'une Nouvelle Personne !!!!");
    Console.WriteLine("Nom : ");
    string nom = Console.ReadLine();
    Console.WriteLine("Prenom : ");
    string prenom = Console.ReadLine();
    Console.WriteLine("Age : ");
    int age = int.Parse(Console.ReadLine());
    Console.WriteLine("Email : ");
    string email = Console.ReadLine();

    // Creation de l'objet Personne
    Personne personne = new Personne(nom,prenom,age,email);

    //Console.WriteLine(personne);

    // Mise en place de notre object qui nous servira a interagir avec la bdd
    MySqlConnection connection = new MySqlConnection(connectionString);

    // nous allons essayer de nous connecter a la BDD
    try
    {
        // Ouverture de la connection
        connection.Open();

        // requete a effectue sur la BDD avec les valeurs passée via des parametre pour eviter le injections SQL
        string query = "INSERT INTO Personne (nom,prenom,age,email) VALUES (@nom,@prenom,@age,@email)";

        // on va cree un object commande qui va conteneir la requete a effectue et la connection 
        MySqlCommand cmd = new MySqlCommand(query,connection);

        // On viens remplacer les differents parametres qui sont present dans notre requete
        cmd.Parameters.AddWithValue("@nom", personne.Nom);
        cmd.Parameters.AddWithValue("@prenom", personne.Prenom);
        cmd.Parameters.AddWithValue("@age", personne.Age);
        cmd.Parameters.AddWithValue("@email", personne.Email);

        // On vas executer la requete sur la bdd et recuperer le nombres de lignes affecter
        int rowAffected = cmd.ExecuteNonQuery();
        if (rowAffected > 0)
        {
            Console.WriteLine("Personne ajouté avec succes");
        }


    }
    catch (Exception e) { // j'atterit dans le catch si une erreur est arrive dans le try
        Console.WriteLine("Erreur : "+e.Message);
    }finally
    {
        connection.Close();
    }

}


void AfficherToutesLesPersonnes()
{
    Console.WriteLine("--- Liste des personnes ---");
    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        //Mise en place de la requete de recuperation des personnes
        string query = "SELECT * FROM Personne";
        //Creation de la commande pour interagir avec la base de donnée
        MySqlCommand cmd = new MySqlCommand(query, connection);

        //Execution de la requete de recuperation des personnes et stockage des donnée dans l'object DataReader
        MySqlDataReader reader = cmd.ExecuteReader();

        //vereification si on a bien au moins une ligne recuperé
        if (reader.HasRows)
        {

            //Pour chaque ligne recuperé on va donc crée un object personne
            while (reader.Read())
            {
               // Personne personne = new Personne(5, "toto","tata", 25, "toto@tata");
                Personne p = new Personne(
                    //recuperation de la valeur a la colone "id" dans une variable de type int
                    reader.GetInt32("id"),
                    //recuperation de la valeur a la colone "nom" dans une variable de type String
                    reader.GetString("nom"),
                    //recuperation de la valeur a la colone "prenom" dans une variable de type String
                    reader.GetString("prenom"),
                    //recuperation de la valeur a la colone "age" dans une variable de type int
                    reader.GetInt32("age"),
                    //recuperation de la valeur a la colone "email" dans une variable de type String
                    reader.GetString("email")
                    );

                Console.WriteLine(p);

            }
        }
        else
        {
            Console.WriteLine("Aucune persoone dans la base de donnée");
        }
        reader.Close();

    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
    //Utilisation du finaly (block au passage obligatoire pour fermer la connection a la bdd
    finally
    {
        connection.Close();
    }
}


void RechercherPersonneParId()
{
    Console.WriteLine("--- Recherche Par Id ---");
    Console.WriteLine("Id de la personne Recherché :");
    var id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string query = "SELECT * FROM Personne WHERE id = @id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        MySqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Personne p = new Personne(
                    //recuperation de la valeur a la colone "id" dans une variable de type int
                    reader.GetInt32("id"),
                    //recuperation de la valeur a la colone "nom" dans une variable de type String
                    reader.GetString("nom"),
                    //recuperation de la valeur a la colone "prenom" dans une variable de type String
                    reader.GetString("prenom"),
                    //recuperation de la valeur a la colone "age" dans une variable de type int
                    reader.GetInt32("age"),
                    //recuperation de la valeur a la colone "email" dans une variable de type String
                    reader.GetString("email")
                    );

            Console.WriteLine("Personne trouvé : " + p);
        }
        else
        {
            Console.WriteLine("Aucune personne trouvée avec cet ID ");
        }

        reader.Close();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}


void UpdatePersonne()
{
    Console.WriteLine("--- Modifier une personne ---");
    Console.WriteLine("Id de la personne a modifier :");
    var id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        //Verification si la personne avec cet id existe bien
        string queryCheck = "SELECT COUNT(*) FROM Personne WHERE id = @id";
        MySqlCommand cmdCheck = new MySqlCommand(queryCheck, connection);
        cmdCheck.Parameters.AddWithValue("@id", id);
        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

        if (count == 0)
        {
            Console.WriteLine("Aucune personne trouvée avec cet Id");
            return;
        }

        Console.WriteLine("Nouveau Nom :");
        var nom = Console.ReadLine();
        Console.WriteLine("Nouveau Prenom :");
        var prenom = Console.ReadLine();
        Console.WriteLine("nouvel Age :");
        var age = int.Parse(Console.ReadLine());
        Console.WriteLine("Nouvel Email :");
        var email = Console.ReadLine();


        string query = "UPDATE Personne SET nom = @nom , prenom = @prenom , age = @age , email = @email WHERE id = @id";


        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nom", nom);
        cmd.Parameters.AddWithValue("@prenom", prenom);
        cmd.Parameters.AddWithValue("@age", age);
        cmd.Parameters.AddWithValue("@email", email);

        int rowsAffected = cmd.ExecuteNonQuery();
        if (rowsAffected > 0)
        {
            Console.WriteLine("Personne modifié avec succès");
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur : " + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}


void DeletePersonne()
{
    Console.WriteLine("--- Supprimer une personne ---");
    Console.WriteLine("Id de la personne a supprimer :");
    int id = int.Parse(Console.ReadLine());

    MySqlConnection connection = new MySqlConnection(connectionString);
    try
    {
        connection.Open();

        string query = "DELETE FROM Personne WHERE id = @id";
        MySqlCommand cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@id", id);

        int rowsAffected = cmd.ExecuteNonQuery();

        if (rowsAffected > 0)
        {
            Console.WriteLine("Personne supprimé avec succès");
        }
        else
        {
            Console.WriteLine("Aucune personne trouvée a cet ID");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erreur :" + ex.Message);
    }
    finally
    {
        connection.Close();
    }
}



//AjouterPersonne();
AfficherToutesLesPersonnes();
//RechercherPersonneParId();
//UpdatePersonne();
//DeletePersonne();   

