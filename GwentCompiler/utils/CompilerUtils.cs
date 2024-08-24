namespace GwentCompiler
{
    public static class CompilerUtils
    {
        public static Dictionary<string, TokenType> Getkeyword = new Dictionary<string, TokenType>() {
            {"and" , TokenType.KeywordAndtoken} ,
            {"true" , TokenType.KeywordTruetoken} ,
            {"false" , TokenType.KeywordFalsetoken} ,
            {"for" , TokenType.KeywordFortoken} ,
            {"in" , TokenType.KeywordIntoken} ,
            {"while" , TokenType.KeywordWhiletoken} ,
            {"card" , TokenType.KeywordCardtoken} ,
            {"effect" , TokenType.KeywordDeclarationEffecttoken} ,
            {"Name" , TokenType.KeywordNametoken} ,
            {"Params" , TokenType.KeywordParamstoken} ,
            {"Action" , TokenType.KeywordActiontoken} ,
            {"Type" , TokenType.KeywordTypetoken} ,
            {"Faction" , TokenType.KeywordFactiontoken} ,
            {"Power" , TokenType.KeywordPowertoken} ,
            {"Range" , TokenType.KeywordRangetoken} ,
            {"OnActivation" , TokenType.KeywordOnActivationtoken} ,
            {"Effect" , TokenType.KeywordEffectCalltoken} ,
            {"Number" , TokenType.KeywordNumbertoken} ,
            {"String" , TokenType.KeywordStringtoken} ,
            {"Bool" , TokenType.KeywordBooltoken} ,
            {"unit" , TokenType.KeywordUnitoken} ,
            {"Selector" , TokenType.KeywordSelectortoken} ,
            {"Source" , TokenType.KeywordSourcetoken} ,
            {"Single" , TokenType.KeywordSingletoken} ,
            {"Predicate" , TokenType.KeywordPredicatetoken} ,
            {"PostAction" , TokenType.KeywordPosActiontoken}};

        public static Dictionary<string, TokenType> Getsymbol = new Dictionary<string, TokenType>(){
        {"+" , TokenType.PlusOperatortoken} ,
        {"+=" , TokenType.PlusEqualOperatortoken},
        {"++" , TokenType.IncrementOperatortoken} ,
        {"-" , TokenType.MinusOperatortoken} ,
        {"-=" , TokenType.MinusEqualOperatortoken},
        {"--" , TokenType.DecrementOperatortoken} ,
        {"*" , TokenType.MultiplicationOptoken} ,
        {"*=" , TokenType.MultiplicationEqualOptoken},
        {"=" , TokenType.SymbolEqualtoken},
        {"==" , TokenType.EqualityOperatortoken},
        {"!=" , TokenType.InequalityOperatortoken},
        {"," , TokenType.Commatoken},
        {"." , TokenType.DotSymbToken},
        {";" , TokenType.Semicolontoken},
        {":" , TokenType.Colontoken},
        {"(" , TokenType.OpenParenthesistoken},
        {")" , TokenType.CloseParenthesistoken},
        {"{" , TokenType.OpenCurlyBrackettoken},
        {"}" , TokenType.CloseCurlyBrackettoken},
        {"[" , TokenType.OpenSquareBrackettoken},
        {"]" , TokenType.CloseSquareBrackettoken},
        {"!" , TokenType.Exclamationtoken},
        {"?" , TokenType.QuestionMarktoken},
        {"=>" , TokenType.Hashrockettoken},
        {">" , TokenType.GreaterThantoken},
        {">=" , TokenType.GreaterEqualThantoken},
        {"<" , TokenType.LessThantoken},
        {"<=" , TokenType.LessEqualThantoken},
        {"\n" , TokenType.SymbolNewLinetoken} ,
        {" " , TokenType.WhiteSpacetoken} ,
        {"#" , TokenType.Commenttoken} ,
        {"\"" , TokenType.StringLiteraltoken} ,
        {"@" , TokenType.AtSigntoken},
        {"@@" , TokenType.DoubleAtSigntoken},
        {"\0" , TokenType.EOFtoken},
        };

        public static Dictionary<string, EffectExpression> EffectList = [];

        public static EffectExpression FindEffect(string name , List<(string, IExpression)> callparameters)
        {
            foreach (var param in callparameters)
            {
                if (EffectList.ContainsKey(name))
                {
                    if (param.Item2.ReturnType == EffectList[param.Item1].ReturnType)
                    {
                        return (EffectExpression)param.Item2;
                    }
                    else
                    {
                        throw new Exception("Effect parameters don't match effect declaration");
                    }
                }
            }
            throw new Exception("Effect not found, make sure it's been declared.");
        }
    }

    public static class GwentPredicates
    {
        public static GwentObject Sum(GwentObject a, GwentObject b)
        {
            return a + b;
        }

        public static GwentObject Sub(GwentObject a, GwentObject b)
        {
            return a - b;
        }

        public static GwentObject Mul(GwentObject a, GwentObject b)
        {
            return a * b;
        }

        public static GwentObject Div(GwentObject a, GwentObject b)
        {
            return a / b;
        }

        public static GwentObject Mod(GwentObject a, GwentObject b)
        {
            return a % b;
        }
    }

}
