using System;
namespace GwentCompiler
{
    public class EffectExpression : IExpression
    {
        string name;
        List<(string, GwentType)> parameters;
        Scope scope;
        FunctionExpression action;

        public EffectExpression(string Name, List<(string, GwentType)> Parameters, FunctionExpression _action, Scope refscope)
        {
            name = Name;
            parameters = Parameters;
            action = _action;
            scope = refscope;
        }

        public bool CheckSemantic()
        {
            foreach (var param in parameters)
            {
                scope.SetType(param.Item1, param.Item2);
            }
            action.CheckSemantic();
            return true;
        }

        public GwentObject Evaluate()
        {
            CompilerUtils.EffectList[name] = this;
            return new GwentObject(0, GwentType.GwentNull);
        }
        public bool CheckParameters(List<(string, IExpression)> callparameters)
        {
            foreach (var callparam in callparameters)
            {
                foreach (var param in parameters)
                {
                        if (callparam.Item2.ReturnType == param.Item2)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception("Effect parameters don't match effect declaration");
                        }
                }
            }
            throw new Exception("Effect parameters don't match effect declaration");
        }
        public GwentType ReturnType => GwentType.GwentVoid;
    }

}