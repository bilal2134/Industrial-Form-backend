using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;

namespace Enwage_API.Repositories
{
    public class StatenameRepository : GenericRepository<Statename>, IStatenameRepository
    {
        public StatenameRepository(Enwage2Context context) : base(context) { }

        // Add any Statename-specific methods here if needed
    }
}