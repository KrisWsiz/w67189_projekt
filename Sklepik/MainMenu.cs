using System;

class MainMenu
{
    // Definicja opcji w menu głównym
    private enum MainMenuOptions { Logowanie, Sklep, Reklamacje, Wyjscie }

    // Metoda uruchamiająca menu główne
    public void Run()
    {
        // Ustawienie początkowej opcji na "Logowanie"
        MainMenuOptions selectedOption = MainMenuOptions.Logowanie;

        // Pętla nieskończona dla interakcji użytkownika z menu
        while (true)
        {
            // Wyczyszczenie konsoli i wyświetlenie nagłówka
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ╔════════════════════════════════════════╗");
            Console.WriteLine(" ║     Sklep z częściami samochodowymi    ║");
            Console.WriteLine(" ╚════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("");

            // Wyświetlenie informacji o zalogowanym użytkowniku, jeśli taki istnieje
            string userEmail = LoginManager.GetLoggedInUserEmail();
            if (userEmail != null)
            {
                Console.WriteLine($" Zalogowano jako: {userEmail}");
            }

            Console.WriteLine("");

            // Wyświetlenie opcji menu
            foreach (MainMenuOptions option in Enum.GetValues(typeof(MainMenuOptions)))
            {
                if (option == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"  {option}");

                Console.ResetColor();
            }

            Console.WriteLine("");
            Console.WriteLine("  Użyj strzałek do góry/dół, aby poruszać się po menu, a ENTER aby wybrać.");

            // Odczytanie klawisza naciśniętego przez użytkownika
            ConsoleKeyInfo key = Console.ReadKey();

            // Obsługa akcji użytkownika w zależności od naciśniętego klawisza
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOption = selectedOption == MainMenuOptions.Logowanie ? MainMenuOptions.Wyjscie : selectedOption - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedOption = selectedOption == MainMenuOptions.Wyjscie ? MainMenuOptions.Logowanie : selectedOption + 1;
                    break;

                case ConsoleKey.Enter:
                    // Wyjście z programu lub obsługa wybranej opcji
                    if (selectedOption == MainMenuOptions.Wyjscie)
                    {
                        Environment.Exit(0); // Wyjście z programu
                    }
                    else
                    {
                        HandleMainMenuOption(selectedOption); // Obsługa wybranej opcji
                    }
                    break;
            }
        }
    }

    // Metoda obsługująca wybrane opcje menu głównego
    private void HandleMainMenuOption(MainMenuOptions option)
    {
        switch (option)
        {
            // Uruchomienie menu sklepu
            case MainMenuOptions.Sklep:
                ShopMenu shopMenu = new ShopMenu();
                shopMenu.Run();
                break;

            // Uruchomienie menu logowania
            case MainMenuOptions.Logowanie:
                LoginManager loginMenu = new LoginManager();
                loginMenu.Run();
                break;

            // Uruchomienie menu reklamacji, jeśli użytkownik jest zalogowany
            case MainMenuOptions.Reklamacje:
                Reklamacje reklamacjeMenu = new Reklamacje();
                reklamacjeMenu.Run();
                break;

                // Tutaj można dodać obsługę innych opcji w menu głównym
        }
    }
}
