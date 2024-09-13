using System.Collections.Generic;
using System;
using System.Linq;

namespace GwentCompiler
{
    public class Parser
    {
        List<Token> tokens;
        int pointer = 0;
        Token Current => Peek(0);

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            CheckSymbolBalance();
        }

        Token Peek(int d)
        {
            int index = Math.Min(pointer + d, tokens.Count - 1);    //para que no se pase
            return tokens[index];
        }

        Token NextToken()
        {                               //ves el token actual y mueves el puntero
            var current = Current;
            pointer++;
            return current;
        }
        Token MatchKind(TokenType type)
        {
            if (Current.Type == type)
            {
                return NextToken();     //devuelve el actual y mueve el puntero
            }
            throw new InvalidCastException($"*Syntax Error*: Received {Current.Type} while expecting {type} ({Current.Line} , {Current.Col})");
        }

        Token MatchKind(List<TokenType> types)
        {
            if (types.Contains(Current.Type))
            {
                return NextToken();     //devuelve el actual y mueve el puntero
            }

            string expected = "(";
            foreach (var t in types)
            {
                expected += t.ToString() + " ";
            }
            expected += ")";
            throw new InvalidCastException($"*Syntax Error*: Received {Current.Type} while expecting {expected} ({Current.Line} , {Current.Col})");
        }

        void CheckSymbolBalance()
        {
            List<TokenType> openSymbols = new() {
                TokenType.OpenParenthesistoken,
                TokenType.OpenCurlyBrackettoken,
                TokenType.OpenSquareBrackettoken
            };

            List<TokenType> closeSymbols = new() {
                TokenType.CloseParenthesistoken,
                TokenType.CloseCurlyBrackettoken,
                TokenType.CloseSquareBrackettoken
            };

            var S = new Stack<TokenType>();
            foreach (var token in tokens)
            {
                if (openSymbols.Contains(token.Type))
                {
                    S.Push(token.Type);
                }

                if (closeSymbols.Contains(token.Type))
                {
                    if (S.Peek() == openSymbols[closeSymbols.IndexOf(token.Type)])   // si tienen el mismo indice en las listas
                    {
                        S.Pop();
                    }
                    else
                    {
                        throw new Exception($"Unbalanced use of symbols ({token.Line} , {token.Col}).");
                    }
                }
            }
            if (S.Count > 0)
            {
                throw new Exception($"Unbalanced use of symbols");
            }
        }

        //____________________________________________________________________________________________________________________________________________________________________

        public AST ParseCode()
        {
            return new AST(ParseStatementList());
        }

        List<IExpression> ParseStatementList()
        {
            var globalscope = new Scope();
            List<IExpression> outExpressions = new();
            while (Current.Type != TokenType.EOFtoken)
            {
                outExpressions.Add(ParseStatement(globalscope));
            }
            return outExpressions;
        }

        IExpression ParseStatement(Scope currentScope)
        {
            switch (NextToken().Type)
            {
                case (TokenType.KeywordDeclarationEffecttoken):
                    return ParseEffectDeclarationExpression(currentScope);
                case (TokenType.KeywordCardtoken):
                    return ParseCardDeclarationExpression(currentScope);
                default:
                    throw new Exception("Unexpected expression declaration in main");
            }
        }

        IExpression ParseEffectDeclarationExpression(Scope scope)
        {
            var newScope = new Scope(scope);

            MatchKind(TokenType.OpenCurlyBrackettoken);
            MatchKind(TokenType.KeywordNametoken);
            MatchKind(TokenType.Colontoken);

            var nameToken = MatchKind(TokenType.StringLiteraltoken);
            MatchKind(TokenType.Commatoken);

            List<(string, GwentType)> paramsList = new List<(string, GwentType)>();

            if (Current.Type == TokenType.KeywordParamstoken)
            {
                NextToken();
                MatchKind(TokenType.Colontoken);
                paramsList = GetParamsList();
            }

            MatchKind(TokenType.KeywordActiontoken);
            MatchKind(TokenType.Colontoken);

            var ActionFExpression = ParseFunctionDeclaration(newScope, GwentType.GwentNull);
            MatchKind(TokenType.CloseCurlyBrackettoken);
            return new EffectDeclarationExpression(nameToken.Value, paramsList, ActionFExpression, newScope);
        }

