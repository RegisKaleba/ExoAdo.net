using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace AdoNet2.Classes
{
    public class LivreIHM
    {
        private  Repository _repository;

        public LivreIHM(string connectionString)
        {
            _repository = new Repository(connectionString);
        }

        public void Lancer()
        {
            while (true)
            {
                Console.WriteLine("\n===== MENU =====");
                Console.WriteLine("1. Ajouter un livre");
                Console.WriteLine("2. Afficher tous les livres");
                Console.WriteLine("3. Consulter un livre");
                Console.WriteLine("4. Modifier un livre");
                Console.WriteLine("5. Supprimer un livre");
                Console.WriteLine("0. Quitter");

                Console.Write("Choix : ");
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1": Ajouter(); break;
                    case "2": Afficher(); break;
                    case "3": Consulter(); break;
                    case "4": Modifier(); break;
                    case "5": Supprimer(); break;
                    case "0": return;
                    default: Console.WriteLine("Choix invalide"); break;
                }
            }
        }

        private void Ajouter()
        {
            Console.Write("Titre : ");
            string titre = Console.ReadLine();

            Console.Write("Auteur : ");
            string auteur = Console.ReadLine();

            Console.Write("Année : ");
            if (!int.TryParse(Console.ReadLine(), out int annee)) return;

            Console.Write("ISBN : ");
            string isbn = Console.ReadLine();

            _repository.Ajouter(new Livre(titre, auteur, annee, isbn));
            Console.WriteLine("Livre ajouté");
        }

        private void Afficher()
        {
            foreach (var livre in _repository.GetAll())
            {
                Console.WriteLine($"{livre.Id} | {livre.Titre} | {livre.Auteur} | {livre.AnneePublication} | {livre.Isbn}");
            }
        }

        private void Consulter()
        {
            Console.Write("Id : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            var livre = _repository.GetById(id);
            if (livre == null)
            {
                Console.WriteLine("Livre introuvable");
                return;
            }

            Console.WriteLine($"{livre.Titre} - {livre.Auteur} - {livre.AnneePublication} - {livre.Isbn}");
        }

        private void Modifier()
        {
            Console.Write("Id : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Console.Write("Titre : ");
            string titre = Console.ReadLine();

            Console.Write("Auteur : ");
            string auteur = Console.ReadLine();

            Console.Write("Année : ");
            if (!int.TryParse(Console.ReadLine(), out int annee)) return;

            Console.Write("ISBN : ");
            string isbn = Console.ReadLine();

            bool ok = _repository.Modifier(
                new Livre(titre, auteur, annee, isbn) { Id = id }
            );

            Console.WriteLine(ok ? "Livre modifié" : "Livre introuvable");
        }

        private void Supprimer()
        {
            Console.Write("Id : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) return;

            Console.WriteLine(_repository.Supprimer(id) ? "Livre supprimé" : "Livre introuvable");
        }
    }
}
