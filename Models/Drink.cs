using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FridgeBuddy.Models;

public class Drink : INotifyPropertyChanged
{
    private int _id;
    
    private string _name = "";
    
    private int _quantity;

    private ServingSize _servingSize;
    
    private int _restockAmount;

    public int Id
    {
        get => _id; 
        set {_id = value; OnPropertyChanged();}
    }

    public string Name
    {
        get => _name; 
        set{ _name = value; OnPropertyChanged();}
    }

    public int Quantity
    {
        get => _quantity; 
        set {  _quantity = value; OnPropertyChanged();}
    }
    
    public ServingSize ServingSize 
    { 
        get => _servingSize; 
        set {  _servingSize = value; OnPropertyChanged();} 
    }
    
    public int RestockAmount
    {
        get => _restockAmount;
        set {  _restockAmount = value; OnPropertyChanged();}
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
}