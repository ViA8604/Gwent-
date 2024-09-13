using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class ContextPopMethodExpression : IExpression
    {
        IExpression zone;
        Scope scope;
        public ContextPopMethodExpression(IExpression Zone, Scope Scope)
        {
            zone = Zone;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            zone.CheckSemantic();
            if (zone.ReturnType != GwentType.Zone)
            {
                throw new Exception($"Cannot  apply Pop into a non zone expression {zone.ReturnType}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            GameManager manager = CompilerUtils.gameManager;
            var ZoneOb = (ZoneObj)zone.Evaluate().value;
            
            CompilerCard newCard = manager.PopCard(ZoneOb);

            return new GwentObject(newCard, GwentType.CardType);
        }

        public GwentType ReturnType => GwentType.CardType;
    }
}