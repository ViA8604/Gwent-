using System;
using System.Runtime.CompilerServices;
namespace GwentCompiler
{
    public class VariableExpression : IExpression
    {
        public string name;
        Scope refscope;

        public VariableExpression(string Name, Scope scope)
        {
            name = Name;
            refscope = scope;
        }

        public bool CheckSemantic()
        {
            if(refscope.FindTypes(name) == null)
            {
                throw new Exception($"**Semantic Error** : variable {name} been used before inicialization");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            return refscope.GetValue(name);
        }
        public GwentType ReturnType => refscope.GetType(name);

        public override string ToString()
        {
            return name;
        }
    }     
}