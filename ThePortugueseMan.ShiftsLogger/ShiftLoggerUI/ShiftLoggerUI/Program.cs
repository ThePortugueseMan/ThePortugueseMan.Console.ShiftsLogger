using Screens;

internal class Program
{
    private static void Main(string[] args)
    {
        var mainMenu = new Screens.MainMenu();
        mainMenu.View();
        Console.Clear();
        Console.WriteLine("Goodbye!");
    }
}