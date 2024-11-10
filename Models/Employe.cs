using System;

namespace RestaurantAPI.Models
{
    public class Employe
    {
        public int Employe_Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Poste { get; set; }
        public decimal Salaire { get; set; }
        public DateTime Date_Embauche { get; set; }
    }
}
