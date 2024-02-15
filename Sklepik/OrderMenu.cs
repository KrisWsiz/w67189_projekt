using MySql.Data.MySqlClient;
using System;

// Klasa obsługująca menu zamówień
class OrderMenu
{
    private enum OrderMenuOptions { MojeZamowienia, ZlozZamowienie, Wroc }
    private readonly string[] options = { "Moje zamówienia", "Złóż zamówienie", "Wróć do sklepu" };
    private int selectedOptionIndex = 0;
    private string connectionString = "Server=localhost;Database=baza_projektowa;Uid=root;Pwd=;";

    // Metoda uruchamiająca menu zamówień
    public void Run()
    {
        ConsoleKeyInfo key;

        do
        {
            Console.Clear();
            Console.WriteLine(" >>> Zamówienia <<<");
            Console.WriteLine();

            // Wyświetlanie opcji menu

            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedOptionIndex)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(" ║ " + options[i].PadRight(60) + " ║");

                Console.ResetColor();
            }

            Console.WriteLine(" ╚══════════════════════════════════════════════════════════════╝");

            key = Console.ReadKey();

            // Obsługa poruszania się po menu

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedOptionIndex = (selectedOptionIndex == 0) ? options.Length - 1 : selectedOptionIndex - 1;
                    break;

