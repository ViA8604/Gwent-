using System;

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
            CheckPredicateReturnType();

            var effect = CompilerUtils.FindEffect(effectname);
            effect.CheckParameters(parameters);
            return true;
        }

        public GwentObject Evaluate()
        {
            return new GwentObject(0, GwentType.GwentNull);
        }

        bool CheckParametersSemantic()
        {
            foreach (var param in parameters)
            {
                if (param.Item2.CheckSemantic())
                {
                }
                else throw new Exception("Parameter must be the same type");
            }
            return true;
        }

        void CheckPredicateReturnType()
        {
            selector.CheckSemantic();
            if (selector.predicate.ReturnType != GwentType.GwentBool)
            {
                throw new InvalidDataException("Predicate must be booleable"); //Exception
            }
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
            return effectname;
        }
    }
}