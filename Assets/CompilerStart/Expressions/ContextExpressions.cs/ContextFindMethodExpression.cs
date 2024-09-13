using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class ContextFindMethodExpression : IExpression
    {
        IExpression zone;
        Scope scope;
        FunctionExpression predicate;
        public ContextFindMethodExpression(IExpression Zone, Scope Scope, FunctionExpression Predicate)
        {
            zone = Zone;
            scope = Scope;
            predicate = Predicate;
        }

        public bool CheckSemantic()
        {
            zone.CheckSemantic();
            if (zone.ReturnType != GwentType.Zone)
            {
                throw new Exception($"Cannot  apply Shuffle into a non zone expression {zone.ReturnType}");
            }
            predicate.CheckSemantic();
            if (predicate.ReturnType != GwentType.GwentBool)
            {
                throw new Exception($"Cannot  apply Shuffle into a non zone predicate {zone.ReturnType}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            List<CompilerCard> cards = new List<CompilerCard>();
            var ZoneOb = (ZoneObj)zone.Evaluate().value;   
            var list = CompilerUtils.gameManager.GetZoneCardNames(ZoneOb);
            foreach (var item in list)
            {
                var value = new GwentObject(item, GwentType.CardType);
                if(predicate.Call(new List <GwentObject>{value}).ToBool())
                {
                cards.Add(item);
                }

            }
            return new GwentObject(cards, ReturnType);

        }

        public GwentType ReturnType => GwentType.GwentList;
    }
}