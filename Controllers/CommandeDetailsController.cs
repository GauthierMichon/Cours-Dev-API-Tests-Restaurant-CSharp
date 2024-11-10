using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Data;
using RestaurantAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeDetailsController : ControllerBase
    {
        private readonly RestaurantContext _context;

        public CommandeDetailsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: api/CommandeDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandeDetail>>> GetCommandeDetails()
        {
            return await _context.Commande_Details.ToListAsync();
        }

        // GET: api/CommandeDetails/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CommandeDetail>> GetCommandeDetailById(int id)
        {
            var commandeDetail = await _context.Commande_Details.FindAsync(id);

            if (commandeDetail == null)
            {
                return NotFound();
            }

            return commandeDetail;
        }

        // POST: api/CommandeDetails
        [HttpPost]
        public async Task<ActionResult<CommandeDetail>> PostCommandeDetail(CommandeDetail commandeDetail)
        {
            _context.Commande_Details.Add(commandeDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommandeDetailById), new { id = commandeDetail.Commande_Detail_Id }, commandeDetail);
        }

        // PUT: api/CommandeDetails/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommandeDetail(int id, CommandeDetail commandeDetail)
        {
            if (id != commandeDetail.Commande_Detail_Id)
            {
                return BadRequest();
            }

            _context.Entry(commandeDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommandeDetailExists(id))
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

        // DELETE: api/CommandeDetails/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommandeDetail(int id)
        {
            var commandeDetail = await _context.Commande_Details.FindAsync(id);
            if (commandeDetail == null)
            {
                return NotFound();
            }

            _context.Commande_Details.Remove(commandeDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Méthode auxiliaire pour vérifier si un CommandeDetail existe
        private bool CommandeDetailExists(int id)
        {
            return _context.Commande_Details.Any(e => e.Commande_Detail_Id == id);
        }
    }
}
