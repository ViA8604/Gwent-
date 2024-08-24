using System;

namespace GwentCompiler
{
    public class EffectCallExpression : IExpression
    {
        string effectname;
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
            CheckPredicateReturnType();

            CompilerUtils.FindEffect(parameters);
            return true;
        }

        public GwentObject Evaluate()
        {
            return new GwentObject(0, GwentType.GwentNull);
        }
        public GwentType ReturnType => GwentType.GwentNull;


        bool CheckParametersSemantic()
        {
            foreach (var param in parameters)
            {
                if (param.Item2.CheckSemantic())
                {
                    return true;
                }
                else throw new Exception("Parameter must be the same type");
            }
            return true;
        }

        void CheckPredicateReturnType()
        {
            if (selector.predicate.ReturnType != GwentType.GwentBool)
            {
                throw new InvalidDataException("Predicate must be booleable"); //Exception
            }
        }
    }
}