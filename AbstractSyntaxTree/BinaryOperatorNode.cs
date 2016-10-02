using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace AbstractSyntaxTree
{
    public class BinaryOperatorNode : AbstractNode
    {
        private readonly AbstractNode LeftChild;
        private readonly AbstractNode RightChild;
        private readonly Token Operator;

        public BinaryOperatorNode(AbstractNode leftChild, AbstractNode rightChild, Token @operator)
        {
            LeftChild = leftChild;
            RightChild = rightChild;
            Operator = @operator;
        }

        public override int Evaluate()
        {
            if (Operator.Type == TokenType.Plus)
            {
                return LeftChild.Evaluate() + RightChild.Evaluate();
            }
            else if (Operator.Type == TokenType.Minus)
            {
                return LeftChild.Evaluate() + RightChild.Evaluate();
            }
            else if (Operator.Type == TokenType.Multiplication)
            {
                return LeftChild.Evaluate() * RightChild.Evaluate();
            }
            else if (Operator.Type == TokenType.Division)
            {
                return LeftChild.Evaluate() / RightChild.Evaluate();
            }
            throw new Exception("Invalid operator type");
        }
    }
}