        private IExpression ParseCardDeclarationExpression(Scope currentScope)
        {
            MatchKind(TokenType.OpenCurlyBrackettoken);

            MatchKind(TokenType.KeywordCardTypetoken);
            MatchKind(TokenType.Colontoken);
            var cardType = MatchKind(TokenType.StringLiteraltoken).Value;
            MatchKind(TokenType.Commatoken);

            MatchKind(TokenType.KeywordNametoken);
            MatchKind(TokenType.Colontoken);
            var cardName = MatchKind(TokenType.StringLiteraltoken).Value;
            MatchKind(TokenType.Commatoken);

            MatchKind(TokenType.KeywordFactiontoken);
            MatchKind(TokenType.Colontoken);
            var cardFaction = MatchKind(TokenType.StringLiteraltoken).Value;
            MatchKind(TokenType.Commatoken);

            MatchKind(TokenType.KeywordPowertoken);
            MatchKind(TokenType.Colontoken);
            var cardPower = ParseExpression(currentScope);

            MatchKind(TokenType.Commatoken);

            MatchKind(TokenType.KeywordRangetoken);
            MatchKind(TokenType.Colontoken);
            MatchKind(TokenType.OpenSquareBrackettoken);
            List<string> ranges = new List<string>();
            while (Current.Type != TokenType.CloseSquareBrackettoken)
            {
                ranges.Add(MatchKind(TokenType.StringLiteraltoken).Value);
                if (MatchKind(new List<TokenType>() { TokenType.Commatoken, TokenType.CloseSquareBrackettoken }).Type == TokenType.CloseSquareBrackettoken)
                {
                    break;
                }
            }
            MatchKind(TokenType.Commatoken);

            var image = "DefaultImage";
            if (Current.Type == TokenType.ImageKeywordtoken)
            {
                NextToken();
                MatchKind(TokenType.Colontoken);
                image = MatchKind(TokenType.StringLiteraltoken).Value;
                MatchKind(TokenType.Commatoken);
            }

            MatchKind(TokenType.KeywordOnActivationtoken);
            MatchKind(TokenType.Colontoken);
            MatchKind(TokenType.OpenSquareBrackettoken);
            MatchKind(TokenType.OpenCurlyBrackettoken);
            var effectCall = ParseEffectCall(currentScope);
            MatchKind(TokenType.CloseCurlyBrackettoken);

            return new CardExpression(cardType, cardName, cardFaction, cardPower, ranges, image, effectCall);
        }

        private EffectCallExpression ParseEffectCall(Scope scope)
        {
            MatchKind(TokenType.KeywordEffectCalltoken);
            MatchKind(TokenType.Colontoken);

            MatchKind(TokenType.OpenCurlyBrackettoken);
            MatchKind(TokenType.KeywordNametoken);
            MatchKind(TokenType.Colontoken);

            var effectname = MatchKind(TokenType.StringLiteraltoken).Value;

            MatchKind(TokenType.Commatoken);

            List<(string, IExpression)> paramsList = new List<(string, IExpression)>();

            while (Current.Type != TokenType.CloseCurlyBrackettoken)
            {
                var paramName = MatchKind(TokenType.IDToken).Value;
                MatchKind(TokenType.Colontoken);

                var paramValue = ParseExpression(scope);
                paramsList.Add((paramName, paramValue));
                MatchKind(TokenType.Commatoken);
                if (Current.Type == TokenType.CloseCurlyBrackettoken)
                {
                    break;
                }
            }
            NextToken();

            MatchKind(TokenType.KeywordSelectortoken);
            MatchKind(TokenType.Colontoken);
            MatchKind(TokenType.OpenCurlyBrackettoken);
            MatchKind(TokenType.KeywordSourcetoken);
            MatchKind(TokenType.Colontoken);

            var source = MatchKind(TokenType.StringLiteraltoken).Value;
            IExpression player = new TriggerPlayerExpression();
            if (source.StartsWith("other"))
            {
                source = source.Substring("other".Length);
                player = new NotTriggerPlayerExpression();
            }
            source = char.ToUpper(source[0]) + source.Substring(1);

            MatchKind(TokenType.Commatoken);
            MatchKind(TokenType.KeywordSingletoken);
            MatchKind(TokenType.Colontoken);
            var single = MatchKind(new List<TokenType>() { TokenType.KeywordTruetoken, TokenType.KeywordFalsetoken });

            bool value = single.Type == TokenType.KeywordTruetoken ? true : false;

            MatchKind(TokenType.Commatoken);
            MatchKind(TokenType.KeywordPredicatetoken);
            MatchKind(TokenType.Colontoken);
            var predicate = ParseFunctionDeclaration(scope, GwentType.CardType);

            var selector = new SelectorExpression(source, player, value, predicate);

            MatchKind(TokenType.CloseCurlyBrackettoken);
            MatchKind(TokenType.CloseCurlyBrackettoken);
            MatchKind(TokenType.CloseSquareBrackettoken);

            return new EffectCallExpression(effectname, paramsList, selector);
        }

