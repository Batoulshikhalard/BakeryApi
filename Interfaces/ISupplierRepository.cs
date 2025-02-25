using BakeryApi.ViewModels;

namespace BakeryApi.Interfaces;
public interface ISupplierRepository
{
    Task<IEnumerable<SuppliersViewModel>> GetSuppliers();
    Task<SupplierRawMaterials> GetSupplierRawMaterials(int supplierId);
    Task<SupplierViewModel> GetSupplierById(int supplierId);
    Task<bool> AddSupplier(SupplierViewModel supplier);
}
