﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tron
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new Controller();
            controller.StartGame();
            Console.ReadKey();
        }
    }
}
