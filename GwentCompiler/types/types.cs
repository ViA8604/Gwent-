using System.Numerics;

namespace GwentCompiler
{
    public enum TokenType
    {
        EOFtoken, WhiteSpacetoken, Commenttoken,
        StringLiteraltoken, DigitToken, IDToken,
        KeywordEffectCalltoken, KeywordNametoken, KeywordParamstoken,
        KeywordStringtoken, KeywordNumbertoken, KeywordBooltoken,
        KeywordActiontoken, KeywordTargetstoken, KeywordContextoken,
        KeywordIntoken, KeywordWhiletoken, KeywordTriggerPlayertoken,
        KeywordBoardtoken, KeywordCardTypetoken, KeywordFactiontoken,
        KeywordPowertoken, KeywordRangetoken, KeywordOnActivationtoken,
        KeywordFortoken, KeywordDecktoken, KeywordHandtoken,
        Unknowntoken, SymbolNewLinetoken, PlusOperatortoken,
        PlusEqualOperatortoken, MinusOperatortoken, MinusEqualOperatortoken,
        SymbolEqualtoken, KeywordDeclarationEffecttoken, SymbolMultiplicationOptoken,
        EqualityOperatortoken, MultiplicationOptoken, MultiplicationEqualOptoken,
        IncrementOperatortoken, DecrementOperatortoken, SymbolMinusEqualtoken,
        Commatoken, DotSymbToken, Semicolontoken,
        Colontoken, OpenParenthesistoken, CloseParenthesistoken,
        OpenCurlyBrackettoken, CloseCurlyBrackettoken, OpenSquareBrackettoken,
        CloseSquareBrackettoken, GreaterThantoken, GreaterEqualThantoken,
        QuestionMarktoken, LessEqualThantoken, LessThantoken,
        Hashrockettoken, InequalityOperatortoken,
        AtSigntoken,
        DoubleAtSigntoken,
        Exclamationtoken
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
                return (bool)value;
            }
            throw new Exception("GwentObject non-booleable");
        }

        public static GwentObject operator +(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-addable");
            }

            double leftValue = (double)left.value;
            double rightValue = (double)right.value;

            return new GwentObject(leftValue + rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator -(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-substractable");
            }

            double leftValue = (double)left.value;
            double rightValue = (double)right.value;

            return new GwentObject(leftValue - rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator *(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-multipliable");
            }

            double leftValue = (double)left.value;
            double rightValue = (double)right.value;

            return new GwentObject(leftValue * rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator /(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-dividable");
            }

            double leftValue = (double)left.value;
            double rightValue = (double)right.value;

            return new GwentObject(leftValue / rightValue, GwentType.GwentNumber);
        }

        public static GwentObject operator %(GwentObject left, GwentObject right)
        {
            if (left.type != GwentType.GwentNumber || right.type != GwentType.GwentNumber)
            {
                throw new Exception("GwentObject non-modulable");
            }

            double leftValue = (double)left.value;
            double rightValue = (double)right.value;

            return new GwentObject(leftValue % rightValue, GwentType.GwentNumber);
    }
    public enum GwentType
    {
        GwentNumber, GwentString, GwentBool, GwentVoid, GwentNull,
    }

}
}