namespace DbConfigurator.Model.Entities.Wrapper
{
    public interface IWrapper<T>
    {
        public T Model { get; }
    }
}
