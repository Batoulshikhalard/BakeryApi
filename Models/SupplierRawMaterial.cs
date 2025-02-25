namespace BakeryApi.Models;
public class SupplierRawMaterial
{
    public int SupplierRawMaterialId { get; set; }
    public int SupplierId { get; set; }
    public int RawMaterialId { get; set; }
    public double PricePerKg { get; set; }

    // Navigation properties
    public virtual Supplier Supplier { get; set; }
    public virtual RawMaterial RawMaterial { get; set; }

}
