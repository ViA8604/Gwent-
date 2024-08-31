using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using System;
namespace GwentCompiler
{
    public class NegativeNumExpression : IExpression
    {
        private IExpression operand;

        public NegativeNumExpression(IExpression operand)
        {
            this.operand = operand;
        }
        public bool CheckSemantic()
        {
            operand.CheckSemantic();
            if (operand.ReturnType == GwentType.GwentNumber)
            {
                return true;
            }
            throw new Exception("Operand must be the same type");
        }
        public GwentObject Evaluate()
        {
            GwentObject result  = new GwentObject(-(double)operand.Evaluate().value , GwentType.GwentNumber);
            return result;
        }
        public GwentType ReturnType => GwentType.GwentNumber;

        public override string ToString()
        {
            return "-(" + operand.ToString() + ")";
        }

    }






}
