using AdoNet3.Classes;
using AdoNet3.DAO;
using System;

namespace AdoNet3.IHM
{
    public class GestionIHM
    {
        private  GestionDAO dao;

        public GestionIHM(GestionDAO dao)
        {
            this.dao = dao;
        }

        public void Lancer()
        {
            AfficherTitre();
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
        }

        private void AfficherTitre()
        {
            Console.WriteLine(@"
   ____                                          _           
  / ___|___  _ __ ___  _ __ ___   __ _ _ __   __| | ___  ___ 
 | |   / _ \| '_ ` _ \| '_ ` _ \ / _` | '_ \ / _` |/ _ \/ __|
 | |__| (_) | | | | | | | | | | | (_| | | | | (_| |  __/\__ \
  \____\___/|_| |_| |_|_| |_| |_|\__,_|_| |_|\__,_|\___||___/
");
        }

        private void AjouterClient()
        {
            Console.WriteLine("\nAjout d'un client");
            Console.Write("Nom : "); string nom = Console.ReadLine();
            Console.Write("Prenom : "); string prenom = Console.ReadLine();
            Console.Write("Adresse : "); string adresse = Console.ReadLine();
            Console.Write("Code Postal : "); string cp = Console.ReadLine();
            Console.Write("Ville : "); string ville = Console.ReadLine();
            Console.Write("Telephone : "); string tel = Console.ReadLine();

            Client client = new Client(nom, prenom, adresse, cp, ville, tel);
            dao.AjouterClient(client);
            Console.WriteLine("Client ajouté avec succès");
        }

        private void AfficherClients()
        {
            var clients = dao.GetClients();
            Console.WriteLine("\nListe des clients :");
            foreach (var c in clients)
            {
                Console.WriteLine($"{c.ID} | {c.Nom} {c.Prenom} | {c.Adresse} | {c.Ville} | {c.Telephone}");
            }
        }

        private void ConsulterClient()
        {
            Console.Write("\nId du client : ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Id invalide");
                return;
            }

            var client = dao.GetClientById(id);
            if (client == null)
            {
                Console.WriteLine("Client introuvable");
                return;
            }

            Console.WriteLine($"\nNom : {client.Nom}\nPrenom : {client.Prenom}\nAdresse : {client.Adresse}\nCP : {client.CodePostal}\nVille : {client.Ville}\nTel : {client.Telephone}");

            var commandes = dao.GetCommandesByClient(id);
            Console.WriteLine("\nCommandes :");
            if (commandes.Count == 0)
            {
                Console.WriteLine("Aucune commande");
            }
            else
            {
                foreach (var cmd in commandes)
                {
                    Console.WriteLine($"{cmd.ID} | {cmd.DateCommande.ToShortDateString()} | {cmd.Total} thunes");
                }
            }
        }

        private void ModifierClient()
        {
            Console.Write("\nId du client à modifier : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Id invalide"); return; }

            Client client = new Client();
            client.ID = id;

            Console.Write("Nom : "); client.Nom = Console.ReadLine();
            Console.Write("Prenom : "); client.Prenom = Console.ReadLine();
            Console.Write("Adresse : "); client.Adresse = Console.ReadLine();
            Console.Write("Code Postal : "); client.CodePostal = Console.ReadLine();
            Console.Write("Ville : "); client.Ville = Console.ReadLine();
            Console.Write("Telephone : "); client.Telephone = Console.ReadLine();

            dao.ModifierClient(client);
            Console.WriteLine("Client modifié");
        }

        private void SupprimerClient()
        {
            Console.Write("\nId du client à supprimer : ");
            if (!int.TryParse(Console.ReadLine(), out int id)) { Console.WriteLine("Id invalide"); return; }
            dao.SupprimerClient(id);
            Console.WriteLine("Client et commandes supprimés");
        }

        private void AjouterCommande()
        {
            Console.Write("\nId du client : ");
            if (!int.TryParse(Console.ReadLine(), out int clientId)) { Console.WriteLine("Id invalide"); return; }

            Console.Write("Date (yyyy-MM-dd) : ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date)) { Console.WriteLine("Date invalide"); return; }

            Console.Write("Total : ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal total)) { Console.WriteLine("Total invalide"); return; }

            dao.AjouterCommande(new Commande(clientId, date, total));
            Console.WriteLine("Commande ajoutée");
        }
    }
}
