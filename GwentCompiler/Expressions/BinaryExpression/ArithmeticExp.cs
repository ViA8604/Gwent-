using System.Linq.Expressions;


namespace GwentCompiler
{
    public class ArithmeticExpression : BinaryExpression
{
    public ArithmeticExpression(IExpression left, IExpression right, Operation operation) : base(left, right, operation)
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

    public override GwentType ReturnType => GwentType.GwentNumber;
}
}











