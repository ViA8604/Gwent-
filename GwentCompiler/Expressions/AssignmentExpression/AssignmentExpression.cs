using System;
namespace GwentCompiler
{
    public class AssignmentExpression : IExpression
    {
        Scope refscope;
        string name;
        IExpression expression;

        public AssignmentExpression(string Name, Scope scope, IExpression exp)
        {
            name = Name;
            refscope = scope;
            expression = exp;
        }

        public bool CheckSemantic()
        {
            expression.CheckSemantic();
            if (refscope.GetType(name) == GwentType.GwentNull)
            {
                refscope.SetType(name, expression.ReturnType);
            }
            else
            {
                if(refscope.GetType(name) != expression.ReturnType)
                {
                    throw new InvalidDataException("No ej posible reasignar variables con un tipo diferente.");
                }
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            GwentObject value = expression.Evaluate();
            refscope.SetValue(name , value);
            return value;
        }

        public GwentType ReturnType => refscope.GetType(name);
    }
}