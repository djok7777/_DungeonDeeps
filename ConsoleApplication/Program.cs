using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryClass;
using LibraryClass.Items;
using System.IO;
using LibraryClass.System;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Map> maps = new List<Map>();

            bool game = true;
            bool battle = false;
            int xPlayerPosition = 30, yPlayerPosition = 20;
            Console.SetWindowSize(102, 42);
            Console.SetBufferSize(102, 42);

            Console.ReadKey();

            GameDisplay gameDisplay = new GameDisplay(xPlayerPosition, yPlayerPosition);

            do
            {
                gameDisplay.PlayerMovement();
            } while (game);

            Console.ReadLine();
            //Console.ReadKey();
        }
    }
}
