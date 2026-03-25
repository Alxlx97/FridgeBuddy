using FridgeBuddy.Views;
using FridgeBuddy.Models;

namespace FridgeBuddy.Services;

public interface IDrinkDialogService
{
    bool TryShowAddDrinkDialog(out AddDrinkResult result);
    bool TryShowEditDrinkDialog(Drink drink, out AddDrinkResult result);
}
     