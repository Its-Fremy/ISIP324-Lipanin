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

        }
        static void function()
        {

        }
    }
}
