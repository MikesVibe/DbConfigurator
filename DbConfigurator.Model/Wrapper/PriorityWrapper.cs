namespace DbConfigurator.Model.Wrapper
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
                SetValue<int>(value);
            }
        }
        public string Name
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue<string>(value);
            }
        }
    }
}
