using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace calc
{
    public class Program
    {
        static void Main(string [] args)
        {
            TextReader input;
            if(args.Length > 0)
            {
                if(File.Exists(args[0]))
                {
                    input = new StreamReader(args[0]);
                } 
                else
                {
                    throw new Exception($"error: file {args[0]} doesn't exit");
                }
            } else
            {
                input = Console.In;
            }

            string? line;
            while((line = input.ReadLine()) != null)
            {
                Lexer lexer = new Lexer(line);
                Parser parser = new Parser(lexer.Tokens);
                Interpreter interpreter = new Interpreter(parser.Expr);
                Console.WriteLine(interpreter.Value);
            }
        }
    }
}