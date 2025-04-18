using AutoRepairMainCore.DTO;
using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairMainCore.Service.Implementations
{
    public class OrderService : IOrderService
    {
        MySqlContext _context;
        IClientService _clientService;

        public OrderService(MySqlContext context, IClientService clientService)
        {
            _context = context;
            _clientService = clientService;
        }

        public Order CreateOrder(int autoserviceId, CreateOrderDto newOrder)
        {
            Client client = GetOrCreateClient(autoserviceId, newOrder.Client);
            ClientCar clientCar = GetOrCreateClientCar(autoserviceId, newOrder.ClientCar);
            Order order = new Order()
            {
                AutoserviceId = autoserviceId,
                ClientCarId = clientCar.Id,
                ClientId = client.Id,
                EmployeeId = newOrder.EmployeeId,
                Description = newOrder.Description,
                DateIn = newOrder.DateIn
            };

            _context.orders.Add(order);
            _context.SaveChanges();
            return order;
        }

        public async Task<List<OrderDto>> GetTenOrdersAsync(int autoserviceId, int skip, int take)
        {
            return await _context.orders
                .Include(o => o.ClientCar)
                    .ThenInclude(cc => cc.Car)
                        .ThenInclude(c => c.Brand)
                .Include(o => o.ClientCar)
                    .ThenInclude(cc => cc.Car)
                        .ThenInclude(c => c.Model)
                .Include(o => o.ClientCar)
                    .ThenInclude(cc => cc.Car)
                        .ThenInclude(c => c.Engine)
                .Include(o => o.OrderFiles)
                .Where(o => o.AutoserviceId == autoserviceId)
                .OrderByDescending(o => o.DateIn)
                .Skip(skip)
                .Take(take)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    Brand = o.ClientCar.Car.Brand.BrandName,
                    Model = o.ClientCar.Car.Model.ModelName,
                    Engine = o.ClientCar.Car.Engine.EngineDescription,
                    DateIn = o.DateIn,
                    DateOut = o.DateOut,
                    ImageUrl = o.OrderFiles.FirstOrDefault() != null
                        ? o.OrderFiles.FirstOrDefault().FilePath : null
                })
                .ToListAsync();
        }


        private Client GetOrCreateClient(int autoserviceId, ClientDto clientDto)
        {
            Client client = _clientService.CreateClient(autoserviceId, clientDto.Name, clientDto.Surname, clientDto.Phone);
            return client;
        }

        private ClientCar GetOrCreateClientCar(int autoserviceId, ClientCarDto clientCarDto)
        {
            ClientCar clientCar = _clientService.CreateClientCar(autoserviceId, clientCarDto.CarId, clientCarDto.VinCode);
            return clientCar;
        }
    }
}
