using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace GwentCompiler
{
    public class EffectDeclarationExpression : IExpression
    {
        IExpression nameExpr;
        List<(string, GwentType)> parameters;
        Scope scope;
        FunctionExpression action;

        public EffectDeclarationExpression(IExpression NameExpr, List<(string, GwentType)> Parameters, FunctionExpression _action, Scope refscope)
        {
            nameExpr = NameExpr;
            parameters = Parameters;
            action = _action;
            scope = refscope;
        }

        public bool CheckSemantic()
        {
            nameExpr.CheckSemantic();
            if(nameExpr.ReturnType != GwentType.GwentString )
                throw new Exception("Effect Name expression must return string");

            foreach (var param in parameters)
            {
                scope.SetType(param.Item1, param.Item2);
            }
            scope.SetType("targets", GwentType.GwentList);
            action.CheckSemantic();
            return true;
        }

        public GwentObject Evaluate()
        {
            string name = nameExpr.Evaluate().value.ToString();
            CompilerUtils.EffectList[name] = this;
            return new GwentObject(0, GwentType.GwentNull);
        }

        public void Execute()
        {
            action.Evaluate();
        }

        public void CheckParameters(List<(string, IExpression)> args)
        {
            if (args.Count() != parameters.Count())
                throw new Exception("Number of parameters doesn't match effect declaration");

            foreach (var param in parameters)
            {
                if (!args.Any(a => a.Item1 == param.Item1 && a.Item2.ReturnType == param.Item2))
                    throw new Exception("Parameters name/type does not match the arguments passed during effect call");
            }
        }
        
        public void SetParameters(List<(string, IExpression)> args, GwentObject value)
        {
            scope.SetValue("targets", value);
            foreach (var item in args)
            {
                scope.SetValue(item.Item1, item.Item2.Evaluate());
            }
        }

        public GwentType ReturnType => GwentType.GwentVoid;

        public override string ToString()
        {
            string output = $"Name: {nameExpr.Evaluate().value.ToString()} \n";
            foreach (var param in parameters)
            {
                output += param.Item1 + ": " + param.Item2.ToString() + ",\n";
            }
            output += "{" + action.ToString();
            return output;
        }
    }

}