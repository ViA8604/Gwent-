using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace GwentCompiler
{
    public class NotExpression : IExpression
    {
        private IExpression operand;

        public NotExpression(IExpression operand)
        {
            this.operand = operand;
        }
        public virtual bool CheckSemantic()
        {
            operand.CheckSemantic();
            if (operand.ReturnType == GwentType.GwentBool)
            {
                return true;
            }
            throw new Exception("Operand must be the same type");
        }
        public GwentObject Evaluate()
        {
            GwentObject result = new GwentObject(!(bool)operand.Evaluate().value , GwentType.GwentBool);
            return result;
        }

        public virtual GwentType ReturnType => GwentType.GwentBool;

        public override string ToString()
        {
            return "!(" + operand.ToString() + ")";
        }

    }






}
