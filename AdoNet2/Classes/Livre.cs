using System;
using System.Collections.Generic;
using System.Text;

namespace AdoNet2.Classes
{
    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Auteur { get; set; } = string.Empty;
        public int AnneePublication { get; set; }
        public string Isbn { get; set; } = string.Empty;

        public Livre() { }

        public Livre(string titre, string auteur, int anneePublication, string isbn)
        {
            Titre = titre;
            Auteur = auteur;
            AnneePublication = anneePublication;
            Isbn = isbn;
        }
    }
}
