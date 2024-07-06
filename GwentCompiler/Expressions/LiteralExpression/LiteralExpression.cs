namespace GwentCompiler
{
    class LiteralExpression : IExpression
    {
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
    }















}