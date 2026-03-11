using BeerTracker.Models;
using BeerTracker.Views;

namespace BeerTracker.Services;

public class BeerDialogService : IBeerDialogService
{
    public bool TryShowAddBeerDialog(out AddBeerResult result)
    {
            var dlg = new AddBeerWindow(isEdit: false);
            bool? ok = dlg.ShowDialog();
            
            if (ok is true)
            {
                result = new AddBeerResult(dlg.BeerName, dlg.ServingSize, dlg.PackSize);
                return true;
            }

            result = default!;
            return false;
        
    }

    public bool TryShowEditBeerDialog(Beer beer, out AddBeerResult result)
    {
        var dlg = new AddBeerWindow(beer.Name, beer.ServingSize, PackSize.Single, isEdit: true);
        bool? ok = dlg.ShowDialog();
            
        if (ok is true)
        {
            result = new AddBeerResult(dlg.BeerName, dlg.ServingSize, dlg.PackSize);
            return true;
        }

        result = default!;
        return false;
    }
}