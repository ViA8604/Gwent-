using System;
namespace GwentCompiler
{
    public class EffectDeclarationExpression : IExpression
    {
        string name;
        List<(string, GwentType)> parameters;
        Scope scope;
        FunctionExpression action;

        public EffectDeclarationExpression(string Name, List<(string, GwentType)> Parameters, FunctionExpression _action, Scope refscope)
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
        public void CheckParameters(List<(string, IExpression)> callp)
        {
            if(callp.Count() != parameters.Count)
            {
            throw new Exception("Number of parameters doesn't match effect declaration");
            }
            else
            {
                for (int i = 0; i < parameters.Count; i++)
                {
                    if(parameters[i].Item2 != callp[i].Item2.ReturnType)
                    {
                        throw new Exception("Parameter type doesn't match effect declaration");
                    }
                }
            }
        }
        public GwentType ReturnType => GwentType.GwentVoid;

        public override string ToString()
        {
            string output = $"Name: {name} \n";
            foreach (var param in parameters)
            {
                output += param.Item1 + ": " + param.Item2.ToString() + ",\n";
            }
            output += "{" + action.ToString();
            return  output;
        }
    }

}