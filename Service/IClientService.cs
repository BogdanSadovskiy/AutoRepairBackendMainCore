using AutoRepairMainCore.Entity.ServiceFolder;

namespace AutoRepairMainCore.Service
{
    public interface IClientService
    {
        ClientCar? FindClientCarByIdAndAutoServiceId(int clientCarId, int autoServiceId);

        Client? FindClientByIdAndAutoserviceId(int autoserviceId, int clientId);

        Client? FindClientByNameAndAutoserviceId(int autoserviceId, string name, string surname);

        Client CreateClient(int autoserviceId, string name, string surname, string phone);

        Client? UpdateClient(int clientId, int autoserviceId, string? name = null, string? surname = null, string? phone = null);

        ClientCar CreateClientCar(int autoserviceId, int? carId, string vinCode);
    }
}
