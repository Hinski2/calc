using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace calc
{
    public class Interpreter
    {
        private readonly Expr _expr;
        public double Value {get;}

        public Interpreter(Expr expr)
        {
            _expr = expr;
            Value = Interpret(_expr);
        }

        private static double Interpret(Expr e)
        {
            if(e is NumberExpr ne)
            {
                return ne.Token.Value ?? 0;
            } else if(e is BinaryExpr be)
            {
                double left = Interpret(be.Left);
                double right = Interpret(be.Right);

                return be.Op.Type switch
                {
                    TokenType.Plus => left + right,
                    TokenType.Minus => left - right,
                    TokenType.Mult => left * right,
                    TokenType.Div => left / right,
                    _ => throw new Exception("error: unknown operator type")
                };
            } else
            {
                throw new Exception("error: unktown ast type");
            }
        }
    }
}