
using System;
using System.Collections.Generic;

namespace GwentCompiler
{
    public abstract class BinaryExpression : IExpression
    {
        protected IExpression left;
        protected IExpression right;
        protected Func<GwentObject,GwentObject,GwentObject> operation;
        protected string opSymbol;

        public delegate GwentObject Operation(GwentObject left, GwentObject right);


        public BinaryExpression(IExpression left, IExpression right, Func<GwentObject,GwentObject,GwentObject> operation, string opSymbol)
        {
            this.left = left;
            this.right = right;
            this.operation = operation;
            this.opSymbol = opSymbol;
        }

        public virtual bool CheckSemantic()
        {
            return left.CheckSemantic() && right.CheckSemantic();
        }

        public GwentObject Evaluate()
        {
            return operation(left.Evaluate(), right.Evaluate());
        }

        public virtual GwentType ReturnType => GwentType.GwentNull; //se sobreescribe en cada expresión.

        public override string ToString()
        {
            string output = $"{left.ToString()} {opSymbol} {right.ToString()}";
            return output;
        }
    }









}