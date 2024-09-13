using System;
using UnityEngine;
using GwentPro;
using System.Collections.Generic;
namespace GwentCompiler
{
    public static class CompilerUtils
    {
        public static string tag = "Custom";
        public static GameManager gameManager;


        public static Dictionary<string, CardExpression> CardExpressions = new Dictionary<string, CardExpression>();

        public static Dictionary<string, EffectDeclarationExpression> EffectList = new Dictionary<string, EffectDeclarationExpression>();
        public static Dictionary<string, TokenType> Getkeyword = new Dictionary<string, TokenType>() {
    {"Action" , TokenType.KeywordActiontoken} ,
    {"Bool" , TokenType.KeywordBooltoken} ,
    {"card" , TokenType.KeywordCardtoken} ,
    {"context", TokenType.KeywordContextoken},
    {"Deck" , TokenType.DeckCardtoken},
    {"DeckOfPlayer" , TokenType.DeckMethodtoken},
    {"else" , TokenType.KeywordElsetoken} ,
    {"effect" , TokenType.KeywordDeclarationEffecttoken} ,
    {"Effect" , TokenType.KeywordEffectCalltoken} ,
    {"Faction" , TokenType.KeywordFactiontoken} ,
    {"false" , TokenType.KeywordFalsetoken} ,
    {"Field" , TokenType.FieldCardtoken},
    {"FieldOfPlayer", TokenType.FieldMethodtoken},
    {"Find", TokenType.FindKeywordtoken},
    {"for" , TokenType.KeywordFortoken} ,
    {"Graveyard" , TokenType.GraveyardCardtoken},
    {"GraveyardOfPlayer" , TokenType.GraveyardMethodtoken},
    {"Hand" , TokenType.HandCardtoken},
    {"HandOfPlayer" , TokenType.HandMethodtoken},
    {"if" , TokenType.KeywordIftoken} ,
    {"Image" , TokenType.ImageKeywordtoken} ,
    {"in" , TokenType.KeywordIntoken} ,
    {"Name" , TokenType.KeywordNametoken} ,
    {"Number" , TokenType.KeywordNumbertoken} ,
    {"OnActivation" , TokenType.KeywordOnActivationtoken} ,
    {"Owner", TokenType.KeywordOwnertoken},
    {"Params" , TokenType.KeywordParamstoken} ,
    {"Pop" , TokenType.PopMethodtoken},
    {"PostAction" , TokenType.KeywordPosActiontoken} ,
    {"Power" , TokenType.KeywordPowertoken} ,
    {"Predicate" , TokenType.KeywordPredicatetoken} ,
    {"Push" , TokenType.PushMethodtoken},
    {"Range" , TokenType.KeywordRangetoken} ,
    {"Remove" , TokenType.RemoveMethodtoken},
    {"Selector" , TokenType.KeywordSelectortoken} ,
    {"SendBottom" , TokenType.SendBottomMethodtoken},
    {"Shuffle" , TokenType.ShuffleMethodtoken},
    {"Single" , TokenType.KeywordSingletoken} ,
    {"Source" , TokenType.KeywordSourcetoken} ,
    {"String" , TokenType.KeywordStringtoken} ,
    {"targets" , TokenType.KeywordTargetstoken},
    {"true" , TokenType.KeywordTruetoken} ,
    {"Type" , TokenType.KeywordCardTypetoken} ,
    {"while" , TokenType.KeywordWhiletoken} ,
};

        public static Dictionary<string, TokenType> Getsymbol = new Dictionary<string, TokenType>() {
    //Arithmetical
    {"+" , TokenType.PlusOperatortoken} ,
    {"+=" , TokenType.PlusEqualOperatortoken},
    {"++" , TokenType.IncrementOperatortoken} ,
    {"-" , TokenType.MinusOperatortoken} ,
    {"-=" , TokenType.MinusEqualOperatortoken},
    {"--" , TokenType.DecrementOperatortoken} ,
    {"*" , TokenType.MultiplicationOptoken} ,
    {"*=" , TokenType.MultiplicationEqualOptoken},
    {"/" , TokenType.SymbolInvertedBackSlashtoken},
    {"%" , TokenType.Moduletoken},
    //Comparison
    {"==" , TokenType.EqualityOperatortoken},
    {"!=" , TokenType.InequalityOperatortoken},
    {">" , TokenType.GreaterThantoken},
    {">=" , TokenType.GreaterEqualThantoken},
    {"<" , TokenType.LessThantoken},
    {"<=" , TokenType.LessEqualThantoken},
    //Logical
    {"&&" , TokenType.KeywordLogicalAndtoken} ,
    {"||" , TokenType.KeywordLogicalOrtoken} ,
    //Punctuation
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
    //Assignment
    {"=" , TokenType.SymbolEqualtoken},
    //Misc
    {"!" , TokenType.Exclamationtoken},
    {"?" , TokenType.QuestionMarktoken},
    {"=>" , TokenType.Hashrockettoken},
    {"&" , TokenType.KeywordAndtoken} ,
    {"|" , TokenType.SymbolStraightSlashtoken},
    {" " , TokenType.WhiteSpacetoken} ,
    {"#" , TokenType.Commenttoken} ,
    {"\"" , TokenType.StringLiteraltoken} ,
    {"@" , TokenType.AtSigntoken},
    {"@@" , TokenType.DoubleAtSigntoken},
    {"\0" , TokenType.EOFtoken},
};

        public static EffectDeclarationExpression FindEffect(string name)
        {
            if (EffectList.ContainsKey(name))
            {
                return EffectList[name];
            }

            throw new Exception("Effect not found, make sure it's been declared.");
        }

        public static void LaunchEffect(string name)
        {
            if (CardExpressions.ContainsKey(name))
            {
                CardExpressions[name].ActiveEffect();
            }
        }

        public static List<TokenType> CardZoneKeywords = new List<TokenType>() { TokenType.DeckCardtoken, TokenType.FieldCardtoken, TokenType.GraveyardCardtoken, TokenType.HandCardtoken };
        public static Dictionary<TokenType, string> NameZoneFKeywords = new Dictionary<TokenType, string>() {
        {TokenType.DeckMethodtoken, "Deck" }, {TokenType.FieldMethodtoken, "Field" }, {TokenType.GraveyardMethodtoken, "Graveyard" }, {TokenType.HandMethodtoken, "Hand" }};


        public static List<GameObject> ConvertCardListGM(List<CardClass> cards)
        {
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (var card in cards)
            {
                gameObjects.Add(card.gameObject);
            }
            return gameObjects;
        }

        public static double GwentObjToDouble(GwentObject a)
        {
            return Double.Parse(a.value.ToString());
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

        public static GwentObject And(GwentObject left, GwentObject right)
        {
            return new GwentObject(left.ToBool() && right.ToBool(), GwentType.GwentBool);
        }

        public static GwentObject Or(GwentObject left, GwentObject right)
        {
            return new GwentObject(left.ToBool() || right.ToBool(), GwentType.GwentBool);
        }

        public static GwentObject Equal(GwentObject left, GwentObject right)
        {
            if (left.type == right.type)
            {
                return new GwentObject(CompilerUtils.GwentObjToDouble(left) == CompilerUtils.GwentObjToDouble(right), GwentType.GwentBool);
            }
            return new GwentObject(false, GwentType.GwentBool);
        }

        public static GwentObject NotEqual(GwentObject left, GwentObject right)
        {
            if (left.type == right.type)
            {
                return new GwentObject(CompilerUtils.GwentObjToDouble(left) != CompilerUtils.GwentObjToDouble(right), GwentType.GwentBool);
            }
            return new GwentObject(true, GwentType.GwentBool);
        }

        public static GwentObject LessThan(GwentObject left, GwentObject right)
        {
            return new GwentObject(CompilerUtils.GwentObjToDouble(left) < CompilerUtils.GwentObjToDouble(right), GwentType.GwentBool);
        }

        public static GwentObject GreaterThan(GwentObject left, GwentObject right)
        {
            return new GwentObject(CompilerUtils.GwentObjToDouble(left) > CompilerUtils.GwentObjToDouble(right), GwentType.GwentBool);
        }

        public static GwentObject LessEqualThan(GwentObject left, GwentObject right)
        {
            return new GwentObject(CompilerUtils.GwentObjToDouble(left) <= CompilerUtils.GwentObjToDouble(right), GwentType.GwentBool);
        }

        public static GwentObject GreaterEqualThan(GwentObject left, GwentObject right)
        {
            return new GwentObject(CompilerUtils.GwentObjToDouble(left) >= CompilerUtils.GwentObjToDouble(right), GwentType.GwentBool);
        }

    }

}
