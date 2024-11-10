namespace RestaurantAPI.Models
{
    public class CommandeDetail
    {
        public int Commande_Detail_Id { get; set; }
        public int Commande_Id { get; set; }
        public int Plat_Id { get; set; }
        public int Quantite { get; set; }

        public Commande? Commande { get; set; }
        public Menu? Plat { get; set; }
    }
}
