namespace GwentCompiler
{
class BinaryExpression : IExpression
{
    IExpression left;
    IExpression right;
    public BinaryExpression(IExpression left, IExpression right)
    {
        this.left = left;
        this.right = right;
    }

    public GwentObject Evaluate()
    {
        throw new System.NotImplementedException();
    }

    public bool CheckSemantic()
    {
        throw new System.NotImplementedException();
    }

    public GwentType ReturnType { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}









}