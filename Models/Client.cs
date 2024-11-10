namespace RestaurantAPI.Models
{
    public class Client
    {
        public int Client_Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string? Email { get; set; }
    }
}