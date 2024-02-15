using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

class ShopMenu
{
    // Definicja typu wyliczeniowego reprezentującego opcje w menu sklepu, które użytkownik może wybrać.
    private enum ShopMenuOptions { FuelSystems, BrakeSystems, LubricationAndOilSupply, ExhaustSystems, Electronics, Search, Orders, BackToMainMenu }

    // Tablica opcji w menu głównym sklepu
    private readonly string[] shopMenuOptions = { "Układy paliwowe", "Układy hamulcowe", "Układy smarowania i zasilania oleju", "Układy wydechowe", "Elektronika", "Wyszukiwanie", "Zamówienia", "Powrót do menu głównego" };
    private int shopMenuIndex = 0; // Indeks aktualnie wybranej opcji w menu głównym

    // Słownik opcji podmenu dla każdej opcji głównego menu. Słownik ten przypisuje listę opcji podmenu dla każdej opcji głównego menu w sklepie.
    private readonly Dictionary<string, List<string>> subMenuOptions = new Dictionary<string, List<string>>()
    {
        { "Układy paliwowe", new List<string>() { "Benzynowe", "Diesla" } },
        { "Układy hamulcowe", new List<string>() { "Zaciski hamulcowe", "Przewody hamulcowe" } },
        { "Układy smarowania i zasilania oleju", new List<string>() { "Węże olejowe", "Chłodnice oleju" } },
        { "Układy wydechowe", new List<string>() { "Układ wydechowy" } },
        { "Elektronika", new List<string>() {  "Czujniki"  } },
        { "Wyszukiwanie", new List<string>() },
        { "Zamówienia", new List<string>() },
        { "Powrót do menu głównego", new List<string>() }
    };

    // Metoda uruchamiająca menu sklepu
    public void Run()
    {
        while (true)
        {
            Console.Clear();

            Console.WriteLine(" ╔═══════════════════════════ Sklep ════════════════════════════╗");
            Console.ResetColor();

            // Wyświetlanie opcji w menu głównym
            for (int i = 0; i < shopMenuOptions.Length; i++)
            {
                if (i == shopMenuIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($" ║ {shopMenuOptions[i].PadRight(60)} ║");

                Console.ResetColor();
            }

            Console.WriteLine(" ╚══════════════════════════════════════════════════════════════╝");

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    shopMenuIndex = (shopMenuIndex == 0) ? shopMenuOptions.Length - 1 : shopMenuIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    shopMenuIndex = (shopMenuIndex == shopMenuOptions.Length - 1) ? 0 : shopMenuIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    SelectSubMenuOption((ShopMenuOptions)shopMenuIndex);
                    break;
            }
        }
    }

    // Metoda obsługująca wybór opcji podmenu
    private void SelectSubMenuOption(ShopMenuOptions selectedOption)
    {
        switch (selectedOption)
        {
            case ShopMenuOptions.BackToMainMenu:
                MainMenu mainMenu = new MainMenu();
                mainMenu.Run(); // Uruchamianie menu głównego
                return;

            case ShopMenuOptions.Search:
                SearchParts(); // Wywołanie funkcji wyszukiwania części
                return;

            case ShopMenuOptions.Orders: // Obsługa opcji "Zamówienia"
                if (LoginManager.IsUserLoggedIn())
                {
                    OrderMenu orderMenu = new OrderMenu();
                    orderMenu.Run();
                }
                else
                {
                    // Komunikat informujący o konieczności logowania przed przejściem do zamówień
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" ╔════════════════════════════════════════════════════════════╗");
                    Console.WriteLine(" ║ Aby przejść do zamówień, zaloguj się.                      ║");
                    Console.WriteLine(" ║ Naciśnij dowolny klawisz, aby kontynuować...               ║");
                    Console.WriteLine(" ╚════════════════════════════════════════════════════════════╝");
                    Console.ResetColor();
                    Console.ReadKey();
                }
                return;
        }

        string selectedOptionText = shopMenuOptions[(int)selectedOption];
        if (subMenuOptions.ContainsKey(selectedOptionText))
        {
            ShowSubMenuOptions(subMenuOptions[selectedOptionText]);
        }
        else
        {
            Console.WriteLine($"Wybrano: {selectedOptionText}");
            Console.WriteLine("");
            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }

