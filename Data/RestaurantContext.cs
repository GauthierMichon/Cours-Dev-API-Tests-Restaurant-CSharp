using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models;

namespace RestaurantAPI.Data
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CommandeDetail> Commande_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Définition des clés primaires
            modelBuilder.Entity<Client>()
                .HasKey(c => c.Client_Id);

            modelBuilder.Entity<Employe>()
                .HasKey(e => e.Employe_Id);

            modelBuilder.Entity<Table>()
                .HasKey(t => t.Table_Id);

            modelBuilder.Entity<Menu>()
                .HasKey(m => m.Plat_Id);

            modelBuilder.Entity<Commande>()
                .HasKey(c => c.Commande_Id);

            modelBuilder.Entity<CommandeDetail>()
                .HasKey(cd => cd.Commande_Detail_Id);

            modelBuilder.Entity<Reservation>()
                .HasKey(r => r.Reservation_Id);

            // Relations avec suppression en cascade ou restriction
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.Client_Id)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade des réservations d'un client

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Table)
                .WithMany()
                .HasForeignKey(r => r.Table_Id)
                .OnDelete(DeleteBehavior.Restrict); // Restriction pour éviter la suppression de la table

            modelBuilder.Entity<Commande>()
                .HasOne(c => c.Reservation)
                .WithMany()
                .HasForeignKey(c => c.Reservation_Id)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade pour les commandes d'une réservation

            modelBuilder.Entity<Commande>()
                .HasOne(c => c.Employe)
                .WithMany()
                .HasForeignKey(c => c.Employe_Id)
                .OnDelete(DeleteBehavior.Restrict); // Restriction pour éviter suppression de l'employé

            modelBuilder.Entity<CommandeDetail>()
                .HasOne(cd => cd.Commande)
                .WithMany()
                .HasForeignKey(cd => cd.Commande_Id)
                .OnDelete(DeleteBehavior.Cascade); // Suppression en cascade pour les détails d'une commande

            modelBuilder.Entity<CommandeDetail>()
                .HasOne(cd => cd.Plat)
                .WithMany()
                .HasForeignKey(cd => cd.Plat_Id)
                .OnDelete(DeleteBehavior.Restrict); // Restriction pour éviter la suppression du plat
        }
    }
}
