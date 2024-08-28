using System;
using System.Runtime.CompilerServices;
namespace GwentCompiler
{
    public class VariableExpression : IExpression
    {
        string name;
        Scope refscope;

        public VariableExpression(string Name, Scope scope)
        {
            name = Name;
            refscope = scope;
        }

        public bool CheckSemantic()
        {
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