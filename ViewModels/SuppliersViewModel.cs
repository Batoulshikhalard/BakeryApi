namespace BakeryApi.ViewModels;
public class SuppliersViewModel
{
    public int SupplierId { get; set; }
    public string? Name { get; set; }
    public string? ContactPerson { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
public class SupplierViewModel : SuppliersViewModel
{
    public List<AddressViewModel> Addresses { get; set; } = [];
}

public class SupplierRawMaterials : SuppliersViewModel
{
    public List<RawMaterialViewModel> RawMaterials { get; set; } = [];
}