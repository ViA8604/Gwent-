namespace GwentCompiler
{
    public static class CompilerUtils
    {
        public static Dictionary<string, TokenType> Getkeyword = new Dictionary<string, TokenType>() {
            { "Effect", TokenType.KeywordDeclarationEffecttoken },
            { "effect", TokenType.KeywordEffectCalltoken },
            { "Name", TokenType.KeywordNametoken },
            { "Params", TokenType.KeywordParamstoken },
            { "Number", TokenType.KeywordNumbertoken },
            { "String", TokenType.KeywordStringtoken },
            { "Bool", TokenType.KeywordBooltoken },
            { "Action", TokenType.KeywordActiontoken },
            { "targets", TokenType.KeywordTargetstoken },
            { "context", TokenType.KeywordContextoken },
            {"for" , TokenType.KeywordFortoken},
            { "in", TokenType.KeywordIntoken },
            { "while", TokenType.KeywordWhiletoken },
            { "TriggerPlayer", TokenType.KeywordTriggerPlayertoken },
            { "Board", TokenType.KeywordBoardtoken},
            {"Type" , TokenType.KeywordCardTypetoken},
            {"Faction" , TokenType.KeywordFactiontoken},
            {"Power" , TokenType.KeywordPowertoken},
            {"Range" , TokenType.KeywordRangetoken},
            {"OnActivation" , TokenType.KeywordOnActivationtoken},
            {"deck" , TokenType.KeywordDecktoken},
            {"Hand" , TokenType.KeywordHandtoken}};

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

    }



}
