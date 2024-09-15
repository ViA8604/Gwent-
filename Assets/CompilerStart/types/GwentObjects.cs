using System;
namespace GwentCompiler
{
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
}