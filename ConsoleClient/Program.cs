using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PascalInterpreter;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = " 2567    - 3     + 4 + 6 - 32       ";
            var interpreter = new Interpreter(program);
            var result = interpreter.Expr();
        }
    }
}
