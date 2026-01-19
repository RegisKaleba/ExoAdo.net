using System;
using System.Collections.Generic;
using System.Text;

namespace ADONET
{
    internal class Personne
    {

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public Personne()
        {

        }

        public Personne(string nom, string prenom, int age, string email)
        {
            Nom = nom;
            Prenom = prenom;
            Age = age;
            Email = email;
        }

        public Personne(int id, string nom, string prenom, int age, string email) : this(nom, prenom, age, email)
        {
            Id = id;
        }


        public override string ToString()
        {
            return $"Id : {Id} , {Prenom} {Nom} ||| Age : {Age} | Email : {Email}";
        }
    }
}
