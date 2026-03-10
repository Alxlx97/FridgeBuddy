using System.IO;
using System.Text.Json;
using BeerTracker.Models;

namespace BeerTracker.Services;

public class BeerStorage
{
    private readonly string _filePath;
    
    public BeerStorage(string? filePath = null)
    {
        _filePath = string.IsNullOrWhiteSpace(filePath) ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Beers.json") : filePath;
    }

    public List<Beer> Load()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Beer>();
        }
        
        var json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Beer>>(json) ?? new List<Beer>();
    }

    public void Save(IEnumerable<Beer> beers)
    {
        var json = JsonSerializer.Serialize(beers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}