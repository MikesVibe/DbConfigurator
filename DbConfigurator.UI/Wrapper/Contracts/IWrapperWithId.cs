namespace DbConfigurator.Model.Contracts
{
    //public interface IWrapperWithId<TEntity>
    public interface IWrapperWithId
    {
        public int Id { get; }
        //public TEntity Entity { get; }
        public IEntity Entity { get; }
    }
}
