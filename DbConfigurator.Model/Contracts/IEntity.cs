namespace DbConfigurator.Model.Contracts
{
    public interface IEntity
    {
        public int Id { get; }
        public IEntity CreateCopy();
    }
}