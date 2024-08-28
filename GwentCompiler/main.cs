using System;
namespace GwentCompiler
{
public class Program
    {
        static void Main()
        {
            // simula la terminal del juego
            Console.Write("> ");
            string? text = Console.ReadLine();
            text ??= "";

            GwentCompiler mycompiler = new (text);
            
        }
    }
}