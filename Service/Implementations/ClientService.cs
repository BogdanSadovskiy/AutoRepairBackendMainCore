using AutoRepairMainCore.Entity.ServiceFolder;
using AutoRepairMainCore.Exceptions.AutoServiceExceptions;
using AutoRepairMainCore.Infrastructure;
using System.Text.RegularExpressions;

namespace AutoRepairMainCore.Service.Implementations
{
    public class ClientService : IClientService
    {
        private MySqlContext _context;

        public ClientService(MySqlContext context)
        {
            _context = context;
        }

        public Client? FindClientByNameAndAutoserviceId(int autoserviceId, string name, string surname)
        {
            return _context.clients.Where(c => c.Name == name && c.Surname == surname &&  c.AutoserviceId == autoserviceId).FirstOrDefault();
        }

        public Client? FindClientByIdAndAutoserviceId(int autoserviceId, int clientId)
        {
            return _context.clients.Where(c => c.Id == clientId && c.AutoserviceId == autoserviceId).FirstOrDefault();
        }

        public List<Client> FindAllClients(int autoserviceId)
        {
            return _context.clients.Where(c => c.AutoserviceId == autoserviceId).ToList();
        }

        public List<ClientCar> FindClientCarsByAutoServiceIdAndClientId(int autoServiceId, int clientId)
        {
            return _context.clientCars.Where(cc => cc.AutoserviceId == autoServiceId &&
                   _context.orders.Any(o => o.ClientCarId == cc.Id && o.ClientId == clientId)).ToList();
        }

        public ClientCar? FindClientCarByAutoServiceIdAndVinCode(int autoServiceId, string vinCode)
        {
            return _context.clientCars.Where(cc => cc.AutoserviceId == autoServiceId && cc.VinCode == vinCode).FirstOrDefault();
        }

        public ClientCar? FindClientCarByIdAndAutoServiceId(int clientCarId, int autoServiceId)
        {
            return _context.clientCars.Where(cc => cc.Id == clientCarId && cc.AutoserviceId == autoServiceId).FirstOrDefault();
        }

        public Client? UpdateClient(int clientId, int autoserviceId, string? name = null, string? surname = null, string? phone = null)
        {
            bool isSmtChanged = false;
            Client? existingClient = FindClientByIdAndAutoserviceId(autoserviceId, clientId);

            if (existingClient == null)
            {
                throw new InvalidParameterException($"There is not existing client to update");
            }
            if (!string.IsNullOrEmpty(name) || existingClient.Name != name)
            {
                existingClient.Name = name;
                isSmtChanged = true;
            }
            if(!string.IsNullOrWhiteSpace(surname) || existingClient.Surname != surname)
            {
                existingClient.Surname = surname;
                isSmtChanged = true;
            }
            if (!string.IsNullOrWhiteSpace(phone) || existingClient.Phone != phone)
            {
                existingClient.Phone = phone;
                isSmtChanged = true;
            }
            if (!isSmtChanged)
            {
                throw new InvalidParameterException("Nothing to update");
            }

            _context.clients.Update(existingClient);
            _context.SaveChanges();
            return existingClient;
        }

        public Client CreateClient(int autoserviceId, string name, string surname, string phone)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(phone))
            {
                throw new InvalidProgramException("fill all fields");
            }

            Client? existingClient = FindClientByNameAndAutoserviceId(autoserviceId, name, surname);

            if (existingClient != null)
            {
                return existingClient;
            }

            existingClient = new Client()
            {
                Name = name,
                Surname = surname,
                Phone = phone
            };

            _context.clients.Add(existingClient);
            _context.SaveChanges();
            return existingClient;
        }

        private bool IsValidVinCode(string vin)
        {
            if (string.IsNullOrWhiteSpace(vin))
            {
                throw new InvalidParameterException("VIN cannot be empty");
            }
            vin = vin.ToUpper();
            if (!Regex.IsMatch(vin, "^[A-HJ-NPR-Z0-9]{17}$"))
            {
                throw new InvalidParameterException("VIN must contain 17 digits and only Uppercase letters");
            }
            return true;
        }

        public ClientCar CreateClientCar(int autoserviceId, int? carId, string vinCode)
        {
            IsValidVinCode(vinCode);
            ClientCar? existingClientCar = FindClientCarByAutoServiceIdAndVinCode(autoserviceId, vinCode);

            if(existingClientCar != null)
            {
                return existingClientCar;
            }

            if (!carId.HasValue)
            {
                throw new InvalidParameterException("No data about car");
            }

            existingClientCar = new ClientCar()
            {
                CarId = carId.Value,
                VinCode = vinCode
            };

            _context.clientCars.Add(existingClientCar);
            _context.SaveChanges();
            return existingClientCar;
        }
    }
}
