/*Варіант 3. Конвертер величин
Дані: відсутні на старті.Значення для конвертації (double) вводиться користувачем після вибору напрямку.

Мінімальне меню: шість напрямків конвертації (три пари одиниць, кожна — в обидва боки) та «Вихід». Набір фіксований, однаковий для всіх:

Вихід.
км → милі: милі = км / 1.609344
милі → км: км = милі × 1.609344
кг → фунти: фунти = кг / 0.45359237
фунти → кг: кг = фунти × 0.45359237
°C → °F: °F = °C × 9 / 5 + 32
°F → °C: °C = (°F − 32) × 5 / 9
Після вибору напрямку програма запитує значення (введення з валідацією TryParse) і виводить результат у форматі «12.5 км = 7.77 милі» (з одиницями виміру в обидва боки).

Рекомендації щодо реалізації:

кожну конвертацію варто оформити окремим методом з параметром (значення) і поверненим значенням (результат), без дублювання формули в кількох місцях коду;
результат зручно округлювати до 2 знаків після коми при виведенні (Math.Round або форматування :F2);
якщо в прямому й зворотному напрямку одна пара використовує ту саму константу (наприклад, 1.609344), винесіть її в окрему іменовану константу — так формула в обох методах виглядає однаково;
Граничний випадок для обробки: значення, яке фізично неможливе для обраної величини. При цьому вивести попередження і не виконувати конвертацію. Наприклад: відстань і маса не можуть бути 
від'ємними; температура може бути від'ємною (наприклад, −10 °C — коректне значення), але не нижчою за абсолютний нуль: −273.15 °C або −459.67 °F.
*/

class Program
{
    static void Main()
    {
        // === 1. Дані програми (локальна змінна Main, передається в методи через параметри) ===
        // Заповнені наперед у коді — не потрібно вводити елементи щоразу при запуску.
        // int[] numbers = { 12, -5, 8, -1, 0, 7 };

        // === 2. Головний цикл меню ===
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n=== Меню ===");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("1. км → милі");
            Console.WriteLine("2. милі → км");
            Console.WriteLine("3. кг → фунти");
            Console.WriteLine("4. фунти → кг");
            Console.WriteLine("5. °C → °F");
            Console.WriteLine("6. °F → °C");

            int choice = ReadInt("Ваш вибір: ", 0, 6);

            isRunning = choice switch
            {
                0 => false,    
                1 => HandleKmToMiles(), 
                2 => HandleMilesToKm(),
                3 => HandleKgToLbs(),
                4 => HandleLbsToKg(),
                5 => HandleCelToF(),
                6 => HandleFToCel(),
                _ => true // сюди потрапити неможливо: ReadInt уже обмежив ввід діапазоном 0-3
            };
        }

        Console.WriteLine("Роботу завершено.");
    }

    const double KmMilesRatio = 1.609344;
    const double KgToLbsRatio = 0.45359237;

    // === 3. Методи для кожного пункту меню ===
    // Кожен викликає "робочий" метод з розділу 4 і повертає true, щоб цикл тривав.
    // Масив передається параметром (за посиланням через ref — див. AddNumber), а не полем класу.


    // --- km - mi
    static bool HandleKmToMiles()
    {
        double km = ReadDouble("Введіть відстань у км: ");
        if (km < 0)
        {
            Console.Write("Відстань не може бути від'ємна");
            return true;
        }

        double mi = ConvertKmToMiles(km);
        Console.WriteLine($"{km} км = {Math.Round(mi, 2)} милі");
        return true;
    }

    // --- mi - km
    static bool HandleMilesToKm()
    {
        double miles = ReadDouble("Введіть відстань у милях: ");
        if (miles < 0)
        {
            Console.Write("Відстань не може бути від'ємна");
            return true;
        }

        double km = ConvertMilesToKm(miles);
        Console.WriteLine($"{miles} миль = {Math.Round(km, 2)} км");
        return true;
    }

    // --- kg - lbs 

    static bool HandleKgToLbs()
    {
        double kg = ReadDouble("Введіть вагу у кг: ");
        if (kg < 0)
        {
            Console.Write("Маса не може бути від'ємна");
            return true;
        }

        double lbs = ConvertKgToLbs(kg);
        Console.WriteLine($"{kg} кг = {Math.Round(lbs, 2)} фунти");
        return true;
    }

// ---  lbs - kg

    static bool HandleLbsToKg()
    {
        double lbs = ReadDouble("Введіть вагу у фунтах: ");
        if (lbs < 0)
        {
            Console.Write("Маса не може бути від'ємна");
            return true;
        }

        double kg = ConvertLbsToKg(lbs);
        Console.WriteLine($"{lbs} фунтів = {Math.Round(kg, 2)} кг");
        return true;
    }

// --- °C → °F: °F = °C × 9 / 5 + 32

    static bool HandleCelToF()
    {
        double celsius = ReadDouble("Введіть температуру в °C: ");
        if (celsius < -273.15)
        {
            Console.Write("Температура не може бути нижча за абсолютний нуль");
            return true;
        }

        double fahrenheit = ConvertCelToF(celsius);
        Console.WriteLine($"{celsius} °C = {Math.Round(fahrenheit, 2)} °F");
        return true;
    }

    // --- °F → °C: °C = (°F − 32) × 5 / 9

    static bool HandleFToCel()
    {
        double fahrenheit = ReadDouble("Введіть температуру в °F: ");
        if (fahrenheit < -459.67)
        {
            Console.Write("Температура не може бути нижча за абсолютний нуль");
            return true;
        }

        double celsius = ConvertFToCel(fahrenheit);
        Console.WriteLine($"{fahrenheit} °F = {Math.Round(celsius, 2)} °C");
        return true;
    }
    // === 4. "Робочі" методи — саме вони мають параметри й повернене значення ===
    // (у своєму варіанті тут буде CalculateAverage/FindMax/Classify тощо, а не CountPositive)

    static int ReadInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            
            bool isSuccess = int.TryParse(input, out int result);
            
            if (isSuccess && result >= min && result <= max)
            {
                return result; 
            }
            else
            {
                Console.WriteLine($"Помилка! Введіть число від {min} до {max}.");
            }
        }
    }

    static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            bool isSuccess = double.TryParse(input, out double result);

            if (isSuccess)
            {
                return result;
            }

            Console.WriteLine("Введіть число");

        }
    }

    static double ConvertKmToMiles(double km)
    {
        return km / KmMilesRatio;
    }

    static double ConvertMilesToKm(double miles)
    {
        return miles * KmMilesRatio;
    }

    static double ConvertKgToLbs(double kg)
    {
        return kg / KgToLbsRatio;
    }

    static double ConvertLbsToKg(double lbs)
    {
        return lbs * KgToLbsRatio;
    }

    static double ConvertCelToF(double celsius)
    {
        return (celsius * 9.0 / 5.0) + 32;
    }

    static double ConvertFToCel(double fahrenheit)
    {
        return (fahrenheit - 32) * (5.0 / 9.0);
    }
}