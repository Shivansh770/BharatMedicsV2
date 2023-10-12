using BharatMedicsV2.Models;

namespace BharatMedicsV2.Repository.IRepository
{
    public interface IDrug : ICommonRepository <Drug>
    {
        Task UpdateDrugsAsync(Drug drug);

        //All other operations of add , delete , save , get drugs are available in IRepository which is common for all. 

    }
}
