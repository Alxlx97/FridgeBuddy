using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using BeerTracker.Models;
using BeerTracker.Services;

namespace BeerTracker.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private int _nextId = 1;
    
    private string _newName = "";
    
    private int _newQuantity = 1;
    
    private int _drinkAmount = 1;

    private ServingSize _newServingSize = ServingSize.Ml355;

    private PackSize _newPackSize = PackSize.Single;
    
    private readonly BeerStorage _beerStorage = new();
    
    private readonly IBeerDialogService _beerDialogService;

    private readonly IConfirmDialogService _confirm = new ConfirmDialogService();
    
    public ObservableCollection<Beer> Beers { get; } = new();
    
    private Beer? _selectedBeer;
    
    public PackSize[] PackSizes { get; } = Enum.GetValues(typeof(PackSize)).Cast<PackSize>().ToArray();
    
    public RelayCommand AddCommand { get; }
    
    public RelayCommand EditCommand { get; }
    
    public RelayCommand DeleteCommand { get; }
    
    public RelayCommand AddOneCommand { get; }
    
    public RelayCommand DrinkOneCommand { get; }
    
    public RelayCommand DrinkManyCommand { get; }
    
    public RelayCommand RestockCommand { get; }

    public int DrinkAmount
    {
        get => _drinkAmount;
        set
        {
            _drinkAmount = value;
            OnPropertyChanged();
            DrinkManyCommand.RaiseCanExecuteChanged();
        }
    }

    public Beer? SelectedBeer
    {
        get => _selectedBeer;
        set
        {
            _selectedBeer = value;
            OnPropertyChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            AddOneCommand.RaiseCanExecuteChanged();
            DrinkOneCommand.RaiseCanExecuteChanged();
            DrinkManyCommand.RaiseCanExecuteChanged();
            RestockCommand.RaiseCanExecuteChanged();
        }
    }
    
    public string NewName { get => _newName; set { _newName = value; OnPropertyChanged(); } }
    
    public int NewQuantity { get => _newQuantity; set { _newQuantity = value; OnPropertyChanged(); } }
    
    public ServingSize NewServingSize { get => _newServingSize; set { _newServingSize = value; OnPropertyChanged(); } }
    public ServingSize[] ServingSizes { get; } = Enum.GetValues(typeof(ServingSize)).Cast<ServingSize>().ToArray();

    public PackSize NewPackSize
    {
        get => _newPackSize;
        set
        {
            _newPackSize = value;
            OnPropertyChanged();
        }
    }
    
    public MainViewModel() : this(new BeerStorage(), new BeerDialogService(),  new ConfirmDialogService()) { }

    public MainViewModel(BeerStorage beerStorage, IBeerDialogService beerDialogService, IConfirmDialogService confirmDialogService)
    {
        
        _beerStorage = beerStorage;
        _beerDialogService = beerDialogService;
        _confirm = confirmDialogService;

        foreach (var beer in _beerStorage.Load())
        {
            if (beer.RestockAmount <= 0)
            {
                beer.RestockAmount = beer.Quantity > 0 ? beer.Quantity : 1;
            }
            
            Beers.Add(beer);
        }
          
            
        _nextId = Beers.Count == 0 ? 1 : Beers.Max(b => b.Id) + 1;
            
        AddCommand = new RelayCommand(AddBeer);
        DeleteCommand = new RelayCommand(DeleteBeer, () => SelectedBeer != null);
        EditCommand = new RelayCommand(EditBeer, () => SelectedBeer != null);
        AddOneCommand = new RelayCommand(AddOneBeer, () => SelectedBeer != null);
        DrinkOneCommand = new RelayCommand(DrinkOneBeer, () => SelectedBeer != null && SelectedBeer.Quantity > 0);
        DrinkManyCommand = new RelayCommand(DrinkManyBeers, () => SelectedBeer != null && DrinkAmount > 0);
        RestockCommand = new RelayCommand(RestockBeer, () => SelectedBeer != null && SelectedBeer.Quantity == 0 && SelectedBeer.RestockAmount > 0);
    }

    private void AddBeer()
    {
        if (!_beerDialogService.TryShowAddBeerDialog(out var result)) return;
        
        if (string.IsNullOrWhiteSpace(result.Name)) return;
        
        int packQty = (int)result.PackSize;
        
        var existingBeer = Beers.FirstOrDefault(b => string.Equals(b.Name, result.Name, StringComparison.OrdinalIgnoreCase) && b.ServingSize == result.ServingSize);

        if (existingBeer is not null)
        {
            existingBeer.Quantity += packQty;
            existingBeer.RestockAmount = packQty;
        }
        else
        {
            Beers.Add(new Beer()
            {
                Id = _nextId++,
                Name = result.Name,
                Quantity = packQty,
                ServingSize = result.ServingSize,
                RestockAmount = packQty
            });   
        }
        
        _beerStorage.Save(Beers);
    }

    private void EditBeer()
    {
        if (SelectedBeer is null) return;

        if (!_beerDialogService.TryShowEditBeerDialog(SelectedBeer, out var result)) return;
        
        SelectedBeer.Name = result.Name;
        SelectedBeer.ServingSize = result.ServingSize;
        
        _beerStorage.Save(Beers);
    }
    
    private void DeleteBeer()
    {
        if (SelectedBeer is null) return;

        if (!_confirm.Confirm("Are you sure you want to delete this beer?", "Delete a beer"))
            return;
        
        Beers.Remove(SelectedBeer);
        SelectedBeer = null;
        DeleteCommand.RaiseCanExecuteChanged();
        _beerStorage.Save(Beers);
    }

    private void AddOneBeer()
    {
        if (SelectedBeer is null) return;
        
        SelectedBeer.Quantity += 1;
       
       _beerStorage.Save(Beers);
       
       AddOneCommand.RaiseCanExecuteChanged();
       DrinkOneCommand.RaiseCanExecuteChanged();
       DrinkManyCommand.RaiseCanExecuteChanged();
       RestockCommand.RaiseCanExecuteChanged();
    }

    private void DrinkOneBeer()
    {
        if (SelectedBeer is null) return;
        if (SelectedBeer.Quantity <= 0) return;
        
        SelectedBeer.Quantity -= 1;
       
        _beerStorage.Save(Beers);
        
        AddOneCommand.RaiseCanExecuteChanged();
        DrinkOneCommand.RaiseCanExecuteChanged();
        DrinkManyCommand.RaiseCanExecuteChanged();
        RestockCommand.RaiseCanExecuteChanged();
    }

    private void DrinkManyBeers()
    {
        if (SelectedBeer is null) return;
        if(DrinkAmount <= 0) return;

        if (DrinkAmount > SelectedBeer.Quantity)
        {
            MessageBox.Show("You can't drink more beers than the number of beers available", "Drinking many beers",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        SelectedBeer.Quantity = Math.Max(0, SelectedBeer.Quantity - DrinkAmount);
        _beerStorage.Save(Beers);
        
        AddOneCommand.RaiseCanExecuteChanged();
        DrinkOneCommand.RaiseCanExecuteChanged();
        DrinkManyCommand.RaiseCanExecuteChanged();
        RestockCommand.RaiseCanExecuteChanged();
    }

    private void RestockBeer()
    {
        if (SelectedBeer is null) return;

        SelectedBeer.Quantity = SelectedBeer.RestockAmount;
        
        _beerStorage.Save(Beers);
        
        AddOneCommand.RaiseCanExecuteChanged();
        DrinkOneCommand.RaiseCanExecuteChanged();
        DrinkManyCommand.RaiseCanExecuteChanged();
        RestockCommand.RaiseCanExecuteChanged();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}