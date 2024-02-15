using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

// Klasa zarządzająca logowaniem użytkowników
class LoginManager
{
    private static string loggedInUserEmail; // Zmienna przechowująca adres e-mail zalogowanego użytkownika
    private string connectionString = "Server=localhost;Database=baza_projektowa;Uid=root;Pwd=;"; // Połączenie do bazy danych MySQL

    // Metoda uruchamiająca proces logowania
    public void Run()
    {
        // Wyczyszczenie konsoli i wyświetlenie nagłówka logowania
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("   ╔══════════════════════════════════════════════════╗");
        Console.WriteLine("   ║                  Logowanie                       ║");
        Console.WriteLine("   ╚══════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        // Sprawdzenie, czy użytkownik jest już zalogowany
        if (IsUserLoggedIn())
        {
            Console.WriteLine($"Zalogowano jako: {GetLoggedInUserEmail()}");
            DisplayLoggedInUserInfo();
            Console.WriteLine("");
            List<string> options = new List<string> { "Wyloguj", "Wróć do menu głównego" };
            SubMenu menu = new SubMenu(options);
            int choice = menu.Run();

            // Obsługa wybranej opcji przez użytkownika
            switch (choice)
            {
                case 0:
                    Logout();
                    Console.WriteLine("Zostałeś wylogowany.");
                    break;
                case 1:
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }
        else
        {
            // Jeśli użytkownik nie jest zalogowany, prosi o wprowadzenie adresu e-mail
            Console.Write("Wprowadź swój adres e-mail lub naciśnij Enter, aby użyć przykładowego adresu: ");
            string email = Console.ReadLine();

            // Użyj przykładowego adresu e-mail, jeśli użytkownik naciśnie Enter
            if (string.IsNullOrEmpty(email))
            {
                email = "kamil.szymanski@example.com";
                Console.WriteLine($"Używany przykładowy adres e-mail: {email}");
            }

            // Sprawdzenie, czy użytkownik o podanym adresie e-mail istnieje w bazie danych
            bool userExists = CheckUserExists(email);

            if (userExists)
            {
                // Jeśli użytkownik istnieje, wyświetla informacje o nim
                Console.WriteLine("Znaleziono użytkownika o podanym adresie e-mail:");
                DisplayUserInfo(email);
                loggedInUserEmail = email;
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu głównego...");
                Console.ReadKey();
            }
            else
            {
                // Jeśli użytkownik nie istnieje, wyświetla odpowiedni komunikat
                Console.WriteLine("Nie znaleziono użytkownika o podanym adresie e-mail.");
                Console.WriteLine("Naciśnij dowolny klawisz, aby wrócić do menu głównego...");
                Console.ReadKey();
            }
        }
    }


    // Metoda sprawdzająca istnienie użytkownika o podanym adresie e-mail w bazie danych
    private bool CheckUserExists(string email)
    {
        bool exists = false;

        try
        {
            // Ustanowienie połączenia z bazą danych i wykonanie zapytania sprawdzającego
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dane WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Jeśli zapytanie zwróciło wynik, użytkownik istnieje
                        if (reader.Read())
                        {
                            exists = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas sprawdzania użytkownika w bazie danych: {ex.Message}");
        }

        return exists;
    }

    // Metoda wyświetlająca informacje o użytkowniku o podanym adresie e-mail
    private void DisplayUserInfo(string email)
    {
        try
        {
            // Ustanowienie połączenia z bazą danych i wykonanie zapytania pobierającego informacje o użytkowniku
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dane WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["DaneID"]}");
                            Console.WriteLine($"Imię: {reader["Imie"]}");
                            Console.WriteLine($"Nazwisko: {reader["Nazwisko"]}");
                            Console.WriteLine($"Telefon: {reader["Telefon"]}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania informacji o użytkowniku z bazy danych: {ex.Message}");
        }
    }

    // Metoda wyświetlająca informacje o zalogowanym użytkowniku
    public void DisplayLoggedInUserInfo()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("   ╔════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("   ║                    Informacje o zalogowanym użytkowniku        ║");
        Console.WriteLine("   ╚════════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();
        try
        {
            // Ustanowienie połączenia z bazą danych i wykonanie zapytania pobierającego informacje o zalogowanym użytkowniku
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM dane WHERE Email = @Email";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", GetLoggedInUserEmail());
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine($"ID: {reader["DaneID"]}");
                            Console.WriteLine($"Imię: {reader["Imie"]}");
                            Console.WriteLine($"Nazwisko: {reader["Nazwisko"]}");
                            Console.WriteLine($"Telefon: {reader["Telefon"]}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania informacji o zalogowanym użytkowniku z bazy danych: {ex.Message}");
        }
    }
    // Metoda sprawdzająca, czy użytkownik jest zalogowany
    public static bool IsUserLoggedIn()
    {
        return loggedInUserEmail != null;
    }

    // Metoda zwracająca adres e-mail zalogowanego użytkownika
    public static string GetLoggedInUserEmail()
    {
        return loggedInUserEmail;
    }

    // Metoda wylogowująca użytkownika
    public static void Logout()
    {
        loggedInUserEmail = null;
    }
}