using System.IO;
using System.Text.Json;
using FridgeBuddy.Models;

namespace FridgeBuddy.Services;

public class DrinkStorage
{
    private readonly string _filePath;
    
    public DrinkStorage(string? filePath = null)
    {
        if (!string.IsNullOrWhiteSpace(filePath))
        {
            _filePath = filePath;
            return;
        }
        
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FridgeBuddy");
        
        Directory.CreateDirectory(folder);
        _filePath = Path.Combine(folder, "drinks.json");
    }

    public List<Drink> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Drink>();
        }
        
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Drink>>(json) ?? new List<Drink>();
    }

    public void Save(IEnumerable<Drink> drinks)
    {
        var json = JsonSerializer.Serialize(drinks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}