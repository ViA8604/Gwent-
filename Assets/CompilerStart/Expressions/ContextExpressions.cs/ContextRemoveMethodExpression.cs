using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class ContextRemoveMethodExpression : IExpression
    {
        IExpression zone;
        IExpression variable;
        Scope scope;
        public ContextRemoveMethodExpression(IExpression Zone, IExpression Variable, Scope Scope)
        {
            zone = Zone;
            variable = Variable;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            zone.CheckSemantic();
            if (zone.ReturnType != GwentType.Zone || variable.ReturnType != GwentType.CardType)
            {
                throw new Exception($"Cannot  apply Remove into a non card or zone expression {zone.ReturnType} , {variable.ReturnType}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            GameManager manager = CompilerUtils.gameManager;
            var ZoneOb = (ZoneObj)zone.Evaluate().value;

            CompilerCard card = (CompilerCard)variable.Evaluate().value;

            manager.RemoveCard(card.cardName, ZoneOb);
            return new GwentObject(0, GwentType.GwentNull);
        }

        public GwentType ReturnType => GwentType.GwentVoid;
    }
}