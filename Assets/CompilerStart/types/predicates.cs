using System;

namespace GwentCompiler
{
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
        public static GwentObject ConcatString(GwentObject left, GwentObject right)
        {
            if(left.type != GwentType.GwentString || right.type != GwentType.GwentString )
                throw new Exception("Concatenation must be applied to strings");
            
            string newValue = left.value.ToString() + right.value.ToString();
            return new GwentObject(newValue,GwentType.GwentString);
        }
        public static GwentObject ConcatStringWithSpace(GwentObject left, GwentObject right)
        {
            if(left.type != GwentType.GwentString || right.type != GwentType.GwentString )
                throw new Exception("Concatenation must be applied to strings");
            
            string newValue = left.value.ToString() + " " + right.value.ToString();
            return new GwentObject(newValue,GwentType.GwentString);
        } 
    }
}