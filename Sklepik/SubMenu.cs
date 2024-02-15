using System;
using System.Collections.Generic;

// Klasa reprezentująca podmenu do logowania
class SubMenu
{
    private readonly List<string> options; // Lista opcji w podmenu

    // Konstruktor inicjalizujący podmenu daną listą opcji
    public SubMenu(List<string> options)
    {
        this.options = options;
    }

    // Metoda uruchamiająca podmenu i zwracająca indeks wybranej opcji
    public int Run()
    {
        int subMenuIndex = 0; // Indeks aktualnie wybranej opcji w podmenu

        // Pętla nieskończona dla interakcji użytkownika z podmenu
        while (true)
        {
            // Wyczyszczenie konsoli i wyświetlenie nagłówka podmenu
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   ╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("   ║                           Menu logowania                       ║");
            Console.WriteLine($"   ║            Zalogowano jako: {LoginManager.GetLoggedInUserEmail()}        ║");
            Console.WriteLine("   ╚════════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();

            // Wyświetlenie opcji w podmenu
            for (int i = 0; i < options.Count; i++)
            {
                if (i == subMenuIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(options[i]);

                Console.ResetColor();
            }

            // Odczytanie klawisza naciśniętego przez użytkownika
            ConsoleKeyInfo key = Console.ReadKey();

            // Obsługa akcji użytkownika w zależności od naciśniętego klawisza
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    subMenuIndex = (subMenuIndex == 0) ? options.Count - 1 : subMenuIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    subMenuIndex = (subMenuIndex == options.Count - 1) ? 0 : subMenuIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    return subMenuIndex; // Zwrócenie indeksu wybranej opcji
            }
        }
    }
}
