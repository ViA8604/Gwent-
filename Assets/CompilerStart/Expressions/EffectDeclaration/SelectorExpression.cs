using System;
using System.Collections.Generic;
using TMPro;
using GwentPro;
using UnityEngine;
namespace GwentCompiler
{
    public class SelectorExpression : IExpression
    {
        string source;
        IExpression player;
        bool single;
        FunctionExpression predicate;
        public SelectorExpression(string Source, IExpression Player, bool Single, FunctionExpression _predicate)
        {
            source = Source;
            player = Player;
            single = Single;
            predicate = _predicate;
        }
        public bool CheckSemantic()
        {
            predicate.CheckSemantic();
            if (predicate.ReturnType != GwentType.GwentBool)
            {
                throw new Exception("Predicate must be booleable"); //Exception
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            int playerID = Convert.ToInt32(player.Evaluate().value);
            ZoneObj zone = new ZoneObj(source, playerID);

            List<CompilerCard> cards = new List<CompilerCard>();
            List<CompilerCard> list = CompilerUtils.gameManager.GetZoneCardNames(zone);
            foreach (var item in list)
            {
                var value = new GwentObject(item, GwentType.CardType);
                if (predicate.Call(new List<GwentObject> { value }).ToBool())
                {
                    cards.Add(item);
                }
            }
            if (single) cards = new List<CompilerCard> { cards[0] };
            return new GwentObject(cards, GwentType.GwentList);
        }
        public GwentType ReturnType => GwentType.GwentList;

        public override string ToString()
        {
            return $"Source: {source} Single: {single} Predicate: {predicate}";
        }
    }
}