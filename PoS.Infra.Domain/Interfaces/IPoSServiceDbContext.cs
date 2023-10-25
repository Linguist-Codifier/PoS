namespace PoS.Infra.Domain.Interfaces
{
    public interface IPoSServiceDbContext<PoSServiceRepository> where PoSServiceRepository : IPoSRepository
    {
        public PoSServiceRepository Repository { get; }
    }
}