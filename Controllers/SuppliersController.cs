using BakeryApi.Data;
using BakeryApi.Interfaces;
using BakeryApi.Models;
using BakeryApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierRepository _repo;

        public SuppliersController(ISupplierRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult> GetSuppliers()
        {
            return Ok(await _repo.GetSuppliers());
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(int id)
        {
            var supplier = await _repo.GetSupplierById(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        [HttpGet("{id}/raw")]
        public async Task<ActionResult> GetSupplierRawMaterial(int id)
        {
            var supplier = await _repo.GetSupplierRawMaterials(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return Ok(supplier);
        }

        // POST: api/Suppliers
        [HttpPost]
        public async Task<ActionResult> PostSupplier(SupplierViewModel supplier)
        {
            if (await _repo.AddSupplier(supplier))
            {
                 return StatusCode(201);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
