namespace DbConfigurator.Model.Contracts
{
    public interface IEntity
    {
        public int Id { get; set; }
        public IEntity CreateCopy();
    }
}