using System;
using System.Collections.Generic;
using System.Linq;

namespace Shop
{
    enum Category { Electronics, Clothing, Food }

    class Product
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool InStock => Quantity > 0;
        public Category Category { get; set; }

        public Product(string name, decimal price, int quantity, Category category, string code)
        {
            Code = code;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        public void Print()
        {
            Console.WriteLine($"Код: {Code}, Название: {Name}, Цена: {Price}, Количество: {Quantity}, На складе: {(InStock ? "Да" : "Нет")}, Категория: {Category}");
        }
    }

    class Program
    {
        static List<Product> products = new List<Product>();

        static void Main()
        {
            products.Add(new Product("Ноутбук", 50000, 10, Category.Electronics, GetNextCode()));
            products.Add(new Product("Телефон", 30000, 5, Category.Electronics, GetNextCode()));
            products.Add(new Product("Футболка", 1500, 20, Category.Clothing, GetNextCode()));
            products.Add(new Product("Джинсы", 3000, 0, Category.Clothing, GetNextCode()));
            products.Add(new Product("Молоко", 80, 100, Category.Food, GetNextCode()));

            while (true)
            {
                Console.WriteLine("\n1-Добавить");
                Console.WriteLine("2-Удалить");
                Console.WriteLine("3-Поставка");
                Console.WriteLine("4-Продать");
                Console.WriteLine("5-Поиск");
                Console.WriteLine("6-Все");
                Console.WriteLine("0-Выход");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                if (choice == "1") AddProduct();
                else if (choice == "2") RemoveProduct();
                else if (choice == "3") OrderSupply();
                else if (choice == "4") SellProduct();
                else if (choice == "5") Search();
                else if (choice == "6") products.ForEach(p => p.Print());
                else if (choice == "0") return;
            }
        }

        static string GetNextCode()
        {
            int code = 1;
            while (products.Any(p => p.Code == code.ToString()))
            {
                code++;
            }
            return code.ToString();
        }

        static void AddProduct()
        {
            Console.Write("Название: ");
            string name = Console.ReadLine();
            if (string.IsNullOrEmpty(name)) { Console.WriteLine("Ошибка"); return; }

            Console.Write("Цена: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0) { Console.WriteLine("Ошибка"); return; }

            Console.Write("Количество: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty < 0) { Console.WriteLine("Ошибка"); return; }

            Console.Write("Категория (0-Electronics, 1-Clothing, 2-Food): ");
            if (!int.TryParse(Console.ReadLine(), out int cat) || cat < 0 || cat > 2) { Console.WriteLine("Ошибка"); return; }

            string code = GetNextCode();
            products.Add(new Product(name, price, qty, (Category)cat, code));
            Console.WriteLine($"Добавлен с кодом {code}");
        }

        static void RemoveProduct()
        {
            Console.Write("Код: ");
            string code = Console.ReadLine().Trim();
            var p = products.FirstOrDefault(x => x.Code == code);
            if (p != null) { products.Remove(p); Console.WriteLine("Удален"); }
            else Console.WriteLine("Не найден");
        }

        static void OrderSupply()
        {
            Console.Write("Код: ");
            string code = Console.ReadLine().Trim();
            var p = products.FirstOrDefault(x => x.Code == code);
            if (p == null) { Console.WriteLine("Не найден"); return; }

            Console.Write("Сколько: ");
            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
            {
                p.Quantity += amount;
                Console.WriteLine("Готово");
            }
            else Console.WriteLine("Ошибка");
        }

        static void SellProduct()
        {
            Console.Write("Код: ");
            string code = Console.ReadLine().Trim();
            var p = products.FirstOrDefault(x => x.Code == code);

            if (p == null) { Console.WriteLine("Не найден"); return; }

            if (p.Quantity <= 0) { Console.WriteLine("Нет на складе"); return; }

            Console.Write("Сколько: ");
            if (!int.TryParse(Console.ReadLine(), out int amount) || amount <= 0) { Console.WriteLine("Ошибка"); return; }

            if (amount > p.Quantity) { Console.WriteLine("Недостаточно"); return; }

            p.Quantity -= amount;
            Console.WriteLine("Продано");
        }

        static void Search()
        {
            Console.WriteLine("1-По коду 2-По названию 3-По категории");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Код: ");
                string code = Console.ReadLine().Trim();
                var p = products.FirstOrDefault(x => x.Code == code);
                if (p != null) p.Print();
                else Console.WriteLine("Не найден");
            }
            else if (choice == "2")
            {
                Console.Write("Название: ");
                string name = Console.ReadLine().ToLower();
                var found = products.Where(x => x.Name.ToLower().Contains(name)).ToList();
                if (found.Count > 0)
                    found.ForEach(p => p.Print());
                else
                    Console.WriteLine("Не найдено");
            }
            else if (choice == "3")
            {
                Console.Write("Категория (0-2): ");
                if (int.TryParse(Console.ReadLine(), out int cat) && cat >= 0 && cat <= 2)
                {
                    var found = products.Where(x => x.Category == (Category)cat).ToList();
                    if (found.Count > 0)
                        found.ForEach(p => p.Print());
                    else
                        Console.WriteLine("Не найдено");
                }
                else Console.WriteLine("Ошибка");
            }
        }
    }
}