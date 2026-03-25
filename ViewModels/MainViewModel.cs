using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using FridgeBuddy.Models;
using FridgeBuddy.Services;

namespace FridgeBuddy.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private int _nextId = 1;
    
    private int _drinkAmount = 1;
    
    private readonly DrinkStorage _drinkStorage = new();
    
    private readonly IDrinkDialogService _drinkDialogService;

    private readonly IConfirmDialogService _confirm = new ConfirmDialogService();
    
    public ObservableCollection<Drink> Drinks { get; } = new();
    
    private Drink? _selectedDrink;
    
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

    public Drink? SelectedDrink
    {
        get => _selectedDrink;
        set
        {
            _selectedDrink = value;
            OnPropertyChanged();
            DeleteCommand.RaiseCanExecuteChanged();
            EditCommand.RaiseCanExecuteChanged();
            AddOneCommand.RaiseCanExecuteChanged();
            DrinkOneCommand.RaiseCanExecuteChanged();
            DrinkManyCommand.RaiseCanExecuteChanged();
            RestockCommand.RaiseCanExecuteChanged();
        }
    }

    public MainViewModel() : this(new DrinkStorage(), new DrinkDialogService(), new ConfirmDialogService()) { }

    public MainViewModel(DrinkStorage drinkStorage, IDrinkDialogService drinkDialogService, IConfirmDialogService confirmDialogService)
    {
        _drinkStorage = drinkStorage;
        _drinkDialogService = drinkDialogService;
        _confirm = confirmDialogService;

        foreach (var drink in _drinkStorage.Load())
        {
            if (drink.RestockAmount <= 0)
            {
                drink.RestockAmount = drink.Quantity > 0 ? drink.Quantity : 1;
            }
            
            Drinks.Add(drink);
        }
        
        _nextId = Drinks.Count == 0 ? 1 : Drinks.Max(b => b.Id) + 1;
            
        AddCommand = new RelayCommand(AddDrink);
        DeleteCommand = new RelayCommand(DeleteDrink, () => SelectedDrink != null);
        EditCommand = new RelayCommand(EditDrink, () => SelectedDrink != null);
        AddOneCommand = new RelayCommand(AddOneDrink, () => SelectedDrink != null);
        DrinkOneCommand = new RelayCommand(DrinkOneDrink, () => SelectedDrink != null && SelectedDrink.Quantity > 0);
        DrinkManyCommand = new RelayCommand(DrinkManyDrinks, () => SelectedDrink != null && DrinkAmount > 0);
        RestockCommand = new RelayCommand(RestockDrinks, () => SelectedDrink != null && SelectedDrink.Quantity == 0 && SelectedDrink.RestockAmount > 0);
    }

    private void AddDrink()
    {
        if (!_drinkDialogService.TryShowAddDrinkDialog(out var result)) return;
        
        if (string.IsNullOrWhiteSpace(result.Name)) return;
        
        int packQty = (int)result.PackSize;
        
        var existingDrink = Drinks.FirstOrDefault(b => string.Equals(b.Name, result.Name, StringComparison.OrdinalIgnoreCase) && b.ServingSize == result.ServingSize);

        if (existingDrink is not null)
        {
            existingDrink.Quantity += packQty;
            existingDrink.RestockAmount = packQty;
        }
        else
        {
            Drinks.Add(new Drink()
            {
                Id = _nextId++,
                Name = result.Name,
                Quantity = packQty,
                ServingSize = result.ServingSize,
                RestockAmount = packQty
            });   
        }
        
        _drinkStorage.Save(Drinks);
    }

    private void EditDrink()
    {
        if (SelectedDrink is null) return;

        if (!_drinkDialogService.TryShowEditDrinkDialog(SelectedDrink, out var result)) return;
        
        SelectedDrink.Name = result.Name;
        SelectedDrink.ServingSize = result.ServingSize;
        
        _drinkStorage.Save(Drinks);
    }
    
    private void DeleteDrink()
    {
        if (SelectedDrink is null) return;

        if (!_confirm.Confirm("Are you sure you want to delete this drink?", "Delete a drink"))
            return;
        
        Drinks.Remove(SelectedDrink);
        SelectedDrink = null;
        DeleteCommand.RaiseCanExecuteChanged();
        _drinkStorage.Save(Drinks);
    }

    private void AddOneDrink()
    {
        if (SelectedDrink is null) return;
        
        SelectedDrink.Quantity += 1;
       
        _drinkStorage.Save(Drinks);
       
        AddOneCommand.RaiseCanExecuteChanged();
        DrinkOneCommand.RaiseCanExecuteChanged();
        DrinkManyCommand.RaiseCanExecuteChanged();
        RestockCommand.RaiseCanExecuteChanged();
    }

    private void DrinkOneDrink()
    {
        if (SelectedDrink is null) return;
        if (SelectedDrink.Quantity <= 0) return;
        
        SelectedDrink.Quantity -= 1;
       
        _drinkStorage.Save(Drinks);
        
        AddOneCommand.RaiseCanExecuteChanged();
        DrinkOneCommand.RaiseCanExecuteChanged();
        DrinkManyCommand.RaiseCanExecuteChanged();
        RestockCommand.RaiseCanExecuteChanged();
    }

    private void DrinkManyDrinks()
    {
        if (SelectedDrink is null) return;
        if (DrinkAmount <= 0) return;

        if (DrinkAmount > SelectedDrink.Quantity)
        {
            MessageBox.Show("You can't drink more drinks than the number of drinks available", "Drinking many drinks",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        
        SelectedDrink.Quantity = Math.Max(0, SelectedDrink.Quantity - DrinkAmount);
        _drinkStorage.Save(Drinks);
        
        AddOneCommand.RaiseCanExecuteChanged();
        DrinkOneCommand.RaiseCanExecuteChanged();
        DrinkManyCommand.RaiseCanExecuteChanged();
        RestockCommand.RaiseCanExecuteChanged();
    }

    private void RestockDrinks()
    {
        if (SelectedDrink is null) return;

        SelectedDrink.Quantity = SelectedDrink.RestockAmount;
        
        _drinkStorage.Save(Drinks);
        
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