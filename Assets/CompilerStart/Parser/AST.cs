using System.Collections.Generic;
using System.Linq.Expressions;
using System;
namespace GwentCompiler
{
    public sealed class AST
    {
        List<IExpression> expressions;
        public AST(List<IExpression> Expressions)
        {
            expressions = Expressions;
        }

        public void Run()
        {

                this.CheckSemantic();
                this.Evaluate();


        }
        void CheckSemantic()
        {
            foreach(var exp in expressions)
            {
                exp.CheckSemantic();
            }
        }

        GwentObject Evaluate()
        {
            GwentObject result = new GwentObject(0,GwentType.GwentNull);
            foreach (var exp in expressions)
            {
                result = exp.Evaluate();
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            foreach(var exp in expressions)
            {
                result += exp.ToString() + "\n";
            }
            return result;
        }
    }
}