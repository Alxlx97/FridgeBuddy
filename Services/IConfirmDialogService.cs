namespace FridgeBuddy.Services;

public interface IConfirmDialogService
{
    bool Confirm(string message, string title);
}