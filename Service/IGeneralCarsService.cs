using AutoRepairMainCore.DTO;
using AutoRepairMainCore.DTO.Models;
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Entity.ErrorCodesGeneralFolder;

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

        Car AddCar(CarDto newCar);

        Task<CarDto> OpenAICarValidation(CarDto car);

        CreatingECUDto AddBlock(List<string> newNames);
    }
}
