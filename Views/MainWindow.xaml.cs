using System.Windows;
using BeerTracker.Services;
using BeerTracker.ViewModels;
using Wpf.Ui.Controls;

namespace BeerTracker;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(new BeerStorage(), new BeerDialogService(), new ConfirmDialogService());
    }
}