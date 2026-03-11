using System.Windows;

namespace BeerTracker.Services;

public class ConfirmDialogService : IConfirmDialogService
{
    public bool Confirm(string message, string title) =>
        MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
}