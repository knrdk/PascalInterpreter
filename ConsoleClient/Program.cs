using PascalInterpreter;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = "14 + 2 *3-6 /2";
            var interpreter = new Interpreter(program);
            var result = interpreter.Expr();
        }
    }
}
