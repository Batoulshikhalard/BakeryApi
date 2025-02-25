namespace BakeryApi.Models;
public class RawMaterial
{
    public int RawMaterialId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    // Navigation property for SupplierRawMaterials
    public List<SupplierRawMaterial> SupplierRawMaterials { get; set; } = new List<SupplierRawMaterial>();
}
