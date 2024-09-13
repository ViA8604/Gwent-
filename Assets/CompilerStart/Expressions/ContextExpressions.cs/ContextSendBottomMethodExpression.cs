using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
namespace GwentCompiler
{
    public class ContextSendBottomMethodExpression : IExpression
    {
        IExpression zone;
        IExpression parameterExp;
        Scope scope;
        public ContextSendBottomMethodExpression(IExpression Zone, IExpression ParameterExp, Scope Scope)
        {
            zone = Zone;
            parameterExp = ParameterExp;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            zone.CheckSemantic();
            if (zone.ReturnType != GwentType.Zone)
            {
                throw new Exception($"Cannot  apply Push into a non card zone expression {zone.ReturnType}");
            }
            parameterExp.CheckSemantic();
            if (parameterExp.ReturnType != GwentType.CardType)
            {
                throw new InvalidDataException($"Cannot apply Push into a non card expression {parameterExp.ReturnType}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            GameManager manager = CompilerUtils.gameManager;
            var CompCard = (CompilerCard)parameterExp.Evaluate().value;
            var ZoneOb = (ZoneObj)zone.Evaluate().value;

            CompilerCard setCard = manager.SendBottomCard(CompCard, ZoneOb);
            if (parameterExp is VariableExpression)
            {
                VariableExpression variable = (VariableExpression)parameterExp;

                scope.SetValue(variable.name, new GwentObject(setCard, GwentType.CardType));
            }

            return new GwentObject(0, GwentType.GwentNull);
        }

        public GwentType ReturnType => GwentType.CardType;
    }
}