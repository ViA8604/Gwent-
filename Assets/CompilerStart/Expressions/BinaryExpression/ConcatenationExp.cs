using System;
namespace GwentCompiler
{
    public class ConcatenationExpression : BinaryExpression
    {
        public ConcatenationExpression(IExpression left, IExpression right, Func<GwentObject,GwentObject,GwentObject> operation, string opSymbol) : base(left, right, operation, opSymbol)
        {
        }

        public override bool CheckSemantic()
        {
            base.CheckSemantic();
            if (left.ReturnType != GwentType.GwentString || right.ReturnType != GwentType.GwentString)
                throw new Exception($"Both operands must be strings {this}");
            return true ;
        }

        public override GwentType ReturnType => GwentType.GwentString;

    }
}