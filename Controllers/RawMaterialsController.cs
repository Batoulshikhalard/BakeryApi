using BakeryApi.Data;
using BakeryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RawMaterialsController : ControllerBase
    {
        private readonly BakeryContext _context;

        public RawMaterialsController(BakeryContext context)
        {
            _context = context;
        }

        // GET: api/RawMaterials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawMaterial>>> GetRawMaterials()
        {
            return await _context.RawMaterials.ToListAsync();
        }

        // GET: api/RawMaterials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RawMaterial>> GetRawMaterial(int id)
        {
            var rawMaterial = await _context.RawMaterials
                .Include(rm => rm.SupplierRawMaterials)
                .ThenInclude(srm => srm.Supplier)
                .FirstOrDefaultAsync(rm => rm.RawMaterialId == id);

            if (rawMaterial == null)
            {
                return NotFound();
            }

            return rawMaterial;
        }

        [HttpGet("{id}/Prices")]
        public async Task<ActionResult<IEnumerable<SupplierRawMaterial>>> GetRawMaterialPrices(int id)
        {
            var prices = await _context.SupplierRawMaterials
                .Include(srm => srm.Supplier)
                .Where(srm => srm.RawMaterialId == id)
                .ToListAsync();

            if (!prices.Any())
            {
                return NotFound();
            }

            return prices;
        }

        // POST: api/RawMaterials
        [HttpPost]
        public async Task<ActionResult<RawMaterial>> PostRawMaterial(RawMaterial rawMaterial)
        {
            _context.RawMaterials.Add(rawMaterial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRawMaterial), new { id = rawMaterial.RawMaterialId }, rawMaterial);
        }
    }
}
