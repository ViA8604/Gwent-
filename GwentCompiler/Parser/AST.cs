using System.Linq.Expressions;

namespace GwentCompiler
{
    public sealed class AST
    {
        List<IExpression> expressions;
        public AST(List<IExpression> Expressions)
        {
            expressions = Expressions;
        }

        public void Run()
        {
            try
            {
                this.CheckSemantic();
                this.Evaluate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        void CheckSemantic()
        {
            foreach(var exp in expressions)
            {
                exp.CheckSemantic();
            }
        }

        GwentObject Evaluate()
        {
            GwentObject result = new GwentObject(0,GwentType.GwentNull);
            foreach (var exp in expressions)
            {
                result = exp.Evaluate();
            }
            return result;
        }

        public override string ToString()
        {
            string result = "";
            foreach(var exp in expressions)
            {
                result += exp.ToString() + "\n";
            }
            return result;
        }
    }
}