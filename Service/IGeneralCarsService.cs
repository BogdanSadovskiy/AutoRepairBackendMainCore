using AutoRepairMainCore.Entity.CarsGeneralFolder;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Service
{
    public interface IGeneralCarsService
    {
        Task<List<Brand>> GetBrands();
        Task<List<Model>> GetModels();
        Task<List<Engine>> GetEngines();
        Task<Brand> GetBrand(string name);
        Task<Model> GetModel(string name);
        Task<Engine> GetEngine(string name);
        Task<Brand> AddNewBrand(Brand brand);
        
        Task<Model> AddNewModel(Model model);

        Task<string> AddNewEngine(Engine engine);
    }
}