        private FunctionExpression ParseFunctionDeclaration(Scope scope, GwentType paramType)
        {
            var newScope = new Scope(scope);
            List<(string, GwentType)> paramsList = new List<(string, GwentType)>();
            List<IExpression> body = new List<IExpression>();

            MatchKind(TokenType.OpenParenthesistoken);

            while (Current.Type != TokenType.CloseParenthesistoken)
            {
                var paramName = MatchKind(new List<TokenType>() { TokenType.IDToken, TokenType.KeywordTargetstoken, TokenType.KeywordContextoken });

                if (paramName.Type == TokenType.IDToken)
                    paramsList.Add((paramName.Value, paramType));

                if (Current.Type == TokenType.CloseParenthesistoken)
                    break;

                MatchKind(TokenType.Commatoken);
            }
            NextToken();

            MatchKind(TokenType.Hashrockettoken);

            if (Current.Type == TokenType.OpenCurlyBrackettoken)
                GetExpressionBody(newScope, body);

            else
                body.Add(ParseExpression(newScope));

            return new FunctionExpression(paramsList, body, newScope);
        }

        private IExpression ParseExpression(Scope scope)
        {
            if (Current.Type == TokenType.KeywordWhiletoken)
                return ParseWhileExpression(scope);

            else if (Current.Type == TokenType.KeywordFortoken)
                return ParseForExpression(scope);

            else if (Current.Type == TokenType.KeywordIftoken)
            {
                return ParseIfExpression(scope);
            }

            return ParseAssignmentExpression(scope);
        }

        private IExpression ParseSetCardPropertyMethod(Scope scope)
        {
            var name = MatchKind(TokenType.IDToken).Value;
            MatchKind(TokenType.DotSymbToken);

            List<TokenType> cardproperties = new List<TokenType>() { TokenType.KeywordCardTypetoken, TokenType.KeywordNametoken, TokenType.KeywordFactiontoken, TokenType.KeywordPowertoken };

            if (!cardproperties.Contains(Current.Type))
            {
                throw new Exception($"{Current.Value} is not a property");
            }
            var property = NextToken().Value;
            MatchKind(TokenType.SymbolEqualtoken);

            var setvalue = ParseAssignmentExpression(scope);

            return new ContextSetCardPropertyExpression(name, property, setvalue, scope);
        }

        private IExpression ParsePropertyAccessMethod(Scope scope, IExpression name)
        {
            MatchKind(TokenType.DotSymbToken);

            List<TokenType> cardproperties = new List<TokenType>() { TokenType.KeywordCardTypetoken, TokenType.KeywordNametoken, TokenType.KeywordFactiontoken, TokenType.KeywordPowertoken, TokenType.KeywordOwnertoken};

            if (cardproperties.Contains(Current.Type))
            {
                return new ContextCardPropertiesExpression(name, NextToken().Value, scope);
            }

            throw new Exception($"{Current.Value} is not a card property");
        }

        private IExpression ParseAssignmentExpression(Scope scope)
        {
            if (Current.Type == TokenType.IDToken && Peek(1).Type == TokenType.SymbolEqualtoken)
            {
                var nameVar = NextToken().Value;
                MatchKind(TokenType.SymbolEqualtoken);
                IExpression assignment = ParseAssignmentExpression(scope);

                return new AssignmentExpression(nameVar, scope, assignment);
            }
            else if (Current.Type == TokenType.IDToken && Peek(1).Type == TokenType.DotSymbToken && Peek(3).Type == TokenType.SymbolEqualtoken)
            {
                return ParseSetCardPropertyMethod(scope);
            }
            else
            {
                return ParseOrExpression(scope);
            }

        }

