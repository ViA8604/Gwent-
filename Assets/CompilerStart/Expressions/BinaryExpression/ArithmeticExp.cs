using System.Linq.Expressions;
using System;
using System.Collections.Generic;
namespace GwentCompiler
{
    public class ArithmeticExpression : BinaryExpression
    {
        public ArithmeticExpression(IExpression left, IExpression right, Operation operation, string opSymbol) : base(left, right, operation, opSymbol)
        {
        }

        public override bool CheckSemantic()
        {
            base.CheckSemantic();
            if (left.ReturnType == GwentType.GwentNumber && right.ReturnType == GwentType.GwentNumber)
            {
                return true;
            }
            else
            {
                throw new Exception($"Both operands must be numbers {this}");
            }
        }

        public override GwentType ReturnType => GwentType.GwentNumber;

    }
}











