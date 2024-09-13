using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class ContextZonesExpression : IExpression
    {
        string zoneName;
        IExpression playerExp;

        public ContextZonesExpression(string ZoneName, IExpression PlayerExp)
        {
            zoneName = ZoneName;
            playerExp = PlayerExp;
        }

        public bool CheckSemantic()
        {
            playerExp.CheckSemantic();
            if (playerExp.ReturnType != GwentType.GwentNumber)
            {
                throw new Exception($"Player {playerExp} return type must be number");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            int playerID = Convert.ToInt32(playerExp.Evaluate().value);
            if (playerID != 1 && playerID != 0)
            {
                throw new Exception("Player ID must be 0 or 1");
            }
            var zone = new ZoneObj(zoneName, playerID);
            return new GwentObject(zone, GwentType.Zone);
        }

        public GwentType ReturnType => GwentType.Zone;
    }
}