using BharatMedicsV2.Models;

namespace BharatMedicsV2.Repository.IRepository
{
    public interface IOrder : ICommonRepository <Order>
    {
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetAllOrdersByEmails(string email);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);

    }
}
