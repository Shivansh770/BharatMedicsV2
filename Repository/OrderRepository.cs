using BharatMedicsV2.DataFiles;
using BharatMedicsV2.Models;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BharatMedicsV2.Repository
{
    public class OrderRepository : CommonRepository<Order> , IOrder
    {
        private readonly DataContext dataContext;
        public OrderRepository(DataContext _dataContext) : base(_dataContext)
        {
            dataContext = _dataContext;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var ord = dataContext.Orders.Include(u =>u.Carts).ToList();
            return ord;
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var ord = dataContext.Orders.Where(x => x.OrderId == order.OrderId).Include(x => x.Carts).AsNoTracking().FirstOrDefault();
            foreach (var item in ord.Carts)
            {

                await QuantityNeededUpdate(item.SupplierId, item.NoOfItems);
            }

            dataContext.Orders.Update(order);
            await dataContext.SaveChangesAsync();
        }
        public async Task QuantityNeededUpdate(int supplierId, int noOfItems)
        {
            var ord = dataContext.Suppliers.Where(u => u.SupplierId == supplierId).FirstOrDefault();
            ord.DrugQuantity = noOfItems;
            dataContext.Suppliers.Update(ord);
            await dataContext.SaveChangesAsync();
        }

        

        public async Task DeleteOrderAsync(int id)
        {
            var temp = dataContext.Orders.Where(x => x.OrderId == id).Include(x => x.Carts).FirstOrDefault();

            temp.Carts = null;

            dataContext.Orders.Update(temp);
            await dataContext.SaveChangesAsync();


            dataContext.Orders.Remove(temp);
        }

        public Task PriceUpdate(int id, int price)
        {
            return Task.FromResult(0);
        }

        public async Task<List<Order>> GetAllOrdersByEmails(string email)
        {
            var ord = dataContext.Orders.Where(u => u.Email == email).ToListAsync();
            return await ord;
        }
        
    }
}
