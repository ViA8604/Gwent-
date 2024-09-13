using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace GwentCompiler
{
    public class FunctionExpression : IExpression
    {
        public List<(string, GwentType)> parameters;
        public Scope scope;
        public List<IExpression> body;

        public FunctionExpression(List<(string, GwentType)> Parameters, List<IExpression> Body, Scope refscope)
        {
            parameters = Parameters;
            body = Body;
            scope = refscope;
        }
        public bool CheckSemantic()
        {
            foreach (var param in parameters)
            {
                scope.SetType(param.Item1, param.Item2);
            }
            foreach (var exp in body)
            {
                exp.CheckSemantic();
            }
            return true;
        }
        public GwentObject Evaluate()
        {
            var result = new GwentObject(0, GwentType.GwentNull);
            foreach (var exp in body)
            {
                result = exp.Evaluate();
                Console.WriteLine(result);
            }
            return result;
        }


        public GwentObject Call(List<GwentObject> paraams)
        {
            if (paraams.Count() != parameters.Count())
            {
                throw new Exception($"Wrong number of parameters. Expected {parameters.Count()} but got {paraams.Count()}");
            }
            for (int i = 0; i < paraams.Count(); i++)
            {
                scope.SetValue(parameters[i].Item1, paraams[i]);
            }
            return this.Evaluate();
        }
        public GwentType ReturnType => body[body.Count - 1].ReturnType;

        public override string ToString()
        {
            string output = "(";
            foreach (var param in parameters)
            {
                output += $"{param.Item1} : type {param.Item2}, ";
            }
            output += ") \n { \n";

            foreach (var exp in body)
            {
                output += exp.ToString();
            }

            output += "}";
            return output;
        }
    }

}