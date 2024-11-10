namespace RestaurantAPI.Models
{
    public class Menu
    {
        public int Plat_Id { get; set; }
        public string Nom_Plat { get; set; }
        public string? Description { get; set; }
        public decimal Prix { get; set; }
        public string Categorie { get; set; }
    }
}
