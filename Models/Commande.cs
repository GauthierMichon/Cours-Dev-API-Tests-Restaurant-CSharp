using System;

namespace RestaurantAPI.Models
{
    public class Commande
    {
        public int Commande_Id { get; set; }
        public int? Reservation_Id { get; set; }
        public int Employe_Id { get; set; }
        public DateTime Date_Commande { get; set; }  // Type DateTime
        public TimeSpan Heure_Commande { get; set; } // Type TimeSpan
        public string Statut { get; set; }

        public Reservation? Reservation { get; set; }
        public Employe? Employe { get; set; }
    }
}
