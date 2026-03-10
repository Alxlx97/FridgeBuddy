using System.Windows;
using BeerTracker.Models;

namespace BeerTracker.Views;

public partial class AddBeerWindow : Window
{
    public string BeerName { get; private set; } = "";
    public ServingSize ServingSize { get; private set; } = ServingSize.Ml355;
    public PackSize PackSize { get; private set; } = PackSize.Single;

    public AddBeerWindow()
    {
        InitializeComponent();
        
        ServingSizeBox.ItemsSource = Enum.GetValues(typeof(ServingSize)).Cast<ServingSize>();
        ServingSizeBox.SelectedItem = ServingSize;
        
        PackSizeBox.ItemsSource = Enum.GetValues(typeof(PackSize)).Cast<PackSize>();
        PackSizeBox.SelectedItem = PackSize;
    }

    private void AddClick(object sender, RoutedEventArgs e)
    {
        var name = (NameBox.Text ??  "").Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Please enter a name.");
            return;
        }
        
        BeerName = name;
        ServingSize = (ServingSize)(ServingSizeBox.SelectedItem ?? ServingSize.Ml355);
        PackSize = (PackSize)(PackSizeBox.SelectedItem ?? PackSize.Single);
        
        DialogResult = true;
        Close();
    }
}