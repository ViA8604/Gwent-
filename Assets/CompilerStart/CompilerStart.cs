using System.Dynamic;
using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Diagnostics;
//using UnityEngine;
namespace GwentCompiler
{
    public class GwentCompiler
    {
        public Lexer lexer;

        public Parser parser;

        public AST ast;

        public GwentCompiler(string text, string tag)
        {
            CompilerUtils.tag = tag;
            lexer = new Lexer(text);
            List<Token> tokens = lexer.Tokenize();
            parser = new Parser(tokens);
            ast = parser.ParseCode();
            ast.Run();

            
        }
    }
}
