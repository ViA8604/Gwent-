using System.Numerics;
using System;
using System.Collections.Generic;
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
    SymbolStraightSlashtoken,
    
    Unknowntoken,
    WhiteSpacetoken,
        DeckCardtoken,
        FieldCardtoken,
        GraveyardCardtoken,
        HandCardtoken,
        DeckMethodtoken,
        FieldMethodtoken,
        GraveyardMethodtoken,
        HandMethodtoken,
        ShuffleMethodtoken,
        SendBottomMethodtoken,
        RemoveMethodtoken,
        PushMethodtoken,
        PopMethodtoken,
        FindKeywordtoken,
        ImageKeywordtoken,
        KeywordOwnertoken,
    }

    public class GwentObject
    {
        public object value;
        public GwentType type;

        public GwentObject(object value, GwentType type)
        {
            this.value = value;
            this.type = type;
        }

        public bool ToBool()
        {
            if (type == GwentType.GwentBool)
            {
                return value.ToString().ToLower() == "true";
            }
            throw new Exception("GwentObject non-booleable");
        }

        public static GwentObject operator +(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-addable");
            }

            double leftValue = CompilerUtils.GwentObjToDouble(left);
            double rightValue = CompilerUtils.GwentObjToDouble(right);

            return new GwentObject(leftValue + rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator -(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-substractable");
            }
            
            double leftValue = CompilerUtils.GwentObjToDouble(left);
            double rightValue = CompilerUtils.GwentObjToDouble(right);

            return new GwentObject(leftValue - rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator *(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-multipliable");
            }

            double leftValue = CompilerUtils.GwentObjToDouble(left);
            double rightValue = CompilerUtils.GwentObjToDouble(right);

            return new GwentObject(leftValue * rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator /(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-dividable");
            }

            double leftValue = CompilerUtils.GwentObjToDouble(left);
            double rightValue = CompilerUtils.GwentObjToDouble(right);

            return new GwentObject(leftValue / rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator %(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-modulable");
            }

            double leftValue = CompilerUtils.GwentObjToDouble(left);
            double rightValue = CompilerUtils.GwentObjToDouble(right);

            return new GwentObject(leftValue % rightValue, GwentType.GwentNumber);
        }
       
        
    }
    public enum GwentType
    {
        GwentNumber, GwentString, GwentBool, GwentVoid, GwentNull,
        CardType, GwentList,
        Zone,
    }
    public class CompilerCard
    {
        public string cardName;
        public ZoneObj zone;
        public CompilerCard(string CardName, ZoneObj Zone)
        {
            cardName = CardName;
            zone = Zone;
        }
    }

    public class ZoneObj
    {
        public string ZoneName;
        public int PlayerID;

        public ZoneObj(string zoneName, int playerID)
        {
            ZoneName = zoneName;
            PlayerID = playerID;
        }
    }
}