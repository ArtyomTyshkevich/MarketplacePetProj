namespace MarketplacePetProj.Service.Interfaces
{
    public interface IBacket
    {
        public Task BacketAdd(int id);
        public Task BacketRead();
        public Task BacketDelete();
    }
}
