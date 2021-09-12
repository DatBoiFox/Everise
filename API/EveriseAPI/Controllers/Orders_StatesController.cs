using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EveriseAPI.Models;

namespace EveriseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Orders_StatesController : ControllerBase
    {
        private readonly APIContext _context;

        public Orders_StatesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Orders_States
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders_States>>> GetOrderStates()
        {
            return await _context.OrderStates.ToListAsync();
        }

        // GET: api/Orders_States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders_States>> GetOrders_States(int id)
        {
            var orders_States = await _context.OrderStates.FindAsync(id);

            if (orders_States == null)
            {
                return NotFound();
            }

            return orders_States;
        }

        // PUT: api/Orders_States/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrders_States(int id, Orders_States orders_States)
        {
            if (id != orders_States.StateId)
            {
                return BadRequest();
            }

            _context.Entry(orders_States).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Orders_StatesExists(id))
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

        // POST: api/Orders_States
        [HttpPost]
        public async Task<ActionResult<Orders_States>> PostOrders_States(Orders_States orders_States)
        {
            _context.OrderStates.Add(orders_States);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrders_States", new { id = orders_States.StateId }, orders_States);
        }

        // DELETE: api/Orders_States/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders_States>> DeleteOrders_States(int id)
        {
            var orders_States = await _context.OrderStates.FindAsync(id);
            if (orders_States == null)
            {
                return NotFound();
            }

            _context.OrderStates.Remove(orders_States);
            await _context.SaveChangesAsync();

            return orders_States;
        }

        private bool Orders_StatesExists(int id)
        {
            return _context.OrderStates.Any(e => e.StateId == id);
        }
    }
}
