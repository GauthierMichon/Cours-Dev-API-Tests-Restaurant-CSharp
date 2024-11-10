using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public ClientsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClientById(int id)
        {
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // POST: api/Clients
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClientById), new { id = client.Client_Id }, client);
        }

        // PUT: api/Clients/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            if (id != client.Client_Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Clients/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null)
                {
                    return NotFound();
                }

                // Supprimer les réservations associées
                var reservations = await _context.Reservations
                    .Where(r => r.Client_Id == id)
                    .ToListAsync();

                // Supprimer les commandes associées aux réservations
                var commandes = await _context.Commandes
                    .Where(c => reservations.Select(r => r.Reservation_Id).Contains(c.Reservation_Id.Value))
                    .ToListAsync();

                // Supprimer les détails des commandes associés aux commandes
                var commandeDetails = await _context.Commande_Details
                    .Where(cd => commandes.Select(c => c.Commande_Id).Contains(cd.Commande_Id))
                    .ToListAsync();

                _context.Commande_Details.RemoveRange(commandeDetails);
                _context.Commandes.RemoveRange(commandes);
                _context.Reservations.RemoveRange(reservations);

                // Supprimer le client
                _context.Clients.Remove(client);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log exception si nécessaire
                return StatusCode(500, $"Erreur lors de la suppression du client et des enregistrements liés : {ex.Message}\n{ex.StackTrace}");
            }
        }



        // Méthode auxiliaire pour vérifier si un client existe
        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.Client_Id == id);
        }
    }
}
