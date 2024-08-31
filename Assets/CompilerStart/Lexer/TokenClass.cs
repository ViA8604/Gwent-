namespace GwentCompiler
{
    public class Token
    {
        public TokenType Type;
        public string Value;
        public int Line;
        public int Col;
    
        public Token(string value, TokenType type, int line, int col)
        {
            this.Value = value;
            this.Type = type;
            this.Line = line;
            this.Col = col;
        }
        public override string ToString()
        {
            return $"{{Tipo: {Type} , Valor: {Value}, Coo:({Line},{Col}) }}";
        }
    }
}