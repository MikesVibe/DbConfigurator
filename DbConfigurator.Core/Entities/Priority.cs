using  DbConfigurator.Core.Contracts;

namespace  DbConfigurator.Core.Entities
{
    public class Priority : IEntity
    {
        //[Required]
        public int Id { get; set; }

        //[Required]
        //[MaxLength(6)]
        public string Name { get; set; }
        public int Value { get; set; }

        public IEntity CreateCopy()
        {
            return new Priority { Id = Id, Name = Name, Value = Value};
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