    // Metoda wyświetlająca opcje podmenu
    private void ShowSubMenuOptions(List<string> options)
    {
        int subMenuIndex = 0;

        while (true)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Wybrano: {shopMenuOptions[shopMenuIndex]}");
            Console.ResetColor();
            Console.WriteLine("");

            for (int i = 0; i < options.Count; i++)
            {
                if (i == subMenuIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                // Wyświetlanie opcji podmenu
                Console.WriteLine($" ║ {options[i].PadRight(60)} ║");

                Console.ResetColor();
            }

            Console.WriteLine(" ╚══════════════════════════════════════════════════════════════╝");

            ConsoleKeyInfo key = Console.ReadKey();


            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    subMenuIndex = (subMenuIndex == 0) ? options.Count - 1 : subMenuIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    subMenuIndex = (subMenuIndex == options.Count - 1) ? 0 : subMenuIndex + 1;
                    break;

                case ConsoleKey.Enter:
                    // Pobieranie części dla wybranej podkategorii
                    string selectedOptionText = options[subMenuIndex];
                    List<(int, string, decimal, int)> parts = GetPartsForSubcategory(selectedOptionText);
                    // Wyświetlanie pobranych części wraz z ich identyfikatorami
                    Console.Clear();
                    Console.WriteLine($"Części dla podkategorii {selectedOptionText}:");
                    Console.WriteLine("");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.WriteLine("|   ID   |                   Nazwa                  |     Cena (zł)    |     Dostępność             |");
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    foreach (var part in parts)
                    {
                        Console.WriteLine($"|{part.Item1,6} | {part.Item2.PadRight(40).Substring(0, 40),-40} | {part.Item3,12:N2} zł | {part.Item4,13} szt           |");
                    }
                    Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                    Console.ReadKey();
                    return;
            }
        }
    }

    // Metoda obsługująca wyszukiwanie części
    private void SearchParts()
    {
        Console.Clear();
        Console.WriteLine("Wyszukiwanie części");
        Console.Write("Wprowadź nazwę części: ");
        string searchQuery = Console.ReadLine();
        Console.WriteLine("");
        string connectionString = "Server=localhost;Database=baza_projektowa;Uid=root;Pwd=;"; // Połączenie z bazą danych

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM czesci WHERE NazwaCzesci LIKE @searchQuery";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@searchQuery", "%" + searchQuery + "%");
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("|   ID   |            Nazwa części            |       Cena (zł)     |");
                        Console.WriteLine("---------------------------------------------------------------------");
                        while (reader.Read())
                        {

                            if (reader["CzescID"] != DBNull.Value && reader["NazwaCzesci"] != DBNull.Value && reader["Cena"] != DBNull.Value)
                            {
                                Console.WriteLine($"| {reader["CzescID"],4} | {reader["NazwaCzesci"].ToString().PadRight(40).Substring(0, 40),-40} | {reader["Cena"],12:N2} zł |");
                            }
                        }
                        Console.WriteLine("---------------------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("");
            Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do poprzedniego menu.");
            Console.ReadKey();
        }
    }

    // Metoda pobierająca części dla danej podkategorii
    private List<(int, string, decimal, int)> GetPartsForSubcategory(string subcategory)
    {
        List<(int, string, decimal, int)> parts = new List<(int, string, decimal, int)>();
        string connectionString = "Server=localhost;Database=baza_projektowa;Uid=root;Pwd=;"; // Połączenie z bazą danych

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CzescID, NazwaCzesci, Cena, Dostepnosc FROM czesci JOIN podkategorie ON czesci.PodkategoriaID = podkategorie.PodkategoriaID WHERE NazwaPodkategorii = @subcategory";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@subcategory", subcategory);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["CzescID"] != DBNull.Value && reader["NazwaCzesci"] != DBNull.Value && reader["Cena"] != DBNull.Value && reader["Dostepnosc"] != DBNull.Value)
                            {
                                parts.Add(((int)reader["CzescID"], reader["NazwaCzesci"].ToString(), (decimal)reader["Cena"], (int)reader["Dostepnosc"]));
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
        return parts;
    }
}

