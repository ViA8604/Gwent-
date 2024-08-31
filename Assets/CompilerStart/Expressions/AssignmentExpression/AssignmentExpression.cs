using System;
using System.IO;
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
            if (refscope.GetType(name) == GwentType.GwentNull)  // primera asignacion de la variable
            {
                refscope.SetType(name, expression.ReturnType);  
            }
            else    // la variable hania sido asignada anteriormente
            {
                if(refscope.GetType(name) != expression.ReturnType)
                {
                    throw new InvalidDataException($"variable {name} ha sido reasignada con un tipo diferente");
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

        public override string ToString()
        {
            return name + " = " + expression.ToString();
        }
    }
}