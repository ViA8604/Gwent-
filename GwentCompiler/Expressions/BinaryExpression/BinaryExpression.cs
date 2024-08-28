

namespace GwentCompiler
{
    public abstract class BinaryExpression : IExpression
    {
        protected IExpression left;
        protected IExpression right;
        protected Operation operation;
        protected string operatorS;

        public delegate GwentObject Operation(GwentObject left, GwentObject right);

        public BinaryExpression(IExpression left, IExpression right, Operation operation, string OperatorS)
        {
            this.left = left;
            this.right = right;
            this.operation = operation;
            this.operatorS = OperatorS;
        }

        public GwentObject Evaluate()
        {
            return operation(left.Evaluate(), right.Evaluate());
        }

        public virtual bool CheckSemantic()
        {
            if(left.CheckSemantic() && right.CheckSemantic())
            {
                return true;
            }
            else throw new Exception("Both operands must be the same type");
        }

        public virtual GwentType ReturnType => GwentType.GwentNull;
    }









}