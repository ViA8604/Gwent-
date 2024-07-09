using static GwentCompiler.GwentObject;

namespace GwentCompiler
{
    class ConditionalExpression : IExpression
    {
        IExpression condition;
        List<IExpression> false_body;
        List<IExpression> true_body;
        public ConditionalExpression(IExpression Condexpression, List<IExpression> False_body, List<IExpression> True_body)
        {
            condition = Condexpression;
            false_body = False_body;
            true_body = True_body;
        }
        public bool CheckSemantic()
        {
            condition.CheckSemantic();
            foreach (var exp in true_body)
            {
                exp.CheckSemantic();
            }
            foreach (var exp in false_body)
            {
                exp.CheckSemantic();
            }

            if (condition.ReturnType == GwentType.GwentBool)
            {
                return true;
            }
            throw new Exception("Condition of If statement must be booleable.");
        }

        public GwentType ReturnType => GwentType.GwentVoid;
        public GwentObject Evaluate()
        {
            if (condition.Evaluate().ToBool())
            {
                foreach (var expression in true_body)
                {
                    expression.Evaluate();
                }
            }
            else
            {
                foreach (var expression in false_body)
                {
                    expression.Evaluate();
                }
            }
            return new GwentObject(0, GwentType.GwentVoid);
        }


    }
}