using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using System.Diagnostics;
namespace GwentCompiler
{
    public class EffectCallExpression : IExpression
    {
        public string effectname;
        List<(string, IExpression)> parameters;
        SelectorExpression selector;

        public EffectCallExpression(string Effectname, List<(string, IExpression)> Parameters, SelectorExpression Selector)
        {
            effectname = Effectname;
            parameters = Parameters;
            selector = Selector;
        }
        public bool CheckSemantic()
        {
            CheckParametersSemantic();
            selector.CheckSemantic();

            return true;
        }

        public GwentObject Evaluate()
        {
            var effect = CompilerUtils.FindEffect(effectname);
            effect.SetParameters(parameters, selector.Evaluate());
            effect.Execute();
            
            return new GwentObject(0, GwentType.GwentNull);
        }

        public void CheckCall()
        {
            var effect = CompilerUtils.FindEffect(effectname);
            effect.CheckParameters(parameters);
        }

        bool CheckParametersSemantic()
        {
            foreach (var param in parameters)
            {
                param.Item2.CheckSemantic();
            }
            return true;
        }

        public GwentType ReturnType => GwentType.GwentNull;


        public override string ToString()
        {
            string output = effectname + ", \n";
            foreach (var param in parameters)
            {
                output += param.Item1 + ": " + param.Item2.ToString() + ",\n";
            }
            output += "{" + selector.ToString();
            return output;
        }
    }
}