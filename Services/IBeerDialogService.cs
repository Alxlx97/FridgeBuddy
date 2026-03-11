using BeerTracker.Models;
using BeerTracker.Views;

namespace BeerTracker.Services;

public interface IBeerDialogService
{
    bool TryShowAddBeerDialog(out AddBeerResult result);
    bool TryShowEditBeerDialog(Beer beer, out AddBeerResult result);
}
     