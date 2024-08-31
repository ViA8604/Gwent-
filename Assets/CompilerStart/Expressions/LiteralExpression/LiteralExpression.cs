using System;
using System.Collections.Generic;
namespace GwentCompiler
{
    public class LiteralExpression : IExpression
    {
        //Caso base
        GwentObject obj;
        public LiteralExpression(GwentObject gwentObject)
        {
            obj = gwentObject;
        }
        public bool CheckSemantic()
        {
            return true;
        }
        public GwentObject Evaluate()
        {
            return obj;
        }
        public GwentType ReturnType => obj.type;

        public override string ToString()
        {
            return obj.value.ToString();
        }
    }















}