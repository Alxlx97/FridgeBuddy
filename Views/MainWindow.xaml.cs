using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BeerTracker.ViewModels;

namespace BeerTracker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    private void btn_delete(object sender, RoutedEventArgs e)
    {
        var deleteResult = MessageBox.Show("Are you sure you want to delete this beer?", "Delete a beer",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (deleteResult is not MessageBoxResult.Yes)
        {
            return;
        }

        if (DataContext is MainViewModel vm && vm.DeleteCommand.CanExecute(null))
        {
            vm.DeleteCommand.Execute(null);
        }
    }
}