        private IExpression ParseIfExpression(Scope scope)
        {
            List<IExpression> truebody = new List<IExpression>();
            List<IExpression> falsebody = new List<IExpression>();
            NextToken();  // se salta el if

            MatchKind(TokenType.OpenParenthesistoken);
            var condition = ParseExpression(scope);
            MatchKind(TokenType.CloseParenthesistoken);

            GetExpressionBody(scope, truebody);

            if (Peek(1).Type == TokenType.KeywordElsetoken)
            {
                MatchKind(TokenType.Semicolontoken);
                NextToken();    // se brinca el else
                GetExpressionBody(scope, falsebody);
            }

            return new ConditionalExpression(condition, falsebody, truebody);
        }

        private void GetExpressionBody(Scope scope, List<IExpression> body)
        {
            MatchKind(TokenType.OpenCurlyBrackettoken);
            while (Current.Type != TokenType.CloseCurlyBrackettoken)
            {
                body.Add(ParseExpression(scope));
                MatchKind(TokenType.Semicolontoken);
            }
            NextToken();
        }

        private IExpression ParseContextExpression(Scope scope)
        {
            NextToken();
            MatchKind(TokenType.DotSymbToken);
            if (Current.Type == TokenType.KeywordTriggerPlayertoken)
            {
                NextToken();
                return new TriggerPlayerExpression();
            }
            else if (CompilerUtils.CardZoneKeywords.Contains(Current.Type))
            {
                return new ContextZonesExpression(NextToken().Value, new TriggerPlayerExpression());
            }
            else if (CompilerUtils.NameZoneFKeywords.Keys.Contains(Current.Type))
            {
                //Formal Context Expression
                var name = CompilerUtils.NameZoneFKeywords[NextToken().Type];

                MatchKind(TokenType.OpenParenthesistoken);
                var playerID = ParseExpression(scope);
                MatchKind(TokenType.CloseParenthesistoken);

                return new ContextZonesExpression(name, playerID);
            }
            throw new Exception("Unrecognized context expression: " + Current.Value);
        }

        private IExpression ParseCardListMethodExpression(IExpression zone, Scope scope)
        {
            MatchKind(TokenType.DotSymbToken);
            switch (Current.Value)
            {
                //No params methods
                case "Shuffle":
                case "Pop":
                    return ParseNoParametersMethod(zone, scope);

                case "Push":
                case "Remove":
                case "SendBottom":
                    return ParseParametersMethod(zone, scope);

                default:
                    throw new Exception("Unrecognized method: " + Current.Value);
            }
        }

        private IExpression ParseParametersMethod(IExpression zone, Scope scope)
        {
            var token = NextToken();
            MatchKind(TokenType.OpenParenthesistoken);

            if (token.Type == TokenType.FindKeywordtoken)
            {
                var predicate = ParseFunctionDeclaration(scope, GwentType.CardType);
                MatchKind(TokenType.CloseParenthesistoken);
                return new ContextFindMethodExpression(zone, scope, predicate);
            }
            else
            {
                var variable = ParseAssignmentExpression(scope);
                MatchKind(TokenType.CloseParenthesistoken);
                if (token.Type == TokenType.PushMethodtoken) return new ContextPushMethodExpression(zone, variable, scope);
                else if (token.Type == TokenType.RemoveMethodtoken) return new ContextRemoveMethodExpression(zone, variable, scope);
                else return new ContextSendBottomMethodExpression(zone, variable, scope);
            }
        }



        private IExpression ParseNoParametersMethod(IExpression zone, Scope scope)
        {
            var token = NextToken();
            MatchKind(TokenType.OpenParenthesistoken);
            MatchKind(TokenType.CloseParenthesistoken);
            if (token.Type == TokenType.PopMethodtoken) return new ContextPopMethodExpression(zone, scope);
            return new ContextShuffleMethodExpression(zone, scope);
        }


        private IExpression ParseForExpression(Scope scope)
        {
            NextToken();

            List<IExpression> body = new List<IExpression>();

            var valuename = MatchKind(TokenType.IDToken).Value;
            MatchKind(TokenType.KeywordIntoken);

            var start = ParseExpression(scope);

            GetExpressionBody(scope, body);

            return new ForExpression(valuename, start, body, scope);
        }

