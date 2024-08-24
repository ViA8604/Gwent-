namespace GwentCompiler
{
    public interface IExpression {
        GwentObject Evaluate();
        bool CheckSemantic();
        GwentType ReturnType{get;}

    }
}
