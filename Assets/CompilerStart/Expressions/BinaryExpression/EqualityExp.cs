using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace GwentCompiler
{
    public class EqualityExpression : BinaryExpression
{
    public EqualityExpression(IExpression left, IExpression right, Func<GwentObject,GwentObject,GwentObject> operation, string opSymbol) : base(left, right, operation, opSymbol)
    {
    }

    public override bool CheckSemantic()
    {
        return base.CheckSemantic();
    }

    public override GwentType ReturnType => GwentType.GwentBool;

}
}