        private IExpression ParseWhileExpression(Scope scope)
        {
            var newScope = new Scope(scope);
            List<IExpression> body = new List<IExpression>();
            NextToken();

            MatchKind(TokenType.OpenParenthesistoken);
            var condition = ParseExpression(newScope);
            MatchKind(TokenType.CloseParenthesistoken);

            GetExpressionBody(newScope, body);

            return new WhileExpression(condition, body);
        }

        private IExpression ParseOrExpression(Scope scope)
        {
            var left = ParseAndExpression(scope);
            while (Current.Type == TokenType.KeywordLogicalOrtoken)
            {
                var opToken = NextToken();
                var right = ParseAndExpression(scope);
                left = new LogicalExpression(left, right, GwentPredicates.Or, "||");
            }
            return left;
        }

        private IExpression ParseAndExpression(Scope scope)
        {
            var left = ParseEqualityExpression(scope);
            while (Current.Type == TokenType.KeywordLogicalAndtoken)
            {
                var opToken = NextToken();
                var right = ParseEqualityExpression(scope);
                left = new LogicalExpression(left, right, GwentPredicates.And, "&&");
            }
            return left;
        }

        private IExpression ParseEqualityExpression(Scope scope)
        {
            var left = ParseComparisonExpression(scope);
            while (Current.Type == TokenType.EqualityOperatortoken || Current.Type == TokenType.InequalityOperatortoken)
            {
                var op = NextToken();
                var right = ParseComparisonExpression(scope);
                if (op.Type == TokenType.EqualityOperatortoken)
                    left = new EqualityExpression(left, right, GwentPredicates.Equal, "==");
                else
                    left = new EqualityExpression(left, right, GwentPredicates.NotEqual, "!=");
            }
            return left;
        }

        private IExpression ParseComparisonExpression(Scope scope)
        {
            var left = ParseSumOrSubExpression(scope);
            while (Current.Type == TokenType.LessThantoken || Current.Type == TokenType.GreaterThantoken || Current.Type == TokenType.LessEqualThantoken || Current.Type == TokenType.GreaterEqualThantoken)
            {
                var op = NextToken();
                var right = ParseSumOrSubExpression(scope);
                switch (op.Type)
                {
                    case TokenType.LessThantoken:
                        left = new ComparisonExpression(left, right, GwentPredicates.LessThan, "<");
                        break;
                    case TokenType.GreaterThantoken:
                        left = new ComparisonExpression(left, right, GwentPredicates.GreaterThan, ">");
                        break;
                    case TokenType.LessEqualThantoken:
                        left = new ComparisonExpression(left, right, GwentPredicates.LessEqualThan, "<=");
                        break;
                    case TokenType.GreaterEqualThantoken:
                        left = new ComparisonExpression(left, right, GwentPredicates.GreaterEqualThan, ">=");
                        break;
                }
            }
            return left;
        }

        private IExpression ParseSumOrSubExpression(Scope scope)
        {
            var left = ParseMulOrDivExpression(scope);
            while (Current.Type == TokenType.PlusOperatortoken || Current.Type == TokenType.MinusOperatortoken)
            {
                var op = NextToken(); // -
                var right = ParseMulOrDivExpression(scope);
                switch (op.Type)
                {
                    case TokenType.PlusOperatortoken:
                        left = new ArithmeticExpression(left, right, GwentPredicates.Sum, "+");
                        break;
                    case TokenType.MinusOperatortoken:
                        left = new ArithmeticExpression(left, right, GwentPredicates.Sub, "-");
                        break;
                }
            }
            return left;
        }

        private IExpression ParseMulOrDivExpression(Scope scope)
        {
            var left = ParseTerm(scope);
            while (Current.Type == TokenType.MultiplicationOptoken || Current.Type == TokenType.SymbolInvertedBackSlashtoken || Current.Type == TokenType.Moduletoken)
            {
                var op = NextToken();
                var right = ParseTerm(scope);
                switch (op.Type)
                {
                    case TokenType.MultiplicationOptoken:
                        left = new ArithmeticExpression(left, right, GwentPredicates.Mul, "*");
                        break;
                    case TokenType.SymbolInvertedBackSlashtoken:
                        left = new ArithmeticExpression(left, right, GwentPredicates.Div, "/");
                        break;
                    case TokenType.Moduletoken:
                        left = new ArithmeticExpression(left, right, GwentPredicates.Mod, "%");
                        break;
                }
            }
            return left;
        }