                case ConsoleKey.DownArrow:
                    selectedOptionIndex = (selectedOptionIndex == options.Length - 1) ? 0 : selectedOptionIndex + 1;
                    break;
            }
        } while (key.Key != ConsoleKey.Enter);

        // Obsługa wybranej opcji menu

        HandleOrderMenuOption(selectedOptionIndex);
    }

    // Metoda obsługująca wybraną opcję menu zamówień
    private void HandleOrderMenuOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case (int)OrderMenuOptions.MojeZamowienia:
                Console.WriteLine($"Zalogowano jako: {LoginManager.GetLoggedInUserEmail()}");
                DisplayLoggedInUserOrders(LoginManager.GetLoggedInUserEmail());
                break;

            case (int)OrderMenuOptions.ZlozZamowienie:
                Console.Clear();
                Console.WriteLine(">>> Złóż zamówienie <<<");
                Console.WriteLine();

                Console.Write("ID części: ");
                int partId;
                if (!ValidateInput(Console.ReadLine(), out partId))
                {
                    Console.WriteLine("Błąd: Nieprawidłowe ID części.");
                    Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                    break;
                }

                Console.Write("Ilość: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                // Sprawdzenie dostępności części
                if (CheckPartAvailability(partId, quantity))
                {
                    // Automatyczne pobranie ID klienta (zalogowanego użytkownika)
                    int clientId = GetClientId(LoginManager.GetLoggedInUserEmail());

                    // Automatyczne pobranie ID pracownika
                    int employeeId = GetRandomEmployeeId();

                    // Automatyczne pobranie daty zamówienia
                    DateTime orderDate = DateTime.Now;

                    // Dodanie zamówienia do bazy danych
                    AddOrderToDatabase(partId, quantity, clientId, employeeId, orderDate);

                    Console.WriteLine("Zamówienie zostało złożone.");
                }
                else
                {
                    Console.WriteLine("Brak wymaganej ilości części na stanie.");
                }

                Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
                Console.ReadKey();
                break;
        }

        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey();
    }

    // Metoda wyświetlająca zamówienia zalogowanego użytkownika
    private void DisplayLoggedInUserOrders(string email)
    {
        Console.Clear();
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  ID zamówienia |  Data zamówienia |                   Nazwa części                  |  Ilość | Obsługujący pracownik |");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT zamowienie.ZamowienieID, zamowienie.DataZamowienia, zamowienie.ilosc,
                                    czesci.NazwaCzesci, dane.Imie AS PracownikImie, dane.Nazwisko AS PracownikNazwisko
                             FROM zamowienie
                             INNER JOIN czesci ON zamowienie.czescid = czesci.CzescID
                             INNER JOIN pracownik ON zamowienie.PracownikID = pracownik.PracownikID
                             INNER JOIN dane ON pracownik.DaneID = dane.DaneID
                             WHERE zamowienie.KlientID = (SELECT DaneID FROM dane WHERE Email = @Email)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"| {reader["ZamowienieID"],7}      | {reader["DataZamowienia"],16} | {reader["NazwaCzesci"],40}       | {reader["ilosc"],3}    | {reader["PracownikImie"]} {reader["PracownikNazwisko"]} ");
                        }
                    }
                }
            }

            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania zamówień z bazy danych: {ex.Message}");
        }
    }

    // Metoda sprawdzająca dostępność części na stanie
    private bool CheckPartAvailability(int partId, int quantity)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Dostepnosc FROM czesci WHERE CzescID = @PartId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PartId", partId);
                    int availableQuantity = Convert.ToInt32(cmd.ExecuteScalar());

                    return availableQuantity >= quantity;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas sprawdzania dostępności części: {ex.Message}");
            return false;
        }
    }

    // metoda reazlizujaca skladanie zamowienia
    private void AddOrderToDatabase(int partId, int quantity, int clientId, int employeeId, DateTime orderDate)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Rozpocznij transakcję
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Wybór losowego dostawcy
                        int supplierId = GetRandomSupplierId();

                        // Obliczenie daty dostawy jako tydzień później niż data zamówienia
                        DateTime deliveryDate = orderDate.AddDays(7);

                        // Obliczenie ceny zamówienia
                        decimal totalPrice = CalculateTotalPrice(partId, quantity);

                        // Odejmowanie zamówionej ilości części z dostępnych zapasów
                        string updateQuery = @"UPDATE czesci SET Dostepnosc = Dostepnosc - @Quantity WHERE CzescID = @PartId";
                        using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            cmd.Parameters.AddWithValue("@PartId", partId);
                            cmd.ExecuteNonQuery();
                        }

                        // Wstawienie zamówienia do tabeli zamowienie
                        string orderQuery = @"INSERT INTO zamowienie (KlientID, PracownikID, DataZamowienia, czescid, ilosc) 
                                         VALUES (@ClientId, @EmployeeId, @OrderDate, @PartId, @Quantity);
                                         SELECT LAST_INSERT_ID();"; // Zwraca ostatnio wstawione ID zamówienia

                        int orderId;
                        using (MySqlCommand cmd = new MySqlCommand(orderQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ClientId", clientId);
                            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                            cmd.Parameters.AddWithValue("@OrderDate", orderDate);
                            cmd.Parameters.AddWithValue("@PartId", partId);
                            cmd.Parameters.AddWithValue("@Quantity", quantity);
                            orderId = Convert.ToInt32(cmd.ExecuteScalar()); // Pobiera ostatnio wstawione ID zamówienia
                        }

                        // Wstawienie dostawy do tabeli dostawa
                        string deliveryQuery = @"INSERT INTO dostawa (ZamowienieID, Data_Dostawy, DostawcaID) 
                                             VALUES (@OrderId, @DeliveryDate, @SupplierId);";
                        using (MySqlCommand cmd = new MySqlCommand(deliveryQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.Parameters.AddWithValue("@DeliveryDate", deliveryDate);
                            cmd.Parameters.AddWithValue("@SupplierId", supplierId);
                            cmd.ExecuteNonQuery();
                        }

                        // Wstawienie płatności do tabeli platnosc
                        string paymentQuery = @"INSERT INTO platnosc (ZamowienieID, Kwota) 
                                            VALUES (@OrderId, @TotalPrice);";
                        using (MySqlCommand cmd = new MySqlCommand(paymentQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@OrderId", orderId);
                            cmd.Parameters.AddWithValue("@TotalPrice", totalPrice);
                            cmd.ExecuteNonQuery();
                        }

                        // Zatwierdź transakcję
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // W przypadku błędu, cofnij transakcję
                        transaction.Rollback();
                        Console.WriteLine($"Błąd podczas dodawania zamówienia do bazy danych: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas dodawania zamówienia do bazy danych: {ex.Message}");
        }
    }

    private decimal CalculateTotalPrice(int partId, int quantity)
    {
        // Pobranie ceny czesci z bazy danych
        decimal partPrice = GetPartPriceFromDatabase(partId);

        // Obliczenie całkowitej ceny
        decimal totalPrice = partPrice * quantity;

        return totalPrice;
    }

    // Metoda pobierająca cenę części z bazy danych
    private decimal GetPartPriceFromDatabase(int partId)
    {
        decimal partPrice = 0;

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Cena FROM czesci WHERE CzescID = @PartId";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@PartId", partId);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        partPrice = Convert.ToDecimal(result);
                    }
                    else
                    {
                        Console.WriteLine("Nie znaleziono ceny części o podanym ID.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania ceny części z bazy danych: {ex.Message}");
        }

        return partPrice;
    }

    // Metoda losująca ID dostawcy
    private int GetRandomSupplierId()
    {
        Random random = new Random();
        return random.Next(1, 4); // losowanie id dostawcy
    }

    // Metoda losująca ID pracownika
    private int GetRandomEmployeeId()
    {
        Random random = new Random();
        return random.Next(1, 7); // Losuje ID pracownika z przedziału 1-6
    }

    // Metoda walidująca wprowadzone dane wejściowe
    private bool ValidateInput(string input, out int value)
    {
        return int.TryParse(input, out value);
    }

    // Metoda do pobrania ID klienta zalogowanego użytkownika
    private int GetClientId(string email)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT DaneID FROM dane WHERE Email = @Email";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania ID klienta: {ex.Message}");
            return -1; // Wartość domyślna w przypadku błędu
        }
    }
}

