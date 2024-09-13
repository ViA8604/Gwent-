using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
using Unity.VisualScripting;
using Unity.Mathematics;
namespace GwentCompiler
{
    public class ContextSetCardPropertyExpression : IExpression
    {
        string name;
        string property;
        IExpression setValue;
        Scope scope;
        public ContextSetCardPropertyExpression(string Name, string Property, IExpression SetValue, Scope Scope)
        {
            name = Name;
            property = Property;
            setValue = SetValue;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            setValue.CheckSemantic();
            if (scope.GetType(name) != GwentType.CardType)
            {
                throw new Exception($"Cannot request a card property from {name}, {scope.GetType()}");
            }
            else if (this.ReturnType != setValue.ReturnType)
            {
                throw new Exception($"Cannot set card property {property} to a {setValue.ReturnType}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            GameManager manager = CompilerUtils.gameManager;
            string value = setValue.Evaluate().value.ToString();
            CompilerCard card = (CompilerCard)scope.GetValue(name).value;
            manager.SetACardProperty(card.cardName, card.zone, property, value);

            return new GwentObject(0, this.ReturnType);
        }

        public GwentType ReturnType => (this.property == "Power") ? GwentType.GwentNumber : GwentType.GwentString;
    }
}