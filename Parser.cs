using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace calc
{
    public class Parser
    {
        private readonly List<Token> _tokens;
        private int _current = 0;
        public Expr Expr {get;}

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            Expr = ParseExpr();
        }

// utils
        private Token Peek() => _tokens[_current];
        private Token Consume() => _tokens[_current++];
        private bool isAtEnd() => Peek().Type != TokenType.Eof;

        private bool Match(params TokenType[] types)
        {
            foreach(var type in types)
            {
                if(Peek().Type == type)
                {
                    return true;
                }
            }

            return false;
        }

// main logic
        private Expr ParseExpr() // plus, minus
        {
            Expr left = ParseTerm();
            while(Match(TokenType.Plus, TokenType.Minus))
            {
                Token op = Consume();
                Expr right = ParseTerm();
                left = new BinaryExpr(left, op, right);
            }

            return left;
        }    

        private Expr ParseTerm() // mult or div
        {
            Expr left = ParseFactor();
            while(Match(TokenType.Mult, TokenType.Div))
            {
                Token op = Consume();
                Expr right = ParseFactor();
                left = new BinaryExpr(left, op, right);
            }

            return left;
        }

        private Expr ParseFactor() // number, (, )
        {
            if(Match(TokenType.Number))
            {
                Token number = Consume();
                return new NumberExpr(number);
            } else if(Match(TokenType.Lparen))
            {
                Consume(); // consume (
                Expr e = ParseExpr();
                if(!Match(TokenType.Rparen))
                {
                    throw new Exception($"error: Innapropriate token, expected ), received {Peek()}");
                }
                Consume(); // consume )
                return e;
            } else
            {
                throw new Exception($"error: Innapropriate token {Peek()}");
            }
        }
    }

// expr
    public abstract class Expr {}

    public class NumberExpr : Expr
    {
        public Token Token {get;}
        public NumberExpr(Token token) => Token = token;
        public override string ToString() => Token.ToString();
    }

    public class BinaryExpr: Expr
    {
        public Expr Left {get;}
        public Token Op {get;}
        public Expr Right {get;}

        public BinaryExpr(Expr left, Token op, Expr right)
        {
            Left = left;
            Op = op;
            Right = right; 
        }

        public override string ToString() => $"[{Left} {Op} {Right}]";
    }
}