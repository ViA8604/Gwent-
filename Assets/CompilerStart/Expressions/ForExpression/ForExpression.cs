using System.Linq.Expressions;
using System;
using System.Collections.Generic;

namespace GwentCompiler
{
    public class ForExpression : IExpression
    {
        string name;
        IExpression iterableExp;
        List<IExpression> body;
        Scope scope;
        public ForExpression(string Name, IExpression IterableExp, List<IExpression> Body, Scope Scope)
        {
            name = Name;
            iterableExp = IterableExp;
            body = Body;
            scope = Scope;
        }

        public bool CheckSemantic()
        {
            iterableExp.CheckSemantic();
            if (iterableExp.ReturnType != GwentType.Zone && iterableExp.ReturnType != GwentType.GwentList)
            {
                throw new Exception($"Collection {iterableExp.ReturnType} of For statement must be listable");
            }
            scope.SetType(name, GwentType.CardType);
            foreach (var item in body)
            {
                item.CheckSemantic();
            }
            return true;
        }

        public GwentObject Evaluate()
        {
            List<CompilerCard> list = new List<CompilerCard>();
            if (iterableExp.ReturnType == GwentType.Zone)
            {
                ZoneObj zoneOb = iterableExp.Evaluate().value as ZoneObj;
                list = CompilerUtils.gameManager.GetZoneCardNames(zoneOb);

            }
            else if (iterableExp.ReturnType == GwentType.GwentList)
            {
                list = iterableExp.Evaluate().value as List<CompilerCard>;
            }
            foreach (var item in list)
            {
                scope.SetValue(name, new GwentObject(item, GwentType.CardType));
                foreach (var exp in body)
                {
                    exp.Evaluate();
                }
            }
            return new GwentObject(0, GwentType.GwentVoid);

        }

        public GwentType ReturnType => GwentType.GwentNull;

    }

}