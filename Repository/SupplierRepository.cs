using BharatMedicsV2.DataFiles;
using BharatMedicsV2.Models;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BharatMedicsV2.Repository
{
    public class SupplierRepository : CommonRepository<Supplier>, ISupplier
    {
        private readonly DataContext _dataContext;

        public SupplierRepository(DataContext dataContext) : base(dataContext) 
        {
            _dataContext = dataContext;
        }
        public async Task<List<Supplier>> GetAllSuppliers()
        {
            var supp = _dataContext.Suppliers.Include(u => u.Drugs).ToList();
            return supp;
        }
        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            var sup = _dataContext.Suppliers.Update(supplier);
            await SaveAsync();
        }
    }
}
