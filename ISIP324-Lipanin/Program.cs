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

        }
        static void function()
        {

        }
    }
}
