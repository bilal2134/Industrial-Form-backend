using Enwage_API.Models;
using Enwage_API.Repositories.Interfaces;

namespace Enwage_API.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(Enwage2Context context) : base(context) { }
    }
}