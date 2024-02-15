using System;
using System.IO;
using MySql.Data.MySqlClient;

// Klasa obsługująca zarządzanie reklamacjami użytkowników
class Reklamacje
{
    private string connectionString = "Server=localhost;Database=baza_projektowa;Uid=root;Pwd=;"; // Połączenie do bazy danych MySQL

    // Metoda uruchamiająca proces zarządzania reklamacjami
    public void Run()
    {
        string userEmail = LoginManager.GetLoggedInUserEmail(); // Pobiera adres e-mail zalogowanego użytkownika

        if (userEmail != null)
        {
            Console.WriteLine($"Zalogowano jako: {userEmail}");
            DisplayUserOrdersAndComplaints(userEmail); // Wyświetlenie zamówień i reklamacji użytkownika
            SubmitComplaintOrExport(userEmail); // Wywołanie metody która uruchamia proces składania reklamacji lub eksportu danych do pliku CSV
        }
        else
        {
            Console.WriteLine("Najpierw musisz się zalogować."); // Komunikat o konieczności zalogowania się
        }

        Console.ReadKey();
    }

    // Metoda wyświetlająca zamówienia i reklamacje użytkownika
    private void DisplayUserOrdersAndComplaints(string userEmail)
    {
        Console.Clear();
        Console.WriteLine(">>> Poniżej jest lista twoich zamówień oraz reklamacji: <<<");
        Console.WriteLine("");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|  ID zamówienia |  Data zamówienia |                   Nazwa części                  |  Ilość | Obsługujący pracownik |");
        Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Zapytanie o zamówienia użytkownika
                string orderQuery = @"SELECT zamowienie.ZamowienieID, zamowienie.DataZamowienia, zamowienie.ilosc,
                                    czesci.NazwaCzesci, dane.Imie AS PracownikImie, dane.Nazwisko AS PracownikNazwisko
                                 FROM zamowienie
                                 INNER JOIN czesci ON zamowienie.czescid = czesci.CzescID
                                 INNER JOIN pracownik ON zamowienie.PracownikID = pracownik.PracownikID
                                 INNER JOIN dane ON pracownik.DaneID = dane.DaneID
                                 WHERE zamowienie.KlientID = (SELECT DaneID FROM dane WHERE Email = @Email)";

                // Wykonanie zapytania o zamówienia
                using (MySqlCommand cmd = new MySqlCommand(orderQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"| {reader["ZamowienieID"],7}      | {reader["DataZamowienia"],16} | {reader["NazwaCzesci"],40}       | {reader["ilosc"],3}    | {reader["PracownikImie"]} {reader["PracownikNazwisko"]} ");
                        }
                    }
                }

                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");

                // Zapytanie o reklamacje użytkownika
                string complaintQuery = @"SELECT ReklamacjaID, Opis, DataZgloszenia, ZamowienieID
                                      FROM reklamacja
                                      WHERE ZamowienieID IN (SELECT ZamowienieID FROM zamowienie
                                                            WHERE KlientID = (SELECT DaneID FROM dane WHERE Email = @Email))";

                // Wykonanie zapytania o reklamacje
                using (MySqlCommand cmd = new MySqlCommand(complaintQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        Console.WriteLine(">>> Twoje reklamacje: <<<");
                        Console.WriteLine("");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                        Console.WriteLine("| ID reklamacji | ID Zamowienia |       Opis reklamacji                         |     Data zgłoszenia     |");
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine($"| {reader["ReklamacjaID"],13} | {reader["ZamowienieID"],7}       | {reader["Opis"],45} | {reader["DataZgloszenia"],23} |");
                        }
                        Console.WriteLine("----------------------------------------------------------------------------------------------------------");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania zamówień i reklamacji z bazy danych: {ex.Message}");
        }
    }

    // Metoda umożliwiająca zgłoszenie reklamacji lub eksport danych do pliku CSV
    private void SubmitComplaintOrExport(string userEmail)
    {
        Console.WriteLine("Czy chcesz złożyć reklamację? (T/N)");
        Console.WriteLine("(Jeżeli chcesz wyeksporotwac informacje o swoich zamowieniach i reklamacjach wybierz N a następnie T)");
        string response = Console.ReadLine().ToUpper();
        if (response == "T")
        {
            SubmitComplaint(userEmail);
        }
        else if (response == "N")
        {
            Console.WriteLine("Czy chcesz wyeksportować informacje o swoich zamówieniach i reklamacjach do pliku CSV? (T/N)");
            string exportResponse = Console.ReadLine().ToUpper();
            if (exportResponse == "T")
            {
                ExportToCSV(userEmail);
            }
            else if (exportResponse == "N")
            {
                Console.WriteLine("Dziękujemy. Do widzenia!");
            }
            else
            {
                Console.WriteLine("Nieprawidłowa odpowiedź. Proszę podać 'T' (tak) lub 'N' (nie).");
            }
        }
        else
        {
            Console.WriteLine("Nieprawidłowa odpowiedź. Proszę podać 'T' (tak) lub 'N' (nie).");
        }
    }

    // Metoda umożliwiająca zgłoszenie reklamacji
    private void SubmitComplaint(string userEmail)
    {
        Console.Write("Podaj ID zamówienia, do którego chcesz złożyć reklamację: ");
        int orderId = Convert.ToInt32(Console.ReadLine());

        if (CheckIfOrderExists(orderId, userEmail)) // Sprawdzenie istnienia zamówienia o podanym ID dla obecnie zalogowanego klienta
        {
            Console.Write("Opisz reklamację: ");
            string description = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(description)) // Sprawdzenie czy opis reklamacji nie jest pusty
            {
                AddComplaintToDatabase(orderId, description); // Dodanie reklamacji do bazy danych
                Console.WriteLine("Reklamacja została złożona pomyślnie.");
                Console.WriteLine("Naciśnij dowolny klawisz aby powrócić do menu głównego ...");
            }
            else
            {
                Console.WriteLine("Opis reklamacji nie może być pusty.");
            }
        }
        else
        {
            Console.WriteLine("Nie ma zamówienia o podanym ID.");
        }
    }

    // Metoda sprawdzająca istnienie zamówienia o podanym ID
    private bool CheckIfOrderExists(int orderId, string userEmail)
    {
        bool orderExists = false;
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT COUNT(*) FROM zamowienie 
                             WHERE ZamowienieID = @OrderId AND KlientID = (SELECT DaneID FROM dane WHERE Email = @Email)";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    orderExists = count > 0;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas sprawdzania istnienia zamówienia: {ex.Message}");
        }
        return orderExists;
    }

    // Metoda dodająca reklamację do bazy danych
    private void AddComplaintToDatabase(int orderId, string description)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO reklamacja (ZamowienieID, Opis, DataZgloszenia) VALUES (@OrderId, @Description, NOW())";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@OrderId", orderId);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas dodawania reklamacji do bazy danych: {ex.Message}");
        }
    }

    // Metoda eksportująca informacje o zamówieniach i reklamacjach do pliku CSV
    private void ExportToCSV(string userEmail)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        string fileName = $"{userEmail}OrdersAndComplaints.csv";
        string filePath = Path.Combine(desktopPath, fileName);

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT * FROM ( 
                            SELECT zamowienie.ZamowienieID, zamowienie.DataZamowienia, zamowienie.ilosc,
                            czesci.NazwaCzesci, dane.Imie AS PracownikImie, dane.Nazwisko AS PracownikNazwisko
                            FROM zamowienie
                            INNER JOIN czesci ON zamowienie.czescid = czesci.CzescID
                            INNER JOIN pracownik ON zamowienie.PracownikID = pracownik.PracownikID
                            INNER JOIN dane ON pracownik.DaneID = dane.DaneID
                            WHERE zamowienie.KlientID = (SELECT DaneID FROM dane WHERE Email = @Email)
                            UNION 
                            SELECT null AS ZamowienieID, null AS DataZamowienia, null AS ilosc,
                            null AS NazwaCzesci, null AS PracownikImie, null AS PracownikNazwisko
                            FROM dual
                            UNION
                            SELECT ReklamacjaID, null AS DataZamowienia, null AS ilosc,
                            Opis AS NazwaCzesci, null AS PracownikImie, null AS PracownikNazwisko
                            FROM reklamacja
                            WHERE ZamowienieID IN (SELECT ZamowienieID FROM zamowienie
                                                  WHERE KlientID = (SELECT DaneID FROM dane WHERE Email = @Email))
                        ) AS user_data
                        ORDER BY DataZamowienia DESC";

                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", userEmail);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        using (StreamWriter file = new StreamWriter(filePath))
                        {
                            while (reader.Read())
                            {
                                file.WriteLine($"{reader["ZamowienieID"]},{reader["DataZamowienia"]},{reader["NazwaCzesci"]},{reader["ilosc"]},{reader["PracownikImie"]} {reader["PracownikNazwisko"]}");
                            }
                        }
                    }
                }

                Console.WriteLine($"Informacje zostały wyeksportowane do pliku CSV: {filePath}");
                Console.WriteLine("Nacisnij dowolny klawisz aby powrocic ...");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas eksportowania danych: {ex.Message}");
        }
    }

}
