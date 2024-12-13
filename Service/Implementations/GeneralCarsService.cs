
using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairMainCore.Service.Implementations
{
    public class GeneralCarsService : IGeneralCarsService
    {
        private readonly MySqlContext _context;

        public GeneralCarsService(MySqlContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetBrands()
        {
            return await _context.brands.ToListAsync();
        }

        public async Task<List<Model>> GetModels()
        {
            return await _context.models.ToListAsync();
        }

        public async Task<List<Engine>> GetEngines()
        {
            return await _context.engines.ToListAsync();
        }

        public Task<string> AddNewBrand(Brand brand)
        {
            throw new NotImplementedException();
        }

        public Task<string> AddNewModel(Model model)
        {
            throw new NotImplementedException();
        }

        public async Task<Engine> AddNewEngine(string engineDescription)
        {
            Engine existingEngine = await _context.engines.FirstOrDefaultAsync(e => e.EngineDescription == engineDescription);
            if (existingEngine != null)
            {
                return existingEngine; 
            }

            Engine newEngine = new Engine { EngineDescription = engineDescription };
            _context.engines.Add(newEngine);
            await _context.SaveChangesAsync();
            return newEngine;
        }

        
    }
}
