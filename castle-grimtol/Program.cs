using grimtol_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace castle_grimtol
{
    class Program
    {
        static void Main(string[] args)
        {
            var _game = new Gameplay();
            Console.WriteLine("Welcome to Castle Grimtol.  Would you like to enter? (Y/N)");
            string resp = Console.ReadLine();
            resp = resp.ToUpper();
            if (resp == "Y")
            {
                bool playing = true;
                Console.WriteLine(_game.StartGame());
                Console.ReadLine();
                while (playing)
                {
                    Console.Clear();
                    Console.WriteLine("What do you do?");
                    string cmd = Console.ReadLine();
                    string cmdResp = _game.ProcessCommand(cmd);
                    if(cmdResp == "exit")
                    {
                        playing = false;
                        continue;
                    }
                    Console.WriteLine(cmdResp);
                    Console.WriteLine("Press any key to continue");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Fair enough, goodbye!");
                Console.ReadLine();
            }
        }
    }
}
