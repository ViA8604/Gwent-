namespace GwentCompiler
{
    public class FunctionExpression:IExpression
    {
        public string name;
        public List<(string , GwentType)>parameters;
        public Scope scope;
        public List<IExpression> body;
        GwentObject result;

        public FunctionExpression(string Name , List<(string , GwentType)> Parameters, List<IExpression> Body , Scope refscope)
        {
            name = Name;
            parameters = Parameters;
            body = Body;
            scope = refscope;
            result = new GwentObject(0 , GwentType.GwentNull);
        }
        public bool CheckSemantic()
        {
            foreach (var param in parameters)
            {
                scope.SetType(param.Item1 , param.Item2);
            }
            foreach (var exp in body)
            {
                exp.CheckSemantic();
            }
            return true;
        }
        public GwentObject Evaluate()
        {
            foreach (var exp in body)
            {
                result = exp.Evaluate();
            }
            return result;
        }
        public GwentType ReturnType => body[body.Count - 1].ReturnType;
    }

}