-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 14, 2024 at 10:11 AM
-- Wersja serwera: 10.4.32-MariaDB
-- Wersja PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `baza_projektowa`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `adres`
--

CREATE TABLE `adres` (
  `AdresID` int(11) NOT NULL,
  `Ulica` varchar(100) DEFAULT NULL,
  `Miasto` varchar(50) DEFAULT NULL,
  `KodPocztowy` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `adres`
--

INSERT INTO `adres` (`AdresID`, `Ulica`, `Miasto`, `KodPocztowy`) VALUES
(1, 'Aleja Kwiatowa 123', 'Warszawa', '01-234'),
(2, 'Ul. Leśna 45', 'Kraków', '30-567'),
(3, 'Plac Słoneczny 8', 'Gdańsk', '80-001'),
(4, 'Ul. Polna 70', 'Poznań', '60-789'),
(5, 'Aleja Różana 21', 'Łódź', '90-123'),
(6, 'Plac Solny 5', 'Wrocław', '50-432'),
(7, 'Ul. Lipowa 87', 'Katowice', '40-876'),
(8, 'Rynek Główny 15', 'Kraków', '30-456'),
(9, 'Aleja Dębowa 34', 'Warszawa', '01-876'),
(10, 'Plac Zielony 12', 'Gdańsk', '80-345'),
(11, 'Ul. Brzozowa 56', 'Poznań', '60-234'),
(12, 'Rynek Nowy 9', 'Łódź', '90-567'),
(13, 'Aleja Miodowa 3', 'Wrocław', '50-678'),
(14, 'Ul. Akacjowa 67', 'Katowice', '40-123'),
(15, 'Plac Klonowy 23', 'Kraków', '30-789'),
(16, 'Ul. Słowiańska 11', 'Warszawa', '01-345'),
(17, 'Rynek Stary 44', 'Gdańsk', '80-678'),
(18, 'Aleja Lipcowa 78', 'Poznań', '60-001'),
(19, 'Plac Jasny 32', 'Łódź', '90-456'),
(20, 'Ul. Klonowa 9', 'Wrocław', '50-234'),
(21, 'Aleja Lipowa 123', 'Warszawa', '00-001'),
(22, 'Ul. Kwiatowa 45', 'Kraków', '30-123'),
(23, 'Pl. Słoneczny 67', 'Gdańsk', '80-456'),
(24, 'Os. Radosne 89', 'Poznań', '60-789'),
(25, 'Ul. Kolorowa 12', 'Łódź', '90-234'),
(26, 'Pl. Zabawki 34', 'Wrocław', '50-567'),
(27, 'Os. Tęczowe 56', 'Katowice', '40-890'),
(28, 'Ul. Sportowa 78', 'Kraków', '30-123'),
(29, 'Pl. Zielone 90', 'Gdańsk', '80-456'),
(30, 'Aleja Morska 21', 'Warszawa', '00-001'),
(31, 'Ul. Księżycowa 43', 'Poznań', '60-789'),
(32, 'Pl. Górski 65', 'Łódź', '90-234'),
(33, 'Os. Miodowe 87', 'Wrocław', '50-567'),
(34, 'Aleja Malinowa 11', 'Katowice', '40-890'),
(35, 'Pl. Różane 33', 'Warszawa', '00-001'),
(36, 'Ul. Słowiańska 76', 'Kraków', '30-123'),
(37, 'Os. Dębowe 99', 'Gdańsk', '80-456'),
(38, 'Aleja Spacerowa 54', 'Poznań', '60-789'),
(39, 'Pl. Żabki 88', 'Łódź', '90-234'),
(40, 'Ul. Radosna 22', 'Wrocław', '50-567');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `czesci`
--

CREATE TABLE `czesci` (
  `CzescID` int(11) NOT NULL,
  `NazwaCzesci` varchar(100) NOT NULL,
  `Cena` decimal(10,2) NOT NULL,
  `Dostepnosc` int(11) DEFAULT NULL,
  `PodkategoriaID` int(11) NOT NULL,
  `GwarancjaID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `czesci`
--

INSERT INTO `czesci` (`CzescID`, `NazwaCzesci`, `Cena`, `Dostepnosc`, `PodkategoriaID`, `GwarancjaID`) VALUES
(1, 'Wtrysk benzynowy', 120.00, 5, 1, 1),
(2, 'Pompa paliwa', 250.00, 1, 1, 2),
(3, 'Filtr paliwa', 30.00, 20, 1, 4),
(4, 'Czujnik poziomu paliwa', 45.00, 12, 1, 3),
(21, 'Przewody ham. w oplocie', 180.00, 25, 3, 1),
(22, 'Zaciski ham. przednie brembo', 520.00, 20, 3, 4),
(23, 'Zaciski ham. tylnie brembo', 330.00, 30, 3, 2),
(24, 'Płyn hamulcowy', 15.00, 38, 3, 2),
(25, 'Zestaw naprawczy zacisku', 50.00, 15, 3, 3),
(31, 'Przewód olejowy 1000mm', 70.00, 35, 5, 1),
(32, 'Przewód olejowy w oplocie 1000mm', 120.00, 49, 5, 4),
(33, 'Adapter pod czujnik ciśnienia i temperatury oleju', 70.00, 20, 5, 2),
(34, 'Złączka do przewodów olejowych', 18.00, 30, 5, 4),
(35, 'Chłodnica oleju 16-rzędowa', 160.00, 15, 5, 2),
(41, 'Tłumik końcowy', 150.00, 10, 7, 4),
(42, 'Katalizator uniwersalny fi60', 200.00, 8, 7, 4),
(43, 'Rura wydechowa 1mb fi60', 40.00, 15, 7, 4),
(44, 'Flansza wydechu fi60', 8.00, 25, 7, 4),
(45, 'Wieszak tłumika', 12.00, 20, 7, 4),
(51, 'Sterownik silnika sportowy', 1300.00, 5, 8, 2),
(52, 'Przepustnica elektroniczna 60mm', 80.00, 12, 8, 3),
(53, 'Przepustnica elektroniczna 90mm', 120.00, 8, 8, 3),
(54, 'Mapsensor 2b', 50.00, 15, 8, 2),
(55, 'Mapsensor 4b', 15.00, 20, 8, 2);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `dane`
--

CREATE TABLE `dane` (
  `DaneID` int(11) NOT NULL,
  `Imie` varchar(50) DEFAULT NULL,
  `Nazwisko` varchar(50) DEFAULT NULL,
  `Telefon` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `dane`
--

INSERT INTO `dane` (`DaneID`, `Imie`, `Nazwisko`, `Telefon`, `Email`) VALUES
(1, 'Adam', 'Kowalski', '+48 123 456 789', 'adam.kowalski@example.com'),
(2, 'Anna', 'Nowak', '+48 987 654 321', 'anna.nowak@example.com'),
(3, 'Piotr', 'Jankowski', '+48 555 123 789', 'piotr.jankowski@example.com'),
(4, 'Katarzyna', 'Lis', '+48 111 222 333', 'katarzyna.lis@example.com'),
(5, 'Jan', 'Wójcik', '+48 444 555 666', 'jan.wojcik@example.com'),
(6, 'Ewa', 'Zając', '+48 777 888 999', 'ewa.zajac@example.com'),
(7, 'Marek', 'Kamiński', '+48 333 444 555', 'marek.kaminski@example.com'),
(8, 'Agnieszka', 'Wrońska', '+48 999 888 777', 'agnieszka.wronska@example.com'),
(9, 'Michał', 'Pawłowski', '+48 555 444 333', 'michal.pawlowski@example.com'),
(10, 'Monika', 'Kowalczyk', '+48 777 666 555', 'monika.kowalczyk@example.com'),
(11, 'Tomasz', 'Lewandowski', '+48 111 333 555', 'tomasz.lewandowski@example.com'),
(12, 'Magdalena', 'Krawczyk', '+48 777 999 111', 'magdalena.krawczyk@example.com'),
(13, 'Kamil', 'Szymański', '+48 555 777 999', 'kamil.szymanski@example.com'),
(14, 'Natalia', 'Olszewska', '+48 111 666 222', 'natalia.olszewska@example.com'),
(15, 'Marcin', 'Piotrowski', '+48 888 333 777', 'marcin.piotrowski@example.com'),
(16, 'Karolina', 'Jabłońska', '+48 555 999 666', 'karolina.jablonska@example.com'),
(17, 'Robert', 'Nowicki', '+48 111 222 444', 'robert.nowicki@example.com'),
(18, 'Dominika', 'Malinowska', '+48 777 555 333', 'dominika.malinowska@example.com'),
(19, 'Artur', 'Sikora', '+48 999 555 888', 'artur.sikora@example.com'),
(20, 'Patrycja', 'Górska', '+48 444 666 999', 'patrycja.gorska@example.com'),
(21, 'Łukasz', 'Pawlak', '+48 111 888 555', 'lukasz.pawlak@example.com'),
(22, 'Izabela', 'Witkowska', '+48 555 666 999', 'izabela.witkowska@example.com'),
(23, 'Rafał', 'Kozłowski', '+48 777 888 999', 'rafal.kozlowski@example.com'),
(24, 'Alicja', 'Zielińska', '+48 111 333 555', 'alicja.zielinska@example.com'),
(25, 'Szymon', 'Woźniak', '+48 444 555 666', 'szymon.wozniak@example.com'),
(26, 'Daria', 'Jastrzębska', '+48 777 888 999', 'daria.jastrzebska@example.com'),
(27, 'Oskar', 'Szewczyk', '+48 333 444 555', 'oskar.szewczyk@example.com'),
(28, 'Zuzanna', 'Głowacka', '+48 999 888 777', 'zuzanna.glowacka@example.com'),
(29, 'Krzysztof', 'Baran', '+48 555 444 333', 'krzysztof.baran@example.com'),
(30, 'Agata', 'Tomczyk', '+48 777 666 555', 'agata.tomczyk@example.com'),
(31, 'Sebastian', 'Nowakowski', '+48 111 333 555', 'sebastian.nowakowski@example.com'),
(32, 'Justyna', 'Czarnecka', '+48 777 999 111', 'justyna.czarnecka@example.com'),
(33, 'Grzegorz', 'Kaczmarek', '+48 555 777 999', 'grzegorz.kaczmarek@example.com'),
(34, 'Wiktoria', 'Lipińska', '+48 111 666 222', 'wiktoria.lipinska@example.com'),
(35, 'Paweł', 'Ostrowski', '+48 888 333 777', 'pawel.ostrowski@example.com'),
(36, 'Nikola', 'Kozak', '+48 555 999 666', 'nikola.kozak@example.com'),
(37, 'Mateusz', 'Cieślak', '+48 111 222 444', 'mateusz.cieslak@example.com'),
(38, 'Laura', 'Duda', '+48 777 555 333', 'laura.duda@example.com'),
(39, 'Bartosz', 'Grabowski', '+48 999 555 888', 'bartosz.grabowski@example.com'),
(40, 'Klaudia', 'Krajewska', '+48 444 666 999', 'klaudia.krajewska@example.com');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `dostawa`
--

CREATE TABLE `dostawa` (
  `DostawaID` int(11) NOT NULL,
  `ZamowienieID` int(11) DEFAULT NULL,
  `Data_Dostawy` date DEFAULT NULL,
  `DostawcaID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `dostawa`
--

INSERT INTO `dostawa` (`DostawaID`, `ZamowienieID`, `Data_Dostawy`, `DostawcaID`) VALUES
(1, 101, '2024-02-15', 1),
(2, 102, '2024-02-16', 2),
(3, 103, '2024-02-17', 1),
(4, 104, '2024-02-18', 3),
(5, 105, '2024-02-19', 2),
(6, 106, '2024-02-20', 2),
(7, 107, '2024-02-21', 1),
(8, 108, '2024-02-22', 1),
(9, 109, '2024-02-23', 2),
(10, 110, '2024-02-24', 3),
(11, 111, '2024-02-25', 2),
(12, 112, '2024-02-26', 2),
(13, 113, '2024-02-14', 2),
(14, 114, '2024-02-14', 2),
(15, 116, '2024-02-14', 1),
(16, 117, '2024-02-14', 2),
(17, 118, '2024-02-14', 1),
(18, 121, '2024-02-14', 1),
(19, 122, '2024-02-14', 1),
(20, 123, '2024-02-15', 3),
(21, 124, '2024-02-15', 1),
(22, 125, '2024-02-15', 2),
(23, 126, '2024-02-15', 3),
(24, 127, '2024-02-19', 3);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `dostawca`
--

CREATE TABLE `dostawca` (
  `DostawcaID` int(11) NOT NULL,
  `Nazwa` varchar(100) DEFAULT NULL,
  `ulica` varchar(50) NOT NULL,
  `miasto` varchar(50) NOT NULL,
  `kod_pocztowy` varchar(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `dostawca`
--

INSERT INTO `dostawca` (`DostawcaID`, `Nazwa`, `ulica`, `miasto`, `kod_pocztowy`) VALUES
(1, 'Inpost', 'Mostowa 23', 'Wrocław', '01-223'),
(2, 'DHL', 'Zamkowa 14', 'Kraków', '22-121'),
(3, 'Pocztex', 'Złota 120', 'Warszawa', '26-021');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `faktura`
--

CREATE TABLE `faktura` (
  `FakturaID` int(11) NOT NULL,
  `ZamowienieID` int(11) DEFAULT NULL,
  `Kwota` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `faktura`
--

INSERT INTO `faktura` (`FakturaID`, `ZamowienieID`, `Kwota`) VALUES
(1, 101, 120.50),
(2, 102, 90.25),
(3, 103, 150.75),
(4, 104, 200.00),
(5, 105, 75.50);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `gwarancja`
--

CREATE TABLE `gwarancja` (
  `GwarancjaID` int(11) NOT NULL,
  `Okres_gwarancji` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `gwarancja`
--

INSERT INTO `gwarancja` (`GwarancjaID`, `Okres_gwarancji`) VALUES
(1, 0),
(2, 1),
(3, 2),
(4, 3);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `kategorie`
--

CREATE TABLE `kategorie` (
  `KategoriaID` int(11) NOT NULL,
  `NazwaKategorii` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `kategorie`
--

INSERT INTO `kategorie` (`KategoriaID`, `NazwaKategorii`) VALUES
(1, 'Układy paliwowe'),
(2, 'Układy hamulcowe'),
(3, 'Układy smarowania i zasilania oleju'),
(4, 'Układy wydechowe'),
(5, 'Elektronika');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `klient`
--

CREATE TABLE `klient` (
  `KlientID` int(11) NOT NULL,
  `DaneID` int(11) DEFAULT NULL,
  `AdresID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `klient`
--

INSERT INTO `klient` (`KlientID`, `DaneID`, `AdresID`) VALUES
(1, 8, 8),
(2, 8, 8),
(3, 9, 9),
(4, 10, 10),
(5, 11, 11),
(6, 12, 12),
(7, 13, 13),
(8, 14, 14),
(9, 15, 15),
(10, 16, 16),
(11, 17, 17),
(12, 18, 18),
(13, 19, 19),
(14, 20, 20),
(15, 21, 21),
(16, 22, 22),
(17, 23, 23),
(18, 24, 24),
(19, 25, 25),
(20, 26, 26),
(21, 27, 27),
(22, 28, 28),
(23, 29, 29),
(24, 30, 30),
(25, 31, 31),
(26, 32, 32),
(27, 33, 33),
(28, 34, 34),
(29, 35, 35),
(30, 36, 36),
(31, 37, 37),
(32, 38, 38),
(33, 39, 39),
(34, 40, 40),
(35, 7, 7);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `platnosc`
--

CREATE TABLE `platnosc` (
  `PlatnoscID` int(11) NOT NULL,
  `ZamowienieID` int(11) DEFAULT NULL,
  `Kwota` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `platnosc`
--

INSERT INTO `platnosc` (`PlatnoscID`, `ZamowienieID`, `Kwota`) VALUES
(1, 106, 200.00),
(2, 107, 120.00),
(3, 108, 120.00),
(4, 109, 360.00),
(5, 110, 1040.00),
(6, 111, 1300.00),
(7, 112, 50.00),
(8, 113, 120.00),
(9, 114, 750.00),
(10, 116, 60.00),
(11, 117, 250.00),
(12, 118, 120.00),
(13, 121, 240.00),
(14, 122, 600.00),
(15, 123, 30.00),
(16, 124, 120.00),
(17, 125, 250.00),
(18, 126, 250.00),
(19, 127, 1250.00);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `podkategorie`
--

CREATE TABLE `podkategorie` (
  `PodkategoriaID` int(11) NOT NULL,
  `NazwaPodkategorii` varchar(50) DEFAULT NULL,
  `KategoriaID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `podkategorie`
--

INSERT INTO `podkategorie` (`PodkategoriaID`, `NazwaPodkategorii`, `KategoriaID`) VALUES
(1, 'Benzynowe', 1),
(2, 'Diesla', 1),
(3, 'Zaciski hamulcowe', 2),
(4, 'Przewody hamulcowe', 2),
(5, 'Węże olejowe', 3),
(6, 'Chłodnice oleju', 3),
(7, 'Układ wydechowy', 4),
(8, 'Czujniki', 5);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `pracownik`
--

CREATE TABLE `pracownik` (
  `PracownikID` int(11) NOT NULL,
  `DaneID` int(11) NOT NULL,
  `AdresID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `pracownik`
--

INSERT INTO `pracownik` (`PracownikID`, `DaneID`, `AdresID`) VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 3),
(4, 4, 4),
(5, 5, 5),
(6, 6, 6);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `recenzje_i_opis`
--

CREATE TABLE `recenzje_i_opis` (
  `czescid` int(11) NOT NULL,
  `recenzja` text NOT NULL,
  `opis` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `recenzje_i_opis`
--

INSERT INTO `recenzje_i_opis` (`czescid`, `recenzja`, `opis`) VALUES
(1, 'Produkt dziala bez zarzutu', 'Wtrysk do silnikow benzynowych bosch 221wasfa, 620cc'),
(2, 'Pompa dziala cicho i wydajnie', 'wydajnosc 265LPH przy 40 PSI, ponad 200 lph @ 80PSI Cicha i niezawodna, pracuje z etanolem, Min napiecie 6V, Max napiecie 18V'),
(3, '', 'Numer katalogowy: PP836 wyskosc [mm]: 156 srednica zewn [mm]: 81,5'),
(4, 'Dziala szybko i poprawnie', 'Zasilanie: 12V / 24V Zakres opornosci: 0-190 ohm ( EU )srednica pokrywy: 68 mm / wyskosc 22 mm srednica plywaka: 35 mm srednica otworow monntazowych: 5mm Rozstaw otworow montazowych: co 30 mm (5 otworow - standard SAE)Material‚: Stal nierdzewna, sruby nierdzewne i uszczelka w komplecie Typ sensora / czujnika: plywakowy na 5 srub M5'),
(21, '', 'Przewod hamulcowy racing forge 2 W OPLOCIE 120CM SREBRNY dlugosc: 120CM ZAKUTE'),
(22, '', 'Zestaw fabrycznie nowych, 4ro tloczkowych zaciskow firmy BREMBO. Doskonale sprawdzaja w takich samochodach jak:BMW E30, E36, E46, E39, E28 itp,VAG  mk3, mk4, mk5, mk6, mk7, Scirocco, Passat, Audi A3/S3, itp, Zaciski mozna wykorzystac z tarczami o srednicy 310 do 345mm.Maksymalna grubosc tarczy to 31mm.'),
(23, '', 'srednica tloka: 52 mm  60 mm Producent : brembo z hamulcem recznym'),
(24, 'Odczuwalna roznica po wymianie', '100% syntetyczny plyn do hamulcow, Bardzo wysoka temperatura wrzenia: 336Â° Specyfikacja: FMVSS 116 DOT 4, SAE J1703 Posiada certyfikat: ISO 4925 Pojemnosc: 500ml 0,5L'),
(25, 'Dobra jakosc', 'Zestaw naprawczy srednica tloczka 60mm'),
(31, '', 'Przewod AN8 + skrecane koncowki'),
(32, '', 'waz gumowy przewod do paliwa i oleju zewn oplot stalowy 11mm srednica wewn/zewn wyjscia 11/16mm'),
(33, 'bardzo dobra jakosc', 'Adapter posiada 4 otwory na czujnik ze standardowym gwintem 1/8 NPT. W zestawie znajduje sie 1 sztuka adaptera ( do wyboru):M20,nM22,3/4 UNF Waga gabarytowa w gramach: 86.67'),
(34, '', 'M14x1,5'),
(35, '', 'rdzen-wymiary: 260x125x50mm calkowite wymiary 330x125x50mm gwint: meski 7/8 cala AN10'),
(41, 'Troche glosniejszy niz oryginalny', 'Tlumik srednica wyj/wej 60/55mm'),
(42, 'Spelnia zadanie', 'Katalizator uniwersalny o zlaczach 60mm spelniajacy norme euro 4, 200cpsi'),
(43, '', 'Rura fi60mm, nierdzewna, scianka 1.5mm'),
(44, '', 'Flansza nierdzewna, grubosc 6mm, fi60'),
(45, '', 'Wieszak pod drut 6mm'),
(51, 'Dziala bezawaryjnie od 3 lat', '4 niezalezne mapy 16x16 (mapa paliwa, dwie mapy PWM, mapa zaplonu),8 map korekcji 16x1,2 automatyczna konwersja z MAP to MAF z wykorzystanie algorytmu uczenia,procedura startowa'),
(52, '', 'Producent: BOSCH Marka: Kod producenta: 0 280 750 473 Numer katalogowy czesci: 0 280 750 473 Waga: 1.051 kg'),
(53, '', 'Producent: BOSCH Marka: Kod producenta: 0 280 750 455 Numer katalogowy czesci: 0 280 750 455 Waga: 1.1 kg'),
(54, '', 'ZAKRES DZIAĹANIA: -1 DO 2 BAR TYP: MPX4250P'),
(55, '', 'ZAKRES DZIAĹANIA: -2 DO 4 BAR TYP: MPX400XX4P');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `reklamacja`
--

CREATE TABLE `reklamacja` (
  `ReklamacjaID` int(11) NOT NULL,
  `ZamowienieID` int(11) DEFAULT NULL,
  `Opis` text DEFAULT NULL,
  `DataZgloszenia` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `reklamacja`
--

INSERT INTO `reklamacja` (`ReklamacjaID`, `ZamowienieID`, `Opis`, `DataZgloszenia`) VALUES
(1, 102, 'Pompa daje za niskie ciśnienie', '2024-02-19'),
(2, 107, 'Zaciski zacinaja sie', '2024-02-07'),
(3, 107, 'zaciski zacinaja sie, tloczki zablokowane', '2024-02-07'),
(4, 117, 'nie dziala', '2024-02-08'),
(5, 113, 'wtrysk leje', '2024-02-12'),
(6, 121, 'Wtrysk jest niesprawny', '2024-02-12');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `zamowienie`
--

CREATE TABLE `zamowienie` (
  `ZamowienieID` int(11) NOT NULL,
  `KlientID` int(11) NOT NULL,
  `PracownikID` int(11) NOT NULL,
  `DataZamowienia` date NOT NULL,
  `czescid` int(11) NOT NULL,
  `ilosc` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `zamowienie`
--

INSERT INTO `zamowienie` (`ZamowienieID`, `KlientID`, `PracownikID`, `DataZamowienia`, `czescid`, `ilosc`) VALUES
(101, 7, 1, '2024-02-15', 1, 2),
(102, 8, 2, '2024-02-16', 2, 1),
(103, 9, 3, '2024-02-17', 3, 3),
(104, 10, 4, '2024-02-18', 4, 1),
(105, 11, 5, '2024-02-19', 21, 2),
(106, 12, 6, '2024-02-20', 22, 1),
(107, 13, 1, '2024-02-21', 23, 3),
(108, 14, 2, '2024-02-22', 24, 1),
(109, 15, 3, '2024-02-23', 25, 2),
(110, 16, 4, '2024-02-24', 31, 1),
(111, 17, 5, '2024-02-25', 32, 2),
(112, 18, 6, '2024-02-26', 33, 1),
(113, 13, 6, '2024-02-07', 1, 1),
(114, 13, 4, '2024-02-07', 2, 3),
(116, 13, 2, '2024-02-07', 3, 2),
(117, 13, 5, '2024-02-07', 2, 1),
(118, 13, 1, '2024-02-07', 1, 1),
(121, 13, 3, '2024-02-07', 1, 2),
(122, 13, 3, '2024-02-07', 1, 5),
(123, 13, 5, '2024-02-08', 24, 2),
(124, 13, 5, '2024-02-08', 32, 1),
(125, 13, 1, '2024-02-08', 2, 1),
(126, 13, 1, '2024-02-08', 2, 1),
(127, 13, 3, '2024-02-12', 2, 5);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `adres`
--
ALTER TABLE `adres`
  ADD PRIMARY KEY (`AdresID`),
  ADD UNIQUE KEY `AdresID` (`AdresID`);

--
-- Indeksy dla tabeli `czesci`
--
ALTER TABLE `czesci`
  ADD PRIMARY KEY (`CzescID`),
  ADD UNIQUE KEY `CzescID` (`CzescID`,`PodkategoriaID`),
  ADD UNIQUE KEY `CzescID_2` (`CzescID`,`PodkategoriaID`,`GwarancjaID`),
  ADD KEY `PodkategoriaID` (`PodkategoriaID`),
  ADD KEY `czesci_ibfk_2` (`GwarancjaID`);

--
-- Indeksy dla tabeli `dane`
--
ALTER TABLE `dane`
  ADD PRIMARY KEY (`DaneID`);

--
-- Indeksy dla tabeli `dostawa`
--
ALTER TABLE `dostawa`
  ADD PRIMARY KEY (`DostawaID`),
  ADD KEY `ZamowienieID` (`ZamowienieID`);

--
-- Indeksy dla tabeli `dostawca`
--
ALTER TABLE `dostawca`
  ADD PRIMARY KEY (`DostawcaID`);

--
-- Indeksy dla tabeli `faktura`
--
ALTER TABLE `faktura`
  ADD PRIMARY KEY (`FakturaID`),
  ADD KEY `ZamowienieID` (`ZamowienieID`);

--
-- Indeksy dla tabeli `gwarancja`
--
ALTER TABLE `gwarancja`
  ADD PRIMARY KEY (`GwarancjaID`);

--
-- Indeksy dla tabeli `kategorie`
--
ALTER TABLE `kategorie`
  ADD PRIMARY KEY (`KategoriaID`);

--
-- Indeksy dla tabeli `klient`
--
ALTER TABLE `klient`
  ADD PRIMARY KEY (`KlientID`),
  ADD UNIQUE KEY `KlientID` (`KlientID`,`DaneID`,`AdresID`),
  ADD KEY `AdresID` (`AdresID`),
  ADD KEY `DaneID` (`DaneID`);

--
-- Indeksy dla tabeli `platnosc`
--
ALTER TABLE `platnosc`
  ADD PRIMARY KEY (`PlatnoscID`),
  ADD KEY `ZamowienieID` (`ZamowienieID`);

--
-- Indeksy dla tabeli `podkategorie`
--
ALTER TABLE `podkategorie`
  ADD PRIMARY KEY (`PodkategoriaID`),
  ADD KEY `KategoriaID` (`KategoriaID`);

--
-- Indeksy dla tabeli `pracownik`
--
ALTER TABLE `pracownik`
  ADD PRIMARY KEY (`PracownikID`),
  ADD UNIQUE KEY `PracownikID` (`PracownikID`),
  ADD KEY `pracownik_ibfk_2` (`AdresID`),
  ADD KEY `pracownik_ibfk_1` (`DaneID`);

--
-- Indeksy dla tabeli `recenzje_i_opis`
--
ALTER TABLE `recenzje_i_opis`
  ADD PRIMARY KEY (`czescid`);

--
-- Indeksy dla tabeli `reklamacja`
--
ALTER TABLE `reklamacja`
  ADD PRIMARY KEY (`ReklamacjaID`),
  ADD KEY `ZamowienieID` (`ZamowienieID`);

--
-- Indeksy dla tabeli `zamowienie`
--
ALTER TABLE `zamowienie`
  ADD PRIMARY KEY (`ZamowienieID`),
  ADD KEY `czescid` (`czescid`),
  ADD KEY `KlientID` (`KlientID`),
  ADD KEY `PracownikID` (`PracownikID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `dostawa`
--
ALTER TABLE `dostawa`
  MODIFY `DostawaID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT for table `faktura`
--
ALTER TABLE `faktura`
  MODIFY `FakturaID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `platnosc`
--
ALTER TABLE `platnosc`
  MODIFY `PlatnoscID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `reklamacja`
--
ALTER TABLE `reklamacja`
  MODIFY `ReklamacjaID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `zamowienie`
--
ALTER TABLE `zamowienie`
  MODIFY `ZamowienieID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=128;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `czesci`
--
ALTER TABLE `czesci`
  ADD CONSTRAINT `czesci_ibfk_1` FOREIGN KEY (`PodkategoriaID`) REFERENCES `podkategorie` (`PodkategoriaID`),
  ADD CONSTRAINT `czesci_ibfk_2` FOREIGN KEY (`GwarancjaID`) REFERENCES `gwarancja` (`GwarancjaID`);

--
-- Constraints for table `dostawa`
--
ALTER TABLE `dostawa`
  ADD CONSTRAINT `dostawa_ibfk_1` FOREIGN KEY (`ZamowienieID`) REFERENCES `zamowienie` (`ZamowienieID`);

--
-- Constraints for table `dostawca`
--
ALTER TABLE `dostawca`
  ADD CONSTRAINT `dostawca_ibfk_1` FOREIGN KEY (`DostawcaID`) REFERENCES `dostawa` (`DostawaID`);

--
-- Constraints for table `faktura`
--
ALTER TABLE `faktura`
  ADD CONSTRAINT `faktura_ibfk_1` FOREIGN KEY (`ZamowienieID`) REFERENCES `zamowienie` (`ZamowienieID`);

--
-- Constraints for table `klient`
--
ALTER TABLE `klient`
  ADD CONSTRAINT `klient_ibfk_1` FOREIGN KEY (`AdresID`) REFERENCES `adres` (`AdresID`),
  ADD CONSTRAINT `klient_ibfk_2` FOREIGN KEY (`DaneID`) REFERENCES `dane` (`DaneID`);

--
-- Constraints for table `platnosc`
--
ALTER TABLE `platnosc`
  ADD CONSTRAINT `platnosc_ibfk_1` FOREIGN KEY (`ZamowienieID`) REFERENCES `zamowienie` (`ZamowienieID`);

--
-- Constraints for table `podkategorie`
--
ALTER TABLE `podkategorie`
  ADD CONSTRAINT `podkategorie_ibfk_1` FOREIGN KEY (`KategoriaID`) REFERENCES `kategorie` (`KategoriaID`);

--
-- Constraints for table `pracownik`
--
ALTER TABLE `pracownik`
  ADD CONSTRAINT `pracownik_ibfk_1` FOREIGN KEY (`DaneID`) REFERENCES `dane` (`DaneID`),
  ADD CONSTRAINT `pracownik_ibfk_2` FOREIGN KEY (`AdresID`) REFERENCES `adres` (`AdresID`),
  ADD CONSTRAINT `pracownik_ibfk_3` FOREIGN KEY (`PracownikID`) REFERENCES `zamowienie` (`PracownikID`);

--
-- Constraints for table `recenzje_i_opis`
--
ALTER TABLE `recenzje_i_opis`
  ADD CONSTRAINT `recenzje_i_opis_ibfk_1` FOREIGN KEY (`czescid`) REFERENCES `czesci` (`CzescID`);

--
-- Constraints for table `reklamacja`
--
ALTER TABLE `reklamacja`
  ADD CONSTRAINT `reklamacja_ibfk_1` FOREIGN KEY (`ZamowienieID`) REFERENCES `zamowienie` (`ZamowienieID`);

--
-- Constraints for table `zamowienie`
--
ALTER TABLE `zamowienie`
  ADD CONSTRAINT `zamowienie_ibfk_1` FOREIGN KEY (`czescid`) REFERENCES `czesci` (`CzescID`),
  ADD CONSTRAINT `zamowienie_ibfk_2` FOREIGN KEY (`KlientID`) REFERENCES `klient` (`KlientID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
