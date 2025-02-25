using BakeryApi.Data;
using BakeryApi.Interfaces;
using BakeryApi.Models;
using BakeryApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BakeryApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BakeryContext _context;

    public CustomerRepository(BakeryContext context)
    {
        _context = context;
    }

    public async Task<bool> AddCustomerAsync(CustomerViewModel model)
    {
        try
        {
            if (await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower().Trim() ==
            model.Email.ToLower().Trim()) != null)
            {
                throw new BadHttpRequestException("Email already found");
            }

            var customer = new Customer
            {
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            foreach (var a in model.Addresses)
            {
                var add = new Address
                {
                    Country = a.Country,
                    City = a.City,
                    EntityType = nameof(Customer),
                    EntityId = customer.CustomerId,
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
    
    public async Task<CustomerViewModel> GetCustomerByIdAsync(int id)
    {
        var customer = await _context.Customers
            .Where(x => x.CustomerId == id)
            .FirstOrDefaultAsync();

        if (customer == null)
            throw new Exception("Customer not found");


        var view = new CustomerViewModel
        {
            Email = customer.Email,
            Name = customer.Name,
            PhoneNumber = customer.PhoneNumber,
            CustomerId = customer.CustomerId,
            Addresses = _context.Addresses.Where(x => x.EntityId== customer.CustomerId && x.EntityType == nameof(Customer)).Select(a => new AddressViewModel
            {
                AddressId = a.AddressId,
                City = a.City,
                Country = a.Country,
                EntityId = a.EntityId,
                EntityType = a.EntityType,
                PostalCode = a.PostalCode,
                Street = a.Street
            }).ToList()
        };

        return view;
    }

    public async Task<List<CustomersViewModel>> GetCustomersAsync()
    {
        return await _context.Customers
            .Select(c => new CustomersViewModel
            {
                CustomerId = c.CustomerId,
                Email = c.Email,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber
            })
            .ToListAsync();
    }
}
