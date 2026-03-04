using System.ComponentModel;

namespace BeerTracker.Models;

public enum ServingSize
{
    Ml355 = 355,
    Ml500 = 500,
    Ml750 = 750,
    Ml1000 = 1000
}

public class Beer
{
    private int _id;
    
    private string _name;
    
    private int _quantity;

    private ServingSize _servingSize;

    public int Id => _id;
    
    public string Name { get => _name; set => _name = value; }
    
    public int Quantity { get => _quantity; set => _quantity = value; }
    
    public ServingSize ServingSize { get => _servingSize; set => _servingSize = value; }

    public Beer(int id, string name, int quantity, ServingSize servingSize)
    {
        _id = id;
        _name = name;
        _quantity = quantity;
        _servingSize = servingSize;
    }
    
}