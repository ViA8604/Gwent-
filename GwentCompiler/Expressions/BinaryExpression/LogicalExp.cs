
using System.Linq.Expressions;
using static GwentCompiler.GwentObject;

namespace GwentCompiler
{
    public class LogicalExpression : BinaryExpression
{
    public LogicalExpression(IExpression left, IExpression right, Operation operation) : base(left, right, operation)
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
            throw new Exception("Both operands must be booleables");
        }
    }

    public override GwentType ReturnType => GwentType.GwentBool;
}
}