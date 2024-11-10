using System;

namespace RestaurantAPI.Models
{
    public class Reservation
    {
        public int Reservation_Id { get; set; }
        public int Client_Id { get; set; }
        public int Table_Id { get; set; }
        public DateTime Date_Reservation { get; set; }
        public TimeSpan Heure_Reservation { get; set; }
        public int Nombre_Personnes { get; set; }

        public Client? Client { get; set; }
        public Table? Table { get; set; }
    }
}
