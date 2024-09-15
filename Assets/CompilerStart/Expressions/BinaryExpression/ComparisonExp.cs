using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace GwentCompiler
{
    public class ComparisonExpression : BinaryExpression
{
    public ComparisonExpression(IExpression left, IExpression right, Func<GwentObject,GwentObject,GwentObject> operation, string opSymbol) : base(left, right, operation, opSymbol)
    {
    }

    public override bool CheckSemantic()
    {
        base.CheckSemantic();
        if(left.ReturnType == GwentType.GwentNumber && right.ReturnType == GwentType.GwentNumber)
        {
            return true;
        }
        else{
            throw new Exception($"Both operands must be numbers, \n{this}");
        }
    }

    public override GwentType ReturnType => GwentType.GwentBool;

}
}