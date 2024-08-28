using System.Linq.Expressions;

namespace GwentCompiler
{
    public class NegativeNumExpression : IExpression
    {
        private IExpression operand;

        public NegativeNumExpression(IExpression operand)
        {
            this.operand = operand;
        }
        public GwentObject Evaluate()
        {
            GwentObject result  = new GwentObject(-(double)operand.Evaluate().value , GwentType.GwentNumber);
            return result;
        }

        public bool CheckSemantic()
        {
            operand.CheckSemantic();
            if (operand.ReturnType == GwentType.GwentNumber)
            {
                return true;
            }
            else throw new Exception("Operand must be the same type");
        }
        public GwentType ReturnType => GwentType.GwentNumber;

        public override string ToString()
        {
            return "-(" + operand.ToString() + ")";
        }

    }






}
