using System.ComponentModel;

namespace MemLeak
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        public ItemViewModel(int value)
        {
            Value = value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Value { get; }
    }
}
