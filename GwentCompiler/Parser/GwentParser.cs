using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

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
                expected += t.GetType().ToString() + " ";
            }
            expected += ")";
            throw new InvalidCastException($"*Syntax Error*: Received {Current.Type} while expecting {expected} ({Current.Line} , {Current.Col})");
        }

        void CheckSymbolBalance()
        {
            List<TokenType> openSymbols = new()
            {
    TokenType.OpenParenthesistoken,
    TokenType.OpenCurlyBrackettoken,
    TokenType.OpenSquareBrackettoken
};
            List<TokenType> closeSymbols = new()
{
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
                throw new Exception($"Error message");
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
                MatchKind([TokenType.Semicolontoken, TokenType.EOFtoken]);
                if (Current.Type == TokenType.EOFtoken)
                {
                    break;
                }
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

            List<(string, GwentType)> paramsList = [];

            if (NextToken().Type == TokenType.KeywordParamstoken)
            {
                paramsList = GetParamsList();   //Recuerdar que hay que mover el puntero
            }
            MatchKind(TokenType.KeywordActiontoken);
            MatchKind(TokenType.Colontoken);

            var ActionFExpression = ParseFunctionDeclaration(newScope, GwentType.GwentNull);

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
            List<string> ranges = [];
            while (Current.Type != TokenType.CloseSquareBrackettoken)
            {
                ranges.Add(MatchKind(TokenType.StringLiteraltoken).Value);
                if (MatchKind([TokenType.Commatoken, TokenType.CloseSquareBrackettoken]).Type == TokenType.CloseSquareBrackettoken)
                {
                    break;
                }
            }
            MatchKind(TokenType.Commatoken);

            MatchKind(TokenType.KeywordOnActivationtoken);
            MatchKind(TokenType.Colontoken);
            MatchKind(TokenType.OpenSquareBrackettoken);
            MatchKind(TokenType.OpenCurlyBrackettoken);
            var effectCall = ParseEffectCall(currentScope);
            MatchKind(TokenType.CloseCurlyBrackettoken);

            return new CardExpression(cardType, cardName, cardFaction, cardPower, ranges, effectCall);
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

            List<(string, IExpression)> paramsList = [];

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

            MatchKind(TokenType.Commatoken);
            MatchKind(TokenType.KeywordSingletoken);
            MatchKind(TokenType.Colontoken);
            var single = MatchKind([TokenType.KeywordTruetoken, TokenType.KeywordFalsetoken]);

            bool value = single.Type == TokenType.KeywordTruetoken ? true : false;

            MatchKind(TokenType.Commatoken);
            MatchKind(TokenType.KeywordPredicatetoken);
            MatchKind(TokenType.Colontoken);
            var predicate = ParseFunctionDeclaration(scope, GwentType.CardType);

            var selector = new SelectorExpression(source, value, predicate);

            MatchKind(TokenType.CloseCurlyBrackettoken);
            MatchKind(TokenType.CloseCurlyBrackettoken);
            MatchKind(TokenType.CloseSquareBrackettoken);

            return new EffectCallExpression(effectname, paramsList, selector);
        }

        private FunctionExpression ParseFunctionDeclaration(Scope scope, GwentType paramType)
        {
            var newScope = new Scope(scope);

            MatchKind(TokenType.OpenParenthesistoken);
            List<(string, GwentType)> paramsList = [];
            List<IExpression> body = [];

            while (Current.Type != TokenType.CloseParenthesistoken)
            {
                var paramName = MatchKind([TokenType.IDToken, TokenType.KeywordTargetstoken, TokenType.KeywordContextoken]);
                if (paramName.Type == TokenType.IDToken)
                {
                    paramsList.Add((paramName.Value, paramType));
                }
                if (Current.Type == TokenType.CloseParenthesistoken)
                {
                    break;
                }
            }
            NextToken();

            MatchKind(TokenType.Hashrockettoken);
            if (Current.Type == TokenType.OpenCurlyBrackettoken)
            {
                NextToken();
                while (Current.Type != TokenType.CloseCurlyBrackettoken)
                {
                    body.Add(ParseExpression(newScope));
                    MatchKind(TokenType.Semicolontoken);
                }
                NextToken();
            }
            else
            {
                body.Add(ParseExpression(newScope));
            }

            return new FunctionExpression(paramsList, body, newScope);
        }

        private IExpression ParseExpression(Scope scope)
        {
            if (Current.Type == TokenType.KeywordWhiletoken)
            {
                return ParseWhileExpression(scope);
            }
            else if (Current.Type == TokenType.KeywordFortoken)
            {
                return ParseForExpression(scope);
            }
            else if (Current.Type == TokenType.KeywordIftoken)
            {
                return ParseIfExpression(scope);
            }
            else if (Current.Type == TokenType.IDToken && Peek(1).Type == TokenType.SymbolEqualtoken)
            {
                return ParseAssignmentExpression(scope);
            }
            else
            {
                return ParseOrExpression(scope);
            }
        }

        private IExpression ParseAssignmentExpression(Scope scope)
        {
            var nameVar = Current.Value;
            MatchKind(TokenType.SymbolEqualtoken);
            IExpression assignment = ParseExpression(scope);

            return new AssignmentExpression(nameVar, scope, assignment);
        }

        private IExpression ParseIfExpression(Scope scope)
        {
            List<IExpression> truebody = [];
            List<IExpression> falsebody = [];
            NextToken();

            MatchKind(TokenType.OpenParenthesistoken);
            var condition = ParseExpression(scope);
            MatchKind(TokenType.CloseParenthesistoken);

            MatchKind(TokenType.OpenCurlyBrackettoken);
            GetExpressionBody(scope, truebody);

            if (Current.Type == TokenType.KeywordElsetoken)
            {
                Console.WriteLine("else");
                NextToken();
                MatchKind(TokenType.OpenCurlyBrackettoken);
                GetExpressionBody(scope, falsebody);
                NextToken();
            }

            var a = new ConditionalExpression(condition, falsebody, truebody);
            //Console.WriteLine(a);
            return a;
        }

        private void GetExpressionBody(Scope scope, List<IExpression> body)
        {
            while (Current.Type != TokenType.CloseCurlyBrackettoken)
            {
                body.Add(ParseExpression(scope));
                var type = MatchKind([TokenType.Semicolontoken, TokenType.CloseCurlyBrackettoken]);
                if (type.Type == TokenType.CloseCurlyBrackettoken)
                {
                    break;
                }
            }
            NextToken();
        }
        private IExpression ParseForExpression(Scope scope)
        {
            throw new NotImplementedException();
        }

        private IExpression ParseWhileExpression(Scope scope)
        {
            var newScope = new Scope(scope);
            List<IExpression> body = [];
            NextToken();

            MatchKind(TokenType.OpenParenthesistoken);
            var condition = ParseExpression(newScope);
            MatchKind(TokenType.CloseParenthesistoken);

            MatchKind(TokenType.OpenCurlyBrackettoken);

            GetExpressionBody(newScope, body);

            NextToken();

            return new WhileExpression(condition, body);
        }

        private IExpression ParseOrExpression(Scope scope)
        {
            var left = ParseAndExpression(scope);
            while (Current.Type == TokenType.KeywordLogicalAndtoken)
            {
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
                    left = new ComparisonExpression(left, right, GwentPredicates.Equal, "==");
                else
                    left = new ComparisonExpression(left, right, GwentPredicates.NotEqual, "!=");
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
                var op = NextToken();
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
            while (Current.Type == TokenType.MultiplicationEqualOptoken || Current.Type == TokenType.SymbolInvertedBackSlashtoken || Current.Type == TokenType.Moduletoken)
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
                    expression = ParseExpression(scope);
                    MatchKind(TokenType.CloseParenthesistoken);
                    return expression;

                case TokenType.Exclamationtoken:
                    return new NotExpression(ParseExpression(scope));

                case TokenType.MinusOperatortoken:
                    return new NegativeNumExpression(ParseExpression(scope));

                case TokenType.IDToken:
                    return new VariableExpression(NextToken().Value, scope);

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

        List<(string, GwentType)> GetParamsList()
        {
            //Nombre y tipo
            List<TokenType> gwentypes = [TokenType.KeywordNumbertoken, TokenType.KeywordStringtoken, TokenType.KeywordBooltoken];
            List<(string, GwentType)> outList = [];

            MatchKind(TokenType.OpenCurlyBrackettoken);
            while (Current.Type != TokenType.CloseCurlyBrackettoken)
            {
                var nameToken = MatchKind(TokenType.IDToken);
                MatchKind(TokenType.Colontoken);
                var typeToken = MatchKind(gwentypes);

                outList.Add((nameToken.Value, GetType(typeToken.Value)));

                MatchKind([TokenType.Commatoken, TokenType.CloseCurlyBrackettoken]);
            }
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