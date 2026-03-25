using System.Windows;
using FridgeBuddy.Services;
using FridgeBuddy.ViewModels;
using Wpf.Ui.Controls;

namespace FridgeBuddy;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel(new DrinkStorage(), new DrinkDialogService(), new ConfirmDialogService());
    }
}