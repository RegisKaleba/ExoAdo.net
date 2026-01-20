using System;
using System.Collections.Generic;
using System.Text;

namespace AdoNet3.Classes
{
    public class Client
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Adresse { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }

     
        public Client() { }

      
        public Client(string nom, string prenom, string adresse, string codePostal, string ville, string telephone)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
            Telephone = telephone;
        }

    
        public Client(int id, string nom, string prenom, string adresse, string codePostal, string ville, string telephone)
        {
            ID = id;
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            CodePostal = codePostal;
            Ville = ville;
            Telephone = telephone;
        }

        public override string ToString()
        {
            return $"{ID} - {Nom} {Prenom} - {Adresse}, {CodePostal} {Ville} - {Telephone}";
        }
    }
}