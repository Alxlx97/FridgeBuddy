using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Linq;
using System.Windows.Documents;
using BeerTracker.Models;
using BeerTracker.Services;

namespace BeerTracker.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private int _nextId = 1;
    
    private string _newName = "";
    
    private int _newQuantity = 1;

    private ServingSize _newServingSize = ServingSize.Ml355;

    private PackSize _newPackSize = PackSize.Single;
    
    private readonly BeerStorage _beerStorage = new();
    
    private readonly IBeerDialogService _beerDialogService;

    private readonly IConfirmDialogService _confirm = new ConfirmDialogService();
    
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
            EditCommand.RaiseCanExecuteChanged();
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
    
    public PackSize[] PackSizes { get; } = Enum.GetValues(typeof(PackSize)).Cast<PackSize>().ToArray();
    
    public RelayCommand AddCommand { get; }
    
    public RelayCommand EditCommand { get; }
    
    public RelayCommand DeleteCommand { get; }
    
    public MainViewModel() : this(new BeerStorage(), new BeerDialogService(),  new ConfirmDialogService()) { }

    public MainViewModel(BeerStorage beerStorage, IBeerDialogService beerDialogService, IConfirmDialogService confirmDialogService)
    {
        
        _beerStorage = beerStorage;
        _beerDialogService = beerDialogService;
        _confirm = confirmDialogService;
        
        foreach (var beer in _beerStorage.Load())
            Beers.Add(beer);
            
        _nextId = Beers.Count == 0 ? 1 : Beers.Max(b => b.Id) + 1;
            
        AddCommand = new RelayCommand(AddBeer);
        DeleteCommand = new RelayCommand(DeleteBeer, () => SelectedBeer != null);
        EditCommand = new RelayCommand(EditBeer, () => SelectedBeer != null);
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
        }
        else
        {
            Beers.Add(new Beer()
            {
                Id = _nextId++,
                Name = result.Name,
                Quantity = packQty,
                ServingSize = result.ServingSize
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
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}