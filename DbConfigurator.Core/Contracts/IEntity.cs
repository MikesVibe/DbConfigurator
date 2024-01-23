namespace DbConfigurator.Core.Contracts
{
    public interface IEntity
    {
        public int Id { get; set; }
        public IEntity CreateCopy();
    }
}