using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MemLeak
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;
        private readonly Lazy<Task> _work;

        public MainWindow()
        {
            InitializeComponent();

            _work = new Lazy<Task>(DoWork);
            DataContext = this;
        }

        public ObservableCollection<ItemViewModel> Items { get; } = new();

        protected override void OnActivated(EventArgs e)
        {
            _ = _work.Value;

            base.OnActivated(e);
        }

        private async Task DoWork()
        {
            AddItems(200);

            for (var i = 0; i < 2000; i++)
            {
                await Task.Delay(15);

                _dispatcher.Invoke(() => MoveItems(57));
            }
        }

        private void AddItems(int numberOfItems)
        {
            // The list is filled with items
            for (var i = 0; i < numberOfItems; i++)
            {
                Items.Add(new ItemViewModel(i));
            }
        }

        private void MoveItems(int numberOfItems)
        {
            for (var i = 0; i < numberOfItems; i++)
            {
                // Last item is moved to the top of the list
                Items.Move(Items.Count - 1, 0);
            }
        }
    }
}
