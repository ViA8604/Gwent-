using System.Dynamic;
using System.Runtime.CompilerServices;

namespace GwentCompiler
{
    public class GwentCompiler
    {
        public Lexer lexer;

        public GwentCompiler(string text)
        {
            lexer = new Lexer(text);
        }
    }
}
