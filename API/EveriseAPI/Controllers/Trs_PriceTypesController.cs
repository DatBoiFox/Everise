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
    public class Trs_PriceTypesController : ControllerBase
    {
        private readonly APIContext _context;

        public Trs_PriceTypesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Trs_PriceTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trs_PriceTypes>>> GetTrsPriceTypes()
        {
            return await _context.TrsPriceTypes.ToListAsync();
        }

        // GET: api/Trs_PriceTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trs_PriceTypes>> GetTrs_PriceTypes(int id)
        {
            var trs_PriceTypes = await _context.TrsPriceTypes.FindAsync(id);

            if (trs_PriceTypes == null)
            {
                return NotFound();
            }

            return trs_PriceTypes;
        }

        // PUT: api/Trs_PriceTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrs_PriceTypes(int id, Trs_PriceTypes trs_PriceTypes)
        {
            if (id != trs_PriceTypes.PriceTypeId)
            {
                return BadRequest();
            }

            _context.Entry(trs_PriceTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Trs_PriceTypesExists(id))
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

        // POST: api/Trs_PriceTypes
        [HttpPost]
        public async Task<ActionResult<Trs_PriceTypes>> PostTrs_PriceTypes(Trs_PriceTypes trs_PriceTypes)
        {
            _context.TrsPriceTypes.Add(trs_PriceTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrs_PriceTypes", new { id = trs_PriceTypes.PriceTypeId }, trs_PriceTypes);
        }

        // DELETE: api/Trs_PriceTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trs_PriceTypes>> DeleteTrs_PriceTypes(int id)
        {
            var trs_PriceTypes = await _context.TrsPriceTypes.FindAsync(id);
            if (trs_PriceTypes == null)
            {
                return NotFound();
            }

            _context.TrsPriceTypes.Remove(trs_PriceTypes);
            await _context.SaveChangesAsync();

            return trs_PriceTypes;
        }

        private bool Trs_PriceTypesExists(int id)
        {
            return _context.TrsPriceTypes.Any(e => e.PriceTypeId == id);
        }
    }
}
