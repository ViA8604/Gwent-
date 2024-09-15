using System.Linq.Expressions;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using static GwentCompiler.BinaryExpression;
namespace GwentCompiler
{
    public class PrevSucExpression : IExpression
    {
        private VariableExpression variable;
        Scope scope;
        Func<GwentObject, GwentObject, GwentObject> operation;
        string opSymbol;

        public PrevSucExpression(VariableExpression Variable, Scope Scope, Func<GwentObject, GwentObject, GwentObject> Operate, string OpSymbol)
        {
            variable = Variable;
            scope = Scope;
            operation = Operate;
            opSymbol = OpSymbol;
        }
        public bool CheckSemantic()
        {
            variable.CheckSemantic();
            if(variable.ReturnType != GwentType.GwentNumber)
            {
                throw new Exception($"Variable {variable} return type must be number, but got {variable.ReturnType}");
            }
            return true;
        }
        public GwentObject Evaluate()
        {
            var result = operation(variable.Evaluate(), new GwentObject(1, GwentType.GwentNumber));
            scope.SetValue(variable.name , result);
            return result;
        }
        public GwentType ReturnType => GwentType.GwentNumber;

        public override string ToString()
        {
            return $"{variable.name} {opSymbol}";
        }

    }






}
