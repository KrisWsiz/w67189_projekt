using System;

class Program
{
    static void Main()
    {

        Console.Clear(); // Wyczyszczenie konsoli
        Console.CursorVisible = false;

        // Uruchomienie menu głównego
        new MainMenu().Run();

    }
}
