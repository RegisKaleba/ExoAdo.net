using System;
using System.Collections.Generic;
using System.Text;

namespace AdoNet3.Classes
{
    public class Commande
    {
        public int ID { get; set; }
        public int ClientID { get; set; }
        public DateTime DateCommande { get; set; }
        public decimal Total { get; set; }

        
        public Commande() { }

        
        public Commande(int clientId, DateTime dateCommande, decimal total)
        {
            ClientID = clientId;
            DateCommande = dateCommande;
            Total = total;
        }

    
        public Commande(int id, int clientId, DateTime dateCommande, decimal total)
        {
            ID = id;
            ClientID = clientId;
            DateCommande = dateCommande;
            Total = total;
        }

        public override string ToString()
        {
            return $"{ID} - ClientID: {ClientID} - Date: {DateCommande.ToShortDateString()} - Total: {Total:C}";
        }
    }
}