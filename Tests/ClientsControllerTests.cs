using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RestaurantAPI.Controllers;
using RestaurantAPI.Data;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantAPI.Tests
{
    public class ClientsControllerTests
    {
        private readonly ClientsController _controller;
        private readonly RestaurantContext _context;

        public ClientsControllerTests()
        {
            // Configuration de la base de données unique pour chaque test
            var options = new DbContextOptionsBuilder<RestaurantContext>()
                .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString()) // Nouvelle base pour chaque test
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)) // Ignore les transactions
                .Options;

            _context = new RestaurantContext(options);
            SeedDatabase();
            _controller = new ClientsController(_context);
        }

        private void SeedDatabase()
        {
            _context.Clients.AddRange(
                new Client { Client_Id = 1, Nom = "Dupont", Prenom = "Jean", Telephone = "0102030405", Email = "jean.dupont@example.com" },
                new Client { Client_Id = 2, Nom = "Martin", Prenom = "Paul", Telephone = "0607080910", Email = "paul.martin@example.com" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetClients_ReturnsAllClients()
        {
            var result = await _controller.GetClients();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Client>>>(result);
            var clients = Assert.IsType<List<Client>>(actionResult.Value);
            Assert.Equal(2, clients.Count);
        }

        [Fact]
        public async Task GetClientById_ExistingId_ReturnsClient()
        {
            var result = await _controller.GetClientById(1);
            var actionResult = Assert.IsType<ActionResult<Client>>(result);
            var client = Assert.IsType<Client>(actionResult.Value);
            Assert.Equal(1, client.Client_Id);
        }

        [Fact]
        public async Task GetClientById_NonExistingId_ReturnsNotFound()
        {
            var result = await _controller.GetClientById(99);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostClient_ValidClient_ReturnsCreatedClient()
        {
            var newClient = new Client { Client_Id = 3, Nom = "Bernard", Prenom = "Marie", Telephone = "0505050505", Email = "marie.bernard@example.com" };
            var result = await _controller.PostClient(newClient);
            var actionResult = Assert.IsType<ActionResult<Client>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var client = Assert.IsType<Client>(createdAtActionResult.Value);
            Assert.Equal("Bernard", client.Nom);
        }

        [Fact]
        public async Task DeleteClient_ExistingId_ReturnsNoContent()
        {
            var result = await _controller.DeleteClient(1);
            Assert.IsType<NoContentResult>(result);
            Assert.Null(await _context.Clients.FindAsync(1));
        }

        [Fact]
        public async Task DeleteClient_NonExistingId_ReturnsNotFound()
        {
            var result = await _controller.DeleteClient(99);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
