using FridgeBuddy.Models;
using FridgeBuddy.Views;

namespace FridgeBuddy.Services;

public class DrinkDialogService : IDrinkDialogService
{
    public bool TryShowAddDrinkDialog(out AddDrinkResult result)
    {
            var dlg = new AddDrinkWindow(isEdit: false);
            bool? ok = dlg.ShowDialog();
            
            if (ok is true)
            {
                result = new AddDrinkResult(dlg.DrinkName, dlg.ServingSize, dlg.PackSize);
                return true;
            }

            result = default!;
            return false;
        
    }

    public bool TryShowEditDrinkDialog(Drink drink, out AddDrinkResult result)
    {
        var dlg = new AddDrinkWindow(drink.Name, drink.ServingSize, PackSize.Single, isEdit: true);
        bool? ok = dlg.ShowDialog();
            
        if (ok is true)
        {
            result = new AddDrinkResult(dlg.DrinkName, dlg.ServingSize, dlg.PackSize);
            return true;
        }

        result = default!;
        return false;
    }
}