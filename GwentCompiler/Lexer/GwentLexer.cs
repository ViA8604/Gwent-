using System.Text;
namespace GwentCompiler
{
    public class Lexer
    {
        string code;                // el texto
        int pointer = 0;
        int currentline;
        int currentcol;

        public Lexer(string textcheckin)
        {
            code = textcheckin;
        }

        private char GetChar(int k)
        {
            //Devuelve el texto, moviéndose según el puntero.
            int pos = pointer + k;
            return (pos >= code.Length) ? '\0' : code[pos];
        }
        private void NextChar()
        {
            //Mueve el puntero
            pointer++;
            currentcol++;
        }

        public List<Token> Tokenize()
        {
            //Devuelve una lista con los tokens, cada token tiene su tipo, y su valor.
            Token token;
            List<Token> tokenlist = [];
            do
            {
                token = GetToken();  //Continuará construyendo los tokens hasta que llegues al final del texto.
                if (token.Type != TokenType.WhiteSpacetoken && token.Type != TokenType.Commenttoken)
                {
                    tokenlist.Add(token);
                }
            }
            while (token.Type != TokenType.EOFtoken);

            return tokenlist;
        }

        private Token GetToken()
        {
            /*Crea el token en dependencia del tipo que es,
            cuando detecta que no puede guardar más de ese mismo token,
            lo devuelve.
            */
            TokenType type;
            string value;

            // "   "
            if(GetChar(0) == '\0')
            {
                value = "\0";
                type = TokenType.EOFtoken;
            }
            else if (char.IsDigit(GetChar(0)))
            {
                BuildDigitToken(out type, out value);
            }
            else if (char.IsWhiteSpace(GetChar(0)))
            {
                BuildWhiteSpaceToken(out type, out value);
            }
            else if (char.IsLetter(GetChar(0)) || GetChar(0) == '_')
            {
                BuildKeywordOrIDToken(out type, out value);
            }
            else if (GetChar(0) == '"')
            {
                BuildStringLiteralToken(out type, out value);
            }
            else if (GetChar(0) == '#')
            {
                BuildCommentToken(out type, out value);
            }
            else
            {
                BuildSymbolToken(out type, out value);
            }
            return new Token(value, type, currentline, currentcol);
        }



        // Conjunto de funciones que contruyen el token de un tipo.
        private void BuildDigitToken(out TokenType type, out string value)
        {
            type = TokenType.DigitToken;
            int startpos = pointer;
            int dotcounter = 0;
            while (char.IsDigit(GetChar(0)) || GetChar(0) == '.' && dotcounter < 1)
            {
                if (GetChar(0) == '.') dotcounter++;
                NextChar();             //Aumenta el puntero.
            }
            int length = pointer - startpos;
            value = code.Substring(startpos, length);
        }

        private void BuildWhiteSpaceToken(out TokenType type, out string value)
        {
            type = TokenType.WhiteSpacetoken;
            int startpos = pointer;
            while (char.IsWhiteSpace(GetChar(0)))
            {
                NextChar();
            }
            int length = pointer - startpos;
            value = code.Substring(startpos, length);
        }

        private void BuildKeywordOrIDToken(out TokenType type, out string value)
        {
            int startpos = pointer;
            while (char.IsLetter(GetChar(0)) || char.IsDigit(GetChar(0)) || GetChar(0) == '_')
            {
                NextChar();
            }
            int length = pointer - startpos;
            value = code.Substring(startpos, length);
            if (!CompilerUtils.Getkeyword.TryGetValue(value, out type))
            {
                type = TokenType.IDToken;
            }
        }

        private void BuildStringLiteralToken(out TokenType type, out string value)
        {
            type = TokenType.StringLiteraltoken ;

            NextChar();                     // para saltar la " de inicio
            var sb = new StringBuilder();
            var done = false;

            while (!done)
            {
                switch (GetChar(0))
                {
                    case '\0':
                        throw new InvalidDataException($"Unterminated string literal({currentline},{currentcol}) : \"{sb}...");
                    case '"':
                        done = true;
                        NextChar();
                        break;

                    default:
                        sb.Append(GetChar(0));
                        NextChar();
                        break;
                }
            }
            value = sb.ToString();
        }

        private void BuildCommentToken(out TokenType type, out string value)
        {
            type = TokenType.Commenttoken;
            int startpos = pointer;
            while (GetChar(0) != '\n' && GetChar(0) != '\0')
            {
                NextChar();              //Aumenta el puntero 
            }
            int Length = pointer - startpos;
            value = code.Substring(startpos, Length);
        }

        private void BuildSymbolToken(out TokenType type, out string value)
        {
            string text = "";   // lo reconocido hasta el momento
            int matches;        // cantidad de operadores que comienzan con lo reconocido hasta el momento
            // "+="
            while(true)
            {   
                matches = CompilerUtils.Getsymbol.Count(KeyValuePair => KeyValuePair.Key.StartsWith(text + GetChar(0)));
                if(matches == 1 && CompilerUtils.Getsymbol.ContainsKey(text))
                {
                    value = text + GetChar(0) ;  // guardamos como valor lo que se reconocio
                    NextChar();
                    break ;
                }
                else if(matches == 0 || GetChar(0)=='\0')
                {
                    if(CompilerUtils.Getsymbol.ContainsKey(text))
                    {
                      value = text ;
                      break ;  
                    }
                    else {
                        throw new InvalidDataException($"Unknown symbol : {text + GetChar(0)}");
                    }
                }

                text += GetChar(0);
                NextChar();
                 
            }
            type = CompilerUtils.Getsymbol[value];
            if(type == TokenType.SymbolNewLinetoken){currentline++;}             
        }
    }
}