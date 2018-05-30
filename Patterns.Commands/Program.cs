using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Commands
{
    class Program
    {
        static void Main(string[] args)
        {
            Component pd = new PageData();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("---------------------------------------");
                pd.Render();
                Console.WriteLine("---------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Commands: ");
                Console.WriteLine("\t1. Switch bool");
                Console.WriteLine("\t2. Enter int");
                Console.WriteLine("\t3. Undo action");
                Console.WriteLine("\t4. Redo action");
                Console.WriteLine("Enter command:");
                string variant = Console.ReadLine();
                switch (variant)
                {
                    case "1":
                        bool flag = (bool)pd.GetState("bool");
                        flag = !flag;
                        pd.SetState(new Dictionary<string, object>() { { "bool", flag } });
                        break;
                    case "2":
                        Console.Write("Enter value:");
                        var str = Console.ReadLine();
                        int a = int.Parse(str);
                        pd.SetState(new Dictionary<string, object>() { { "int", a } });
                        break;
                    case "3":
                        pd.Undo();
                        break;
                    case "4":
                        pd.Redo();
                        break;
                }
            }
        }


    }

    class PageData : Component
    {
        public PageData()
        {
            SetState(new Dictionary<string, object>()
            {
                { "bool", false },
                { "int", 1 }
            });
        }

        public override void Render()
        {
            Console.WriteLine("I\'m cool component!");
            Console.WriteLine("And I have undoble state!");
            Console.WriteLine("State: ");
            Console.WriteLine("\tSuper bool: " + GetState("bool"));
            Console.WriteLine("\tSuper int: " + GetState("int"));
        }
    }
}
