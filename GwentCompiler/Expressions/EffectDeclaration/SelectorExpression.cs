using System;

namespace GwentCompiler
{
    public class SelectorExpression : IExpression
    {
        string source;
        bool single;
        public FunctionExpression predicate;
        public SelectorExpression(string Source, bool Single, FunctionExpression _predicate)
        {
            source = Source;
            single = Single;
            predicate = _predicate;
        }
        public bool CheckSemantic()
        {
            predicate.CheckSemantic();
            return true;
        }  

        public GwentObject Evaluate()
        {
            return new GwentObject(0 , GwentType.GwentNull);
        }
        public GwentType ReturnType => GwentType.GwentVoid;
    }
}