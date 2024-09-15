namespace GwentCompiler
{
    public enum TokenType
{
    //A - E
    AtSigntoken, CloseCurlyBrackettoken, CloseParenthesistoken,
    CloseSquareBrackettoken, Colontoken, Commatoken,
    Commenttoken, DecrementOperatortoken, DigitToken,
    DotSymbToken, DoubleAtSigntoken, EOFtoken,

    //E - K
    EqualityOperatortoken, Exclamationtoken, GreaterEqualThantoken,
    GreaterThantoken, Hashrockettoken, IDToken,
    InequalityOperatortoken, IncrementOperatortoken, KeywordActiontoken,
    KeywordAndtoken, KeywordBoardtoken, KeywordBooltoken,

    //K 
    KeywordCardtoken, KeywordCardTypetoken, KeywordContextoken,
    KeywordDeclarationEffecttoken, KeywordEffectCalltoken, KeywordElsetoken,
    KeywordFactiontoken, KeywordFalsetoken, KeywordFortoken,
    KeywordHandtoken, KeywordIftoken, KeywordInumber,

    KeywordIntoken, KeywordLogicalAndtoken, KeywordLogicalOrtoken,
    KeywordNametoken, KeywordNumbertoken, KeywordOnActivationtoken, 
    KeywordParamstoken, KeywordPosActiontoken, KeywordPowertoken,
    KeywordPredicatetoken, KeywordRangetoken, KeywordSelectortoken,

    //K - M
    KeywordSingletoken, KeywordSourcetoken, KeywordStringtoken, 
    KeywordTargetstoken, KeywordTriggerPlayertoken, KeywordTruetoken,
    KeywordWhiletoken, LessEqualThantoken, LessThantoken,
    MinusEqualOperatortoken, MinusOperatortoken, Moduletoken,
    
    //M - S
    MultiplicationEqualOptoken, MultiplicationOptoken, OpenCurlyBrackettoken,
    OpenParenthesistoken, OpenSquareBrackettoken, PlusEqualOperatortoken,
    PlusOperatortoken, QuestionMarktoken, Semicolontoken,
    StringLiteraltoken, SymbolEqualtoken, SymbolInvertedBackSlashtoken,
    SymbolMinusEqualtoken, SymbolMultiplicationOptoken, SymbolNewLinetoken,
    SymbolStraightSlashtoken, Unknowntoken, WhiteSpacetoken, DeckCardtoken, FieldCardtoken, GraveyardCardtoken, HandCardtoken, DeckMethodtoken, FieldMethodtoken, GraveyardMethodtoken, HandMethodtoken, ShuffleMethodtoken, SendBottomMethodtoken, RemoveMethodtoken, PushMethodtoken, PopMethodtoken, FindKeywordtoken, ImageKeywordtoken, KeywordOwnertoken, DivitionEqualOptoken,
    }
    public enum GwentType {
        GwentNumber, GwentString, GwentBool, GwentVoid, GwentNull,
        CardType, GwentList, Zone
    }

}