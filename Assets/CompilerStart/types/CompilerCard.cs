using System.Numerics;
using System;
using System.Collections.Generic;
namespace GwentCompiler
{
    public class CompilerCard
    {
        public string cardName;
        public ZoneObj zone;
        public CompilerCard(string CardName, ZoneObj Zone)
        {
            cardName = CardName;
            zone = Zone;
        }
    }

    
}