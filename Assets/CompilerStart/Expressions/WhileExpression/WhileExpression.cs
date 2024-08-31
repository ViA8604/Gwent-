using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace GwentCompiler
{
public class WhileExpression : IExpression
{
    IExpression condition;
    List<IExpression> body;
    public WhileExpression (IExpression Condition , List<IExpression> Body)
    {
        condition = Condition;
        body = Body;
    }
    public GwentType ReturnType => GwentType.GwentVoid;

    public bool CheckSemantic()
    {
        condition.CheckSemantic();

        foreach (var exp in body)
            exp.CheckSemantic();
        
        if(condition.ReturnType != GwentType.GwentBool)
            throw new Exception("Condition of While statement must be booleable");
        
        return true;
    }

    public GwentObject Evaluate()
    {
        while(condition.Evaluate().ToBool())
        {
            foreach (var exp in body)
            {
                exp.Evaluate();
            }
        }
        return new GwentObject(0, GwentType.GwentVoid);
    }

        public override string ToString()
        {
            string outpu = "while(" + condition.ToString() + "){\n";
            foreach (var exp in body)
            {
                outpu += exp.ToString();
            }
            outpu += "}\n";
            
            return outpu;
        }

    }

}