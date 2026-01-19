using MySql.Data.MySqlClient;
using AdoNet2.Classes;
using System;


Console.WriteLine("Exercice 2 – Gestion des livres");
string connectionString = "Server=localhost;Database=bibliotheque;User ID=root;Password=root";
var ihm = new LivreIHM(connectionString);
ihm.Lancer();