using System;
using System.Collections.Generic;
namespace GwentCompiler
{
    public class SelectorExpression : IExpression
    {
        string source;
        bool single;
        FunctionExpression predicate;
        public SelectorExpression(string Source, bool Single, FunctionExpression _predicate)
        {
            source = Source;
            single = Single;
            predicate = _predicate;
        }
        public bool CheckSemantic()
        {
            predicate.CheckSemantic();
            if (predicate.ReturnType != GwentType.GwentBool)
            {
                throw new Exception("Predicate must be booleable"); //Exception
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            return new GwentObject(0, GwentType.GwentNull);
        }
        public GwentType ReturnType => GwentType.GwentVoid;

        public override string ToString()
        {
            return $"Source: {source} Single: {single} Predicate: {predicate}";
        }
    }
}