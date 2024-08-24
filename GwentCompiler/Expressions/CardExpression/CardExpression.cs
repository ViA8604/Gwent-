using System;

namespace GwentCompiler
{
    public class CardExpression : IExpression
    {
        string type;
        string name;
        string faction;
        string power;
        List<string> range;

        public CardExpression(string Type, string Name, string Faction, string Power, List<string> Range)
        {
            type = Type;
            name = Name;
            faction = Faction;
            power = Power;
            range = Range;
        }

        public bool CheckSemantic()
        {
            return true;
        }

        public GwentObject Evaluate()
        {
            return new GwentObject(0, GwentType.GwentVoid);
        }

        public GwentType ReturnType => GwentType.GwentVoid;

    }
}