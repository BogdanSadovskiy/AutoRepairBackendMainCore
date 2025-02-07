using AutoRepairMainCore.DTO;
using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.CarsGeneralFolder;

namespace AutoRepairMainCore.Service
{
    public interface IGeneralCarsService
    {
        List<Brand> GetBrands();

        List<Model> GetModels();

        List<Engine> GetEngines();

        Brand GetBrand(string name);

        Model GetModel(string name);

        Engine GetEngine(string name);

        CarResults<Brand> AddBrand(string brand);

        CarResults<Model> AddModel(string model);

        CarResults<Engine> AddEngine(string engine);

        Car AddCar(CarDto newCar);

        Task<CarDto> OpenAICarValidation(CarDto car);
    }
}
