using DbConfigurator.Model.Entities.Core;

namespace DbConfigurator.Model.Entities.Wrapper
{
    public class PriorityWrapper : ModelWrapper<Priority>
    {
        public PriorityWrapper(Priority model) : base(model)
        {
        }

        public int Id
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }
    }
}
