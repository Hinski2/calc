using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace calc
{
    public enum TokenType {Number, Plus, Minus, Mult, Div, Lparen, Rparen, Eof};

    public class Token
    {
        public TokenType Type {get;}
        public double? Value {get;}

        public Token(TokenType type, double? value)
        {
            Type = type;
            Value = value;
        }

        public Token(TokenType type) : this(type, null) {}

        public override string ToString()
        {
            return $"[{Type}]";
        }
    }
    public class Lexer
    {
        public List<Token> Tokens {get; set;} = new List<Token>();

        public Lexer(string line)
        {
            for(int i = 0; i < line.Length;)
            {
                if(char.IsWhiteSpace(line[i]))
                {
                    i++;
                } else if(line[i] == '+')
                {
                    Tokens.Add(new Token(TokenType.Plus));
                    i++;
                } else if(line[i] == '-')
                {
                    Tokens.Add(new Token(TokenType.Minus));
                    i++;
                } else if(line[i] == '*')
                {
                    Tokens.Add(new Token(TokenType.Mult));
                    i++;
                } else if(line[i] == '/')
                {
                    Tokens.Add(new Token(TokenType.Div));
                    i++;
                } else if(line[i] == ')')
                {
                    Tokens.Add(new Token(TokenType.Rparen));
                    i++;

                } else if(line[i] == '(')
                {
                    Tokens.Add(new Token(TokenType.Lparen));
                    i++;
                } else if(char.IsNumber(line[i]))
                {
                    var (token, len) = ParseNumber(line.AsSpan(i));
                    Tokens.Add(token);
                    i += len; 
                } else
                {
                    throw new Exception($"error: innapropriate char {line[i]}");
                }
            }

            Tokens.Add(new Token(TokenType.Eof));
        }

        private static (Token, int) ParseNumber(ReadOnlySpan<char> view)
        {
            int len = 0, dots = 0;

            while(len < view.Length)
            {
               if(view[len] == '.')
                {
                    if(dots != 0) throw new Exception($"error: number can't contains two two or more dots");
                    dots++;
                } else if(char.IsNumber(view[len]))
                {
                    len++;
                } else
                {
                    break;
                }
            } 

            double value = double.Parse(view.Slice(0, len).ToString());
            return (new Token(TokenType.Number, value), len);
        }
    }
}