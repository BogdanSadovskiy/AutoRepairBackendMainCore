

using AutoRepairMainCore.Entity.CarsGeneralFolder;
using AutoRepairMainCore.Infrastructure;

namespace AutoRepairMainCore.Service.Implementations
{
    public class GeneralCarsService : IGeneralCarsService
    {
        private readonly MySqlContext _context;

        public GeneralCarsService(MySqlContext context)
        {
            _context = context;
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            return await _context.brands.ToListAsync();
        }

        public async Task<List<Model>> GetModelsAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task<List<Engine>> GetEnginesAsync()
        {
            return await _context.Engines.ToListAsync();
        }
    }
}