        private IExpression ParseTerm(Scope scope)
        {
            IExpression expression;
            switch (Current.Type)
            {
                case TokenType.OpenParenthesistoken:
                    NextToken();
                    expression = ParseAssignmentExpression(scope);
                    MatchKind(TokenType.CloseParenthesistoken);
                    return expression;

                case TokenType.Exclamationtoken:
                    return new NotExpression(ParseExpression(scope));

                case TokenType.KeywordContextoken:

                    var context = ParseContextExpression(scope);
                    if (Current.Type == TokenType.DotSymbToken)
                    {
                        return ParseCardListMethodExpression(context, scope);
                    }
                    return context;

                case TokenType.KeywordTargetstoken:
                    var targets = new VariableExpression(NextToken().Value, scope);
                    if (Peek(1).Type == TokenType.DotSymbToken)
                    {
                        return ParseCardListMethodExpression(targets, scope);
                    }
                    return targets;

                case TokenType.MinusOperatortoken:
                    return new NegativeNumExpression(ParseExpression(scope));

                case TokenType.IDToken:
                    var variable = new VariableExpression(NextToken().Value, scope);
                    if (Current.Type == TokenType.DotSymbToken)
                    {
                        return ParsePropertyOrMethod(scope, variable);
                    }
                    return variable;

                case TokenType.DigitToken:
                    return new LiteralExpression(new GwentObject(NextToken().Value, GwentType.GwentNumber));

                case TokenType.StringLiteraltoken:
                    return new LiteralExpression(new GwentObject(NextToken().Value, GwentType.GwentString));

                case TokenType.KeywordFalsetoken:
                case TokenType.KeywordTruetoken:
                    return new LiteralExpression(new GwentObject(NextToken().Value, GwentType.GwentBool));

                default:
                    throw new Exception("Unexpected token: " + Current.Value);
            }
        }

        private IExpression ParsePropertyOrMethod(Scope scope, IExpression variable)
        {
            List<TokenType> cardproperties = new List<TokenType>() { TokenType.KeywordCardTypetoken, TokenType.KeywordNametoken, TokenType.KeywordFactiontoken, TokenType.KeywordPowertoken, TokenType.KeywordOwnertoken };
            List<TokenType> cardMethod = new List<TokenType>() { TokenType.ShuffleMethodtoken, TokenType.PopMethodtoken, TokenType.PushMethodtoken, TokenType.RemoveMethodtoken, TokenType.SendBottomMethodtoken };

            if (cardproperties.Contains(Peek(1).Type)) return ParsePropertyAccessMethod(scope, variable);
            else if (cardMethod.Contains(Peek(1).Type)) return ParseCardListMethodExpression(variable, scope);
            throw new Exception($"Unknown property or method: {Current.Value}");
        }


        List<(string, GwentType)> GetParamsList()
        {
            List<TokenType> gwentypes = new List<TokenType>() { TokenType.KeywordNumbertoken, TokenType.KeywordStringtoken, TokenType.KeywordBooltoken };

            List<(string, GwentType)> outList = new List<(string, GwentType)>();

            MatchKind(TokenType.OpenCurlyBrackettoken);
            while (Current.Type != TokenType.CloseCurlyBrackettoken)
            {
                var nameToken = MatchKind(TokenType.IDToken);
                MatchKind(TokenType.Colontoken);
                var typeToken = MatchKind(gwentypes);

                outList.Add((nameToken.Value, GetType(typeToken.Value)));

                if (Current.Type == TokenType.CloseCurlyBrackettoken)
                    break;

                MatchKind(TokenType.Commatoken);
            }
            NextToken();
            return outList;
        }

        private GwentType GetType(string value) //Aqu'i recibes cosas como "Number"
        {
            Dictionary<string, GwentType> types = new Dictionary<string, GwentType>(){
                {"Number", GwentType.GwentNumber},
                {"String", GwentType.GwentString},
                {"Bool", GwentType.GwentBool}
            };
            return types[value];
        }
    }

}