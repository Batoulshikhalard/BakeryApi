using System.ComponentModel.DataAnnotations;

namespace BakeryApi.Models;
public class Supplier
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string ContactPerson { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }

    //relation with Address
    public List<Address> Addresses { get; set; } = new List<Address>();

    // Navigation property for SupplierRawMaterials
    public List<SupplierRawMaterial> SupplierRawMaterials { get; set; } = new List<SupplierRawMaterial>();

}
