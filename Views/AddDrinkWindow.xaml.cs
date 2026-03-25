using System.Windows;
using FridgeBuddy.Models;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace FridgeBuddy.Views;

public partial class AddDrinkWindow : Window
{
    public string DrinkName { get; private set; } = "";
    public ServingSize ServingSize { get; private set; } = ServingSize.Ml355;
    public PackSize PackSize { get; private set; } = PackSize.Single;

    public AddDrinkWindow(string? name = null, ServingSize? servingSize = null, PackSize? packSize = null, bool isEdit = false)
    {
        InitializeComponent();
            
        ServingSizeBox.ItemsSource = Enum.GetValues(typeof(ServingSize)).Cast<ServingSize>();
        ServingSizeBox.SelectedItem = ServingSize;
        
        PackSizeBox.ItemsSource = Enum.GetValues(typeof(PackSize)).Cast<PackSize>();
        PackSizeBox.SelectedItem = PackSize;
        
        NameBox.Text = name ?? "";
        
        var initialSize = servingSize ?? ServingSize.Ml355;
        ServingSizeBox.SelectedItem = initialSize;
        
        var initialPackSize = packSize ?? PackSize.Single;
        PackSizeBox.SelectedItem = initialPackSize;
        
        AddButton.Content = isEdit ? "Modify" : "Add";
        Title = isEdit ? "Modify a drink" :  "Add a drink";
    }

    private void btn_add(object sender, RoutedEventArgs e)
    {
        var name = (NameBox.Text ??  "").Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter a name.", "Adding a drink", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        DrinkName = name;
        ServingSize = (ServingSize)(ServingSizeBox.SelectedItem ?? ServingSize.Ml355);
        PackSize = (PackSize)(PackSizeBox.SelectedItem ?? PackSize.Single);
        
        DialogResult = true;
        Close();
    }
}