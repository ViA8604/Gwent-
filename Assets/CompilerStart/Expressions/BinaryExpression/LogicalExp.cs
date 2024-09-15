using System.Linq.Expressions;
using System;
using System.Collections.Generic;
namespace GwentCompiler
{
    public class LogicalExpression : BinaryExpression
{
    public LogicalExpression(IExpression left, IExpression right, Func<GwentObject,GwentObject,GwentObject> operation, string opSymbol) : base(left, right, operation, opSymbol)
    {
    }

    public override bool CheckSemantic()
    {
        base.CheckSemantic();
        if(left.ReturnType == GwentType.GwentBool && right.ReturnType == GwentType.GwentBool)
        {
            return true;
        }
        else{
            throw new Exception($"Both operands must be booleables , \n{this}");
        }
    }

    public override GwentType ReturnType => GwentType.GwentBool;


}
}