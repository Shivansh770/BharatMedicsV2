using BharatMedicsV2.Models;

namespace BharatMedicsV2.Repository.IRepository
{
    public interface ISupplier : ICommonRepository <Supplier>
    {
        Task<List<Supplier>> GetAllSuppliers();
        Task UpdateSupplierAsync(Supplier supplier);
    }
}
