
using System.Linq.Expressions;


namespace GwentCompiler
{
    public class ComparisonExpression : BinaryExpression
{
    public ComparisonExpression(IExpression left, IExpression right, Operation operation, string operatorS) : base(left, right, operation, operatorS)
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
            throw new Exception("Both operands must be numbers");
        }
    }

    public override GwentType ReturnType => GwentType.GwentBool;

    public override string ToString()
    {
        string output = $"{left.ToString} {operatorS} {right.ToString}";
        return output;
    }
}
}