using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP324_Lipanin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int kolvo;
            while (true)
            {
                Console.Write("Введите кол-во операций (от 2 до 40): ");
                kolvo = int.Parse(Console.ReadLine());
                if (kolvo >= 2 && kolvo <= 40)
                {
                    break;
                }
                Console.WriteLine("Введено неверное кол-во операций \nВведите от 2 до 40");
            }



            string[] operation = new string [kolvo];
            int[] prices = new int [kolvo];

            for (int i = 0; i < kolvo; i++)
            {
                Console.WriteLine("Вводите данные в формате Операция;Цена");
                string input = Console.ReadLine();

                string[] parts = input.Split(';');

                if (parts.Length != 2 || !int.TryParse(parts[1].Trim(), out prices[i]))
                {
                    Console.WriteLine("Неверный формат! Попробуйте снова");
                    i--;
                    continue;
                }
                operation[i] = parts[0].Trim();
            }



            while (true)
            {
                Console.WriteLine("\n1.Вывод данных \n2.Статистика \n3.Сортировка по цене \n4.Конвертация валюты \n5.Поиск по названию \n0.Выход");
                
                string choice = Console.ReadLine();
                switch (choice)
                {

                    case "1":
                        {
                            vivod1(operation,prices);
                            break;
                        }
                    case "2":
                        {
                            stata2(prices,kolvo);
                            break;
                        }
                    case "3":
                        {
                            sort3(operation,prices);
                            break;
                        }
                    case "4":
                        {
                            convert4(operation,prices);
                            break;
                        }
                    case "5":
                        {
                            search5(operation,prices);
                            break;
                        }
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Нет такого пункта");
                        break;
                }
            }
        }
        static void vivod1(string[] operation, int[] prices)
        {
            Console.WriteLine("Вывод:");
            for (int i = 0; i < operation.Length; i++)
                Console.WriteLine($"{i + 1}. {operation[i]} — {prices[i]} руб.");
        }
        static void stata2(int[] prices, int kolvo)
        {
            Console.WriteLine("Статистика: ");

            int allmas = 0;
            foreach(int i in prices)
            {
                allmas += i;
            }

            int srednee = allmas/ kolvo;
            int max = prices.Max();
            int min = prices.Min();

            Console.Write("Среднее: " + srednee + "руб.\n");
            Console.Write("Максимальное: " + max + "руб.\n");
            Console.Write("Минимальное: " + min + "руб.\n");
            Console.Write("Сумма: " + allmas + "руб.\n");
        }
        static void sort3(string[] names, int[] prices)
        {
            for (int i = 0; i < prices.Length - 1; i++)
                for (int j = 0; j < prices.Length - 1 - i; j++)
                    if (prices[j] > prices[j + 1])
                    {
                        int a= prices[j];
                        prices[j] = prices[j + 1];
                        prices[j + 1] = a;
                        string b = names[j];
                        names[j] = names[j + 1];
                        names[j + 1] = b;
                    }
        }
        static void convert4(string[] operation, int[] prices)
        {
            Console.WriteLine("1 — USD  2 — EUR  3 — ввести свой курс");
            Console.Write("Выберите: ");
            double rate;
            string currency;

            switch (Console.ReadLine())
            {
                case "1":
                    {
                        rate = 84.26; currency = "USD";
                        break;
                    }
                case "2":
                    {
                        rate = 97.87; currency = "EUR";
                        break;
                    }
                case "3":
                    {
                        Console.Write("Введите курс (руб. за 1 ед. валюты): ");
                        if (!double.TryParse(Console.ReadLine(), out rate) || rate <= 0)
                        {
                            Console.WriteLine("Неверный курс.");
                            return;
                        }
                        currency = "указанной единицы";
                        break;
                    }
                default:
                    Console.WriteLine("Нет такого пункта.");
                    return;
            }

            Console.WriteLine($"Курс: 1 {currency} = {rate} руб.");
            for (int i = 0; i < operation.Length; i++)
                Console.WriteLine($"{operation[i]} — {(prices[i] / rate):F2} {currency}");
        }
        static void search5(string[] operation, int[] prices)
        {
            Console.Write("Введите название для поиска: ");
            string a = Console.ReadLine().Trim().ToLower();
            bool found = false;

            for (int i = 0; i < operation.Length; i++)
                if (operation[i].ToLower().Contains(a))
                {
                    Console.WriteLine($"{operation[i]} — {prices[i]} руб.");
                    found = true;
                }

            if (!found) Console.WriteLine("Ничего не найдено.");
        }
    }
}
