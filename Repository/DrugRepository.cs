using BharatMedicsV2.DataFiles;
using BharatMedicsV2.Models;
using BharatMedicsV2.Repository.IRepository;

namespace BharatMedicsV2.Repository
{
    public class DrugRepository : CommonRepository<Drug>, IDrug
    {

        private readonly DataContext _context;


        public DrugRepository(DataContext context) : base(context)
        {
            _context = context;

        }



        public async Task UpdateDrugsAsync(Drug drug)
        {
            _context.Drugs.Update(drug);
            await SaveAsync();
        }
    }
}
