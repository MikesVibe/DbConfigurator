using DbConfigurator.Model.Entities.Table;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DbConfigurator.Model.Entities.Wrapper.Table
{
    public class TableItemWrapper<T> : ITableItemWrapper where T : ITableItem
    {

        public ITableItem Model { get; }

        public int Id { get; set; }

        public TableItemWrapper(ITableItem model)
        {
            Model = model;
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName] string propertyName = null)
        {
            typeof(T).GetProperty(propertyName)?.SetValue(Model, value);
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
