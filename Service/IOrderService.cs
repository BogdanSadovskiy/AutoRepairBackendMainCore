using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IOrderService
    {
        Order CreateOrder(int autoserviceId, CreateOrderDto newOrder);
        Task<List<OrderDto>> GetTenOrdersAsync(int autoserviceId, int skip, int take);
    }
}
