using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class ContextShuffleMethodExpression : IExpression
    {
        IExpression zone;
        Scope scope;
        public ContextShuffleMethodExpression(IExpression Zone, Scope Scope)
        {
            zone = Zone;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            zone.CheckSemantic();
            if (zone.ReturnType != GwentType.Zone)
            {
                throw new Exception($"Cannot  apply Shuffle into a non zone expression {zone.ReturnType}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            var ZoneOb = (ZoneObj)zone.Evaluate().value;   
            CompilerUtils.gameManager.ShuffleInstantiatedZone(ZoneOb);
                   
            return new GwentObject(ZoneOb, ReturnType);
        }

        public GwentType ReturnType => GwentType.Zone;
    }
}