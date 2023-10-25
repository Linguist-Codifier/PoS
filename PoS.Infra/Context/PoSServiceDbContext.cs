using PoS.Infra.Domain.Interfaces;

namespace PoS.Infra.Context
{
    public class PoSServiceDbContext<PoSServiceRepository> : IPoSServiceDbContext<PoSServiceRepository> where PoSServiceRepository : IPoSRepository
    {
        private readonly PoSServiceRepository serviceScopeRepositoryInstance;

        PoSServiceRepository IPoSServiceDbContext<PoSServiceRepository>.Repository { get => this.serviceScopeRepositoryInstance; }

        public PoSServiceDbContext(PoSServiceRepository poSServiceRepositoryInstance) => this.serviceScopeRepositoryInstance = poSServiceRepositoryInstance;
    }
}