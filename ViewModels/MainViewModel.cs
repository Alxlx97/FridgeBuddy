using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;
using BeerTracker.Models;

namespace BeerTracker.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private int _nextId = 1;
    
    private string _newName = "";
    
    private int _newQuantity = 1;

    private ServingSize _newServingSize = ServingSize.Ml355;

    public ObservableCollection<Beer> Beers { get; } = new();
    
    private Beer? _selectedBeer;

    public Beer? SelectedBeer
    {
        get => _selectedBeer;
        set
        {
            _selectedBeer = value;
            OnPropertyChanged();
            DeleteCommand.RaiseCanExecuteChanged();
        }
    }
    
    public string NewName { get => _newName; set { _newName = value; OnPropertyChanged(); } }
    
    public int NewQuantity { get => _newQuantity; set { _newQuantity = value; OnPropertyChanged(); } }
    
    public ServingSize NewServingSize { get => _newServingSize; set { _newServingSize = value; OnPropertyChanged(); } }
    public ServingSize[] ServingSizes { get; } = System.Enum.GetValues(typeof(ServingSize)).Cast<ServingSize>().ToArray();
    
    public RelayCommand AddCommand { get; }
    public RelayCommand DeleteCommand { get; }

    public MainViewModel()
    {
        AddCommand = new RelayCommand(AddBeer);
        DeleteCommand = new RelayCommand(DeleteBeer, () => SelectedBeer != null);
    }

    private void AddBeer()
    {
        if(string.IsNullOrWhiteSpace(NewName)) return;

        Beers.Add(new Beer(_nextId++, NewName.Trim(), NewQuantity, NewServingSize));

        NewName = "";
        NewQuantity = 1;
        NewServingSize = ServingSize.Ml355;
    }
    
    private void DeleteBeer()
    {
        if (SelectedBeer is null) return;
        
        Beers.Remove(SelectedBeer);
        SelectedBeer = null;
        
        DeleteCommand.RaiseCanExecuteChanged();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}