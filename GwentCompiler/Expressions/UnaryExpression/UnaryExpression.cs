using System.Linq.Expressions;
using static GwentCompiler.GwentObject;

namespace GwentCompiler
{
    public class UnaryExpression : IExpression
    {
        private IExpression operand;
        private string symbol;

        public UnaryExpression(IExpression operand, string symbol)
        {
            this.operand = operand;
            this.symbol = symbol;
        }
        public GwentObject Evaluate()
        {
            return new GwentObject(0, GwentType.GwentVoid);
        }

        public virtual bool CheckSemantic()
        {
            if (operand.CheckSemantic())
            {
                return true;
            }
            else throw new Exception("Operand must be the same type");
        }

        public virtual GwentType ReturnType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }



    }






}
