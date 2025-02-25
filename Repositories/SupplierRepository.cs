using BakeryApi.Data;
using BakeryApi.Interfaces;
using BakeryApi.Models;
using BakeryApi.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace BakeryApi.Repositories;
public class SupplierRepository : ISupplierRepository
{
    private BakeryContext _context;
    public SupplierRepository(BakeryContext context)
    {
        _context = context;
    }

    public async Task<bool> AddSupplier(SupplierViewModel model)
    {
        try
        {
            if (await _context.Suppliers.FirstOrDefaultAsync(c => c.Email.ToLower().Trim() ==
            model.Email.ToLower().Trim()) != null)
            {
                throw new BadHttpRequestException("Email already found");
            }

            var supplier = new Supplier
            {
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                ContactPerson = model.ContactPerson,
            };

            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync();

            foreach (var a in model.Addresses)
            {
                var add = new Address
                {
                    Country = a.Country,
                    City = a.City,
                    EntityType = nameof(Supplier),
                    EntityId = supplier.SupplierId,
                    Street = a.Street,
                    PostalCode = a.PostalCode
                };
                await _context.Addresses.AddAsync(add);
            }

            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<SupplierViewModel> GetSupplierById(int supplierId)
    {
        var supplier = await _context.Suppliers
            .Where(s => s.SupplierId == supplierId)
            .Include(s => s.Addresses)
            .FirstOrDefaultAsync();

        if (supplier == null) 
        {
            throw new Exception("Supplier Notfound");
        }

        var view = new SupplierViewModel
        {
            Addresses = _context.Addresses.Where(s=> s.EntityId == supplier.SupplierId && s.EntityType== nameof(Supplier)).Select(a => new AddressViewModel
            {
                AddressId = a.AddressId,
                City = a.City,
                Country = a.Country,
                EntityId = a.EntityId,
                EntityType = a.EntityType,
                PostalCode = a.PostalCode,
                Street = a.Street
            }).ToList(),
            Email = supplier.Email,
            Name = supplier.Name,
            SupplierId = supplierId,
            PhoneNumber = supplier.PhoneNumber,
            ContactPerson = supplier.ContactPerson
        };
        return view;
    }

    public async Task<SupplierRawMaterials> GetSupplierRawMaterials(int supplierId)
    {
        var supplier = await _context.Suppliers
                        .Include(s => s.SupplierRawMaterials)
                        .ThenInclude(srm => srm.RawMaterial)
                        .FirstOrDefaultAsync(s => s.SupplierId == supplierId);


        var view = new SupplierRawMaterials
        {
            Email = supplier.Email,
            Name = supplier.Name,
            ContactPerson = supplier.ContactPerson,
            PhoneNumber = supplier.PhoneNumber,
            SupplierId = supplier.SupplierId,
            RawMaterials = supplier.SupplierRawMaterials.Select(s => new RawMaterialViewModel
            {
                RawMaterialId = s.RawMaterialId,
                PricePerKg = s.PricePerKg,
                Name = s.RawMaterial.Name,
                Code = s.RawMaterial.Code
            }).ToList(),
        };

        return view;
    }

    public async Task<IEnumerable<SuppliersViewModel>> GetSuppliers()
    {
        return await _context.Suppliers
            .Select(s => new SuppliersViewModel
            {
                Email = s.Email,
                Name = s.Name,
                ContactPerson = s.ContactPerson,
                PhoneNumber = s.PhoneNumber,
                SupplierId = s.SupplierId
            })
            .ToListAsync();
    }
}
