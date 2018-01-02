using PRIExplorer.ViewModels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PRIExplorer
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainViewModel();

            DataContext = viewModel;

            var args = System.Environment.GetCommandLineArgs();
            if (args.Length >= 2)
            {
                viewModel.OpenPriFile(args[1]);
                Task.Delay(1).GetAwaiter().OnCompleted(() =>
                {
                    if (resourceMapTreeView.Items.Count != 0)
                    {
                        var first = resourceMapTreeView.Items[0];


                        TreeViewItem tvItem = (TreeViewItem)resourceMapTreeView
                                                  .ItemContainerGenerator
                                                  .ContainerFromItem(first);
                        tvItem.Focus();


                    }
                });
                
            }
            
        }

        

        private void resourceMapTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            viewModel.SelectedEntry = (EntryViewModel)e.NewValue;
        }

        private void Window_PreviewDragOver(object sender, DragEventArgs e)
        {
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (paths == null || paths.Length != 1)
                return;

            e.Effects = DragDropEffects.Copy;
            e.Handled = true;
        }

        private void Window_Drop(object sender, DragEventArgs e)
        {
            string[] paths = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (paths == null || paths.Length != 1)
                return;

            viewModel.OpenPriFile(paths[0]);

            e.Handled = true;
        }

        
    }
}
