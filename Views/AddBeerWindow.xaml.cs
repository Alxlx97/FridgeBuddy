using System.Windows;
using BeerTracker.Models;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;
using MessageBoxButton = System.Windows.MessageBoxButton;

namespace BeerTracker.Views;

public partial class AddBeerWindow : Window
{
    public string BeerName { get; private set; } = "";
    public ServingSize ServingSize { get; private set; } = ServingSize.Ml355;
    public PackSize PackSize { get; private set; } = PackSize.Single;

    public AddBeerWindow(string? name = null, ServingSize? servingSize = null, PackSize? packSize = null, bool isEdit = false)
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
        Title = isEdit ? "Modify a beer" :  "Add a beer";
    }

    private void btn_add(object sender, RoutedEventArgs e)
    {
        var name = (NameBox.Text ??  "").Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter a name.", "Adding a beer", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        BeerName = name;
        ServingSize = (ServingSize)(ServingSizeBox.SelectedItem ?? ServingSize.Ml355);
        PackSize = (PackSize)(PackSizeBox.SelectedItem ?? PackSize.Single);
        
        DialogResult = true;
        Close();
    }
}