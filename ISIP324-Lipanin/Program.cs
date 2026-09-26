using System;
using System.Collections.Generic;
using System.Linq;

enum Category
{
    Электроника = 1,
    Одежда,
    Еда
}

class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Category Category { get; set; }

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

    static void Main()
    {
        AddProduct("Телефон", 25000, 10, Category.Электроника);
        AddProduct("Ноутбук", 55000, 5, Category.Электроника);
        AddProduct("Футболка", 1500, 50, Category.Одежда);
        AddProduct("Джинсы", 3000, 0, Category.Одежда);
        AddProduct("Хлеб", 50, 100, Category.Еда);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Меню управления складом ---");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("3. Поставка товара");
            Console.WriteLine("4. Продать товар");
            Console.WriteLine("5. Найти товар");
            Console.WriteLine("6. Показать все товары");
            Console.WriteLine("0. Выход");
            Console.Write("\nВыбор: ");

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
                default:
                    Console.WriteLine("Неверный ввод. Попробуйте снова.");
                    break;
            }

            if (choice != "0")
            {
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    static void AddProduct(string name, decimal price, int qty, Category cat)
    {
        Product p = new Product
        {
            Code = counter.ToString(),
            Name = name,
            Price = price,
            Quantity = qty,
            Category = cat
        };
        products.Add(p);
        counter++;
    }

    static Product FindByCode(string code)
    {
        return products.FirstOrDefault(p => p.Code == code);
    }

    static void Add()
    {
        Console.Write("Название: ");
        string name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Название не может быть пустым");
            return;
        }

        Console.Write("Цена: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
        {
            Console.WriteLine("Неверная цена");
            return;
        }

        Console.Write("Количество: ");
        if (!int.TryParse(Console.ReadLine(), out int qty) || qty < 0)
        {
            Console.WriteLine("Неверное количество");
            return;
        }

        Console.WriteLine("Выберите категорию:");

        Console.WriteLine("1. электроника");
        Console.WriteLine("2. одежда");
        Console.WriteLine("3. еда");
        Console.Write("Номер: ");

        if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1 || catIndex > 3)
        {
            Console.WriteLine("Неверный номер категории");
            return;
        }

        Category cat = (Category)(catIndex - 1);
        AddProduct(name, price, qty, cat);
        Console.WriteLine($"Товар добавлен, код: {products.Last().Code}");
    }

    static void Delete()
    {
        Console.Write("Код товара: ");
        string code = Console.ReadLine();

        var product = FindByCode(code);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine("Товар удалён.");
        }
        else
        {
            Console.WriteLine("Товар не найден.");
        }
    }

    static void Restock()
    {
        Console.Write("Код товара: ");
        string code = Console.ReadLine();

        var product = FindByCode(code);
        if (product == null)
        {
            Console.WriteLine("Товар не найден.");
            return;
        }

        Console.Write("Сколько добавить: ");
        if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
        {
            Console.WriteLine("Неверное число");
            return;
        }

        product.Quantity += amount;
        Console.WriteLine($"Добавлено, теперь: {product.Quantity} шт.");
    }

    static void Sell()
    {
        Console.Write("Код товара: ");
        string code = Console.ReadLine();

        var product = FindByCode(code);
        if (product == null)
        {
            Console.WriteLine("Товар не найден.");
            return;
        }

        if (product.Quantity <= 0)
        {
            Console.WriteLine("Нет на складе");
            return;
        }

        Console.Write("Сколько продать: ");
        if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0)
        {
            Console.WriteLine("Неверное число");
            return;
        }

        if (amount > product.Quantity)
        {
            Console.WriteLine("Не хватает на складе");
            return;
        }

        product.Quantity -= amount;
        Console.WriteLine($"Продано, остаток: {product.Quantity} шт.");
    }

    static void Search()
    {
        Console.WriteLine("1. По коду  2. По названию  3. По категории");
        Console.Write("Выбор: ");
        string type = Console.ReadLine();

        switch (type)
        {
            case "1":
                Console.Write("Код: ");
                string code = Console.ReadLine();
                var foundProduct = FindByCode(code);
                if (foundProduct != null)
                    foundProduct.Print();
                else
                    Console.WriteLine("Не найден");
                break;

            case "2":
                Console.Write("Название: ");
                string name = Console.ReadLine();

                var foundByName = products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToList();
                if (foundByName.Any())
                    foundByName.ForEach(p => p.Print());
                else
                    Console.WriteLine("Не найден");
                break;

            case "3":
                Console.WriteLine("1. Электроника  2. Одежда  3. Еда");
                Console.Write("Номер: ");

                if (!int.TryParse(Console.ReadLine(), out int catIndex) || catIndex < 1 || catIndex > 3)
                {
                    Console.WriteLine("Неверный номер");
                    break;
                }

                Category selectedCat = (Category)(catIndex - 1);
                var foundByCat = products.Where(p => p.Category == selectedCat).ToList();
                if (foundByCat.Any())
                    foundByCat.ForEach(p => p.Print());
                else
                    Console.WriteLine("Не найден");
                break;

            default:
                Console.WriteLine("Неверный выбор");
                break;
        }
    }

    static void ShowAll()
    {
        if (!products.Any())
        {
            Console.WriteLine("Склад пуст.");
            return;
        }

        foreach (var p in products)
            p.Print();
    }
}