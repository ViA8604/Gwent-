using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class TriggerPlayerExpression : IExpression
    {
        public TriggerPlayerExpression()
        {

        }

        public bool CheckSemantic()
        {
            return true;
        }

        public GwentObject Evaluate()
        {
            int num = CompilerUtils.gameManager.GetCurrentPlayerNum();
            return new GwentObject(num, GwentType.GwentNumber);
        }

        public GwentType ReturnType => GwentType.GwentNumber;
    }

    public class NotTriggerPlayerExpression : IExpression
    {
        public NotTriggerPlayerExpression()
        {

        }

        public bool CheckSemantic()
        {
            return true;
        }

        public GwentObject Evaluate()
        {
            int num = CompilerUtils.gameManager.NotCurrentPlayerID();
            return new GwentObject(num, GwentType.GwentNumber);
        }

        public GwentType ReturnType => GwentType.GwentNumber;
    }
}