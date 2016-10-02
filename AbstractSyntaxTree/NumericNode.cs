using Model;

namespace AbstractSyntaxTree
{
    public class NumericNode : AbstractNode
    {
        private readonly Token Number;

        public NumericNode(Token number)
        {
            Number = number;
        }

        public int Value
        {
            get
            {
                return int.Parse(Number.Value);
            }
        }

        public override int Evaluate()
        {
            return Value;
        }
    }
}
