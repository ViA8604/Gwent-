using System.Dynamic;
using System.Runtime.CompilerServices;

namespace GwentCompiler
{
    public class GwentCompiler
    {
        public Lexer lexer;

        public Parser parser;

        public AST ast;

        public GwentCompiler(string text)
        {
            lexer = new Lexer(text);
            List<Token> tokens = lexer.Tokenize();
           /* foreach (var item in tokens)
            {
                Console.WriteLine(item);
            }*/
            
            parser = new Parser(tokens);
            ast = parser.ParseCode();

            Console.WriteLine(ast.ToString());
        }
    }
}
