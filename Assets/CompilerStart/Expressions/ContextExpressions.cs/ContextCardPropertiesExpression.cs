using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using GwentPro;
using Unity.VisualScripting;
namespace GwentCompiler
{
    public class ContextCardPropertiesExpression : IExpression
    {
        IExpression cardExp;
        string property;
        Scope scope;
        public ContextCardPropertiesExpression(IExpression CardExp, string Property, Scope Scope)
        {
            cardExp = CardExp;
            property = Property;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            if(cardExp.ReturnType != GwentType.CardType)
            {
                throw new Exception($"Cannot request a card property from {cardExp}, {scope.GetType()}");
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            GameManager manager = CompilerUtils.gameManager;
            var card = (CompilerCard)cardExp.Evaluate().value;
            string cardprop = manager.GetACardProperty(card.cardName, card.zone, property);
            
            return new GwentObject(cardprop, this.ReturnType);
        }

        public GwentType ReturnType => (this.property == "Power" || this.property == "Owner")? GwentType.GwentNumber : GwentType.GwentString;
    }
}