using PascalInterpreter;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = "7 + 3 * (10 / (12 / (3 + 1) - 1))";
            var interpreter = new Interpreter(program);
            var result = interpreter.Expr();
        }
    }
}
