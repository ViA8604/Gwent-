using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using System.Diagnostics;
using UnityEngine;

namespace GwentCompiler
{
    public class EffectCallExpression : IExpression
    {
        public IExpression effectname;
        string effName = "";
        List<(string, IExpression)> parameters;
        SelectorExpression selector;

        public EffectCallExpression(IExpression Effectname, List<(string, IExpression)> Parameters, SelectorExpression Selector)
        {
            effectname = Effectname;
            parameters = Parameters;
            selector = Selector;
        }
        public bool CheckSemantic()
        {
            effectname.CheckSemantic();
            if(effectname.ReturnType != GwentType.GwentString)
                throw new Exception("On Activation effect expresion must return string");
            
            CheckParametersSemantic();
            selector.CheckSemantic();

            return true;
        }

        public GwentObject Evaluate()
        {
            if(effName == "")
                effName = effectname.Evaluate().value.ToString();
            
            var effect = CompilerUtils.FindEffect(effName);
            effect.SetParameters(parameters, selector.Evaluate());
            effect.Execute();
            
            return new GwentObject(0, GwentType.GwentNull);
        }

        public void CheckCall()
        {
            if(effName == "")
                effName = effectname.Evaluate().value.ToString();

            var effect = CompilerUtils.FindEffect(effName);
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