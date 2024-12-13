using AutoRepairMainCore.Entity.CarsGeneralFolder;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Service
{
    public interface IGeneralCarsService
    {
        Task<List<Brand>> GetBrandsAsync();
        Task<List<Model>> GetModelsAsync();
        Task<List<Engine>> GetEnginesAsync();
    }
}
