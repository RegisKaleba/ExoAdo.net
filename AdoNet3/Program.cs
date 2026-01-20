using AdoNet3.DAO;
using AdoNet3.IHM;
using MySql.Data.MySqlClient;
using AdoNet3.Classes;
using System;

string connectionString = "Server=localhost;Database=GestionCommandes;User ID=root;Password=root";
GestionDAO dao = new GestionDAO(connectionString);
GestionIHM ihm = new GestionIHM(dao);
ihm.Lancer();