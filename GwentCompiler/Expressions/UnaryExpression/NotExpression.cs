using System.Linq.Expressions;


namespace GwentCompiler
{
    public class NotExpression : IExpression
    {
        private IExpression operand;

        public NotExpression(IExpression operand)
        {
            this.operand = operand;
        }
        public GwentObject Evaluate()
        {
            GwentObject result = new GwentObject(!(bool)operand.Evaluate().value , GwentType.GwentBool);
            return result;
        }

        public virtual bool CheckSemantic()
        {
            operand.CheckSemantic();
            if (operand.ReturnType == GwentType.GwentBool)
            {
                return true;
            }
            else throw new Exception("Operand must be the same type");
        }

        public virtual GwentType ReturnType => GwentType.GwentBool;

        public override string ToString()
        {
            return "!(" + operand.ToString() + ")";
        }

    }






}
