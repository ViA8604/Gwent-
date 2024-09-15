using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
namespace GwentCompiler
{
    public class AutoSetExpression : IExpression
    {
        private VariableExpression left;
        IExpression right;
        Scope scope;
        Func<GwentObject, GwentObject, GwentObject> operation;
        string opSymbol;

        public AutoSetExpression(IExpression Left, IExpression Right, Scope Scope, Func<GwentObject, GwentObject, GwentObject> Operate, string OpSymbol)
        {
            if(Left is not VariableExpression)
            {
                throw new Exception($"Invalid left argument {Left} must be a variable");
            }
            left = (VariableExpression)Left;
            right = Right;
            scope = Scope;
            operation = Operate;
            opSymbol = OpSymbol;
        }
        public bool CheckSemantic()
        {
            left.CheckSemantic();
            right.CheckSemantic();
            if(left.ReturnType != GwentType.GwentNumber || right.ReturnType != GwentType.GwentNumber)
            {
                throw new Exception($"Both operands {left} and {right} of {opSymbol} return type must be number, but got {left.ReturnType} and {right.ReturnType}");
            }
            return true;
        }
        public GwentObject Evaluate()
        {
            var result = operation(left.Evaluate(), right.Evaluate());
            scope.SetValue(left.name , result);
            return result;
        }
        public GwentType ReturnType => GwentType.GwentNumber;

        public override string ToString()
        {
            return $"{left.name} {opSymbol} {right}";
        }

    }
}
