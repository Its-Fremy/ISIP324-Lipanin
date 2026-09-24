using System;
using System.Collections.Generic;

class Product
{
    public string Code;
    public string Name;
    public decimal Price;
    public int Quantity;
    public string Category;

    public bool InStock => Quantity > 0;

    public void Print()
    {
        Console.WriteLine($"код: {Code} | название: {Name} | цена: {Price} | кол-во: {Quantity} | категория: {Category} | на складе: {(InStock ? "да" : "нет")}");
    }
}

class Program
{
    static List<Product> products = new List<Product>();
    static int counter = 1;
    static string[] categories = { "Электроника", "Одежда", "Еда" };

    static void Main()
    {
        AddProduct("Телефон", 25000, 10, "Электроника");
        AddProduct("Ноутбук", 55000, 5, "Электроника");
        AddProduct("Футболка", 1500, 50, "Одежда");
        AddProduct("Джинсы", 3000, 0, "Одежда");
        AddProduct("Хлеб", 50, 100, "Еда");

        while (true)
        {
            Console.WriteLine("\n1. добавить товар");
            Console.WriteLine("2. удалить товар");
            Console.WriteLine("3. поставка товара");
            Console.WriteLine("4. продать товар");
            Console.WriteLine("5. найти товар");
            Console.WriteLine("6. все товары");
            Console.WriteLine("0. выход");
            Console.Write("выбор: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Add(); break;
                case "2": Delete(); break;
                case "3": Restock(); break;
                case "4": Sell(); break;
                case "5": Search(); break;
                case "6": ShowAll(); break;
                case "0": return;
                default: Console.WriteLine("неверный ввод"); break;
            }
        }
    }

    static void AddProduct(string name, decimal price, int qty, string cat)
    {
        Product p = new Product();
        p.Code = counter.ToString();
        p.Name = name;
        p.Price = price;
        p.Quantity = qty;
        p.Category = cat;
        products.Add(p);
        counter++;
    }

    static void Add()
    {
        Console.Write("название: ");
        string name = Console.ReadLine();
        if (name == "") { Console.WriteLine("название не может быть пустым"); return; }

        Console.Write("цена: ");
        decimal price;
        if (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
        { Console.WriteLine("неверная цена"); return; }

        Console.Write("количество: ");
        int qty;
        if (!int.TryParse(Console.ReadLine(), out qty) || qty < 0)
        { Console.WriteLine("неверное количество"); return; }

        Console.WriteLine("выберите категорию:");
        Console.WriteLine("1. Электроника");
        Console.WriteLine("2. Одежда");
        Console.WriteLine("3. Еда");
        Console.Write("номер: ");
        string catChoice = Console.ReadLine();

        int catIndex;
        if (!int.TryParse(catChoice, out catIndex) || catIndex < 1 || catIndex > 3)
        { Console.WriteLine("неверный номер категории"); return; }

        string cat = categories[catIndex - 1];

        AddProduct(name, price, qty, cat);
        Console.WriteLine("товар добавлен, код: " + products[products.Count - 1].Code);
    }

    static void Delete()
    {
        Console.Write("код товара: ");
        string code = Console.ReadLine();

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == code)
            {
                products.RemoveAt(i);
                Console.WriteLine("удалён");
                return;
            }
        }
        Console.WriteLine("не найден");
    }

    static void Restock()
    {
        Console.Write("код товара: ");
        string code = Console.ReadLine();

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == code)
            {
                Console.Write("сколько добавить: ");
                int amount;
                if (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                { Console.WriteLine("неверное число"); return; }

                products[i].Quantity += amount;
                Console.WriteLine("добавлено, теперь: " + products[i].Quantity + " шт.");
                return;
            }
        }
        Console.WriteLine("не найден");
    }

    static void Sell()
    {
        Console.Write("код товара: ");
        string code = Console.ReadLine();

        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].Code == code)
            {
                if (products[i].Quantity <= 0)
                { Console.WriteLine("нет на складе"); return; }

                Console.Write("сколько продать: ");
                int amount;
                if (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                { Console.WriteLine("неверное число"); return; }

                if (amount > products[i].Quantity)
                { Console.WriteLine("не хватает на складе"); return; }

                products[i].Quantity -= amount;
                Console.WriteLine("продано, остаток: " + products[i].Quantity + " шт.");
                return;
            }
        }
        Console.WriteLine("не найден");
    }

    static void Search()
    {
        Console.WriteLine("1. по коду  2. по названию  3. по категории");
        Console.Write("выбор: ");
        string type = Console.ReadLine();

        switch (type)
        {
            case "1":
                Console.Write("код: ");
                string code = Console.ReadLine();
                bool found1 = false;
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Code == code)
                    { products[i].Print(); found1 = true; }
                }
                if (!found1) Console.WriteLine("не найден");
                break;

            case "2":
                Console.Write("название: ");
                string name = Console.ReadLine();
                bool found2 = false;
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Name.ToLower().Contains(name.ToLower()))
                    { products[i].Print(); found2 = true; }
                }
                if (!found2) Console.WriteLine("не найден");
                break;

            case "3":
                Console.WriteLine("1. Электроника  2. Одежда  3. Еда");
                Console.Write("номер: ");
                string catChoice = Console.ReadLine();
                int catIndex;
                if (!int.TryParse(catChoice, out catIndex) || catIndex < 1 || catIndex > 3)
                { Console.WriteLine("неверный номер"); break; }

                string cat = categories[catIndex - 1];
                bool found3 = false;
                for (int i = 0; i < products.Count; i++)
                {
                    if (products[i].Category == cat)
                    { products[i].Print(); found3 = true; }
                }
                if (!found3) Console.WriteLine("не найден");
                break;

            default:
                Console.WriteLine("неверный выбор");
                break;
        }
    }

    static void ShowAll()
    {
        for (int i = 0; i < products.Count; i++)
            products[i].Print();
    }
}