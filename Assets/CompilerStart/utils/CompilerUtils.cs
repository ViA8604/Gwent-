using System;
using UnityEngine;
using GwentPro;
using System.Collections.Generic;
using Unity.VisualScripting;

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
        {"Board", TokenType.KeywordBoardtoken},
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
        {"None" , TokenType.NoneKeywordtoken} ,
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
        {"/=", TokenType.DivitionEqualOptoken},
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

        public static Dictionary<TokenType, string> NameZoneFKeywords = new Dictionary<TokenType, string>() {
        {TokenType.DeckMethodtoken, "Deck" },
        {TokenType.FieldMethodtoken, "Field" },
        {TokenType.GraveyardMethodtoken, "Graveyard" },
        {TokenType.HandMethodtoken, "Hand" }
        };

        public static Dictionary<TokenType, (Func<GwentObject, GwentObject, GwentObject>, string)> PredicatesDict = new Dictionary<TokenType, (Func<GwentObject, GwentObject, GwentObject>, string)>()
        {
            {TokenType.PlusOperatortoken, (GwentPredicates.Sum, "+")},
            {TokenType.MinusOperatortoken, (GwentPredicates.Sub, "-")},
            {TokenType.MultiplicationOptoken, (GwentPredicates.Mul, "*")},
            {TokenType.SymbolInvertedBackSlashtoken, (GwentPredicates.Div, "/")},

            {TokenType.IncrementOperatortoken, (GwentPredicates.Sum, "++")},
            {TokenType.DecrementOperatortoken, (GwentPredicates.Sub, "--")},

            {TokenType.PlusEqualOperatortoken, (GwentPredicates.Sum, "+=")},
            {TokenType.MinusEqualOperatortoken, (GwentPredicates.Sub, "-=")},
            {TokenType.MultiplicationEqualOptoken, (GwentPredicates.Mul, "*=")},
            {TokenType.DivitionEqualOptoken, (GwentPredicates.Div, "/=")},

            {TokenType.AtSigntoken , (GwentPredicates.ConcatString, "@")},
            {TokenType.DoubleAtSigntoken , (GwentPredicates.ConcatStringWithSpace, "@@" )},

            {TokenType.Moduletoken, (GwentPredicates.Mod, "%=")},
        };
        public static List<TokenType> CardZoneKeywords = new List<TokenType>() { TokenType.KeywordBoardtoken, TokenType.DeckCardtoken, TokenType.FieldCardtoken, TokenType.GraveyardCardtoken, TokenType.HandCardtoken };

        public static EffectDeclarationExpression FindEffect(string name)
        {
            if (EffectList.ContainsKey(name))
            {
                return EffectList[name];
            }

            throw new Exception($"Effect {name} not found, make sure it's been declared.");
        }

        public static void LaunchEffect(string name)
        {
            if (name != "None")
            {
                if (CardExpressions.ContainsKey(name))
                {
                    CardExpressions[name].ActiveEffect();
                }
            }
        }

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
}
