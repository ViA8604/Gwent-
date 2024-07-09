using static GwentCompiler.GwentObject;

namespace GwentCompiler
{
    class LiteralExpression : IExpression
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
    }















}