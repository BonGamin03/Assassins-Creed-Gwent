

using System;

public class BinaryExpr : Expr {
    public BinaryExpr(Expr left, Token token, Expr right)
    {
        Right = right;
        Left  = left;
        Type = token.TokenType;
    }

    public Expr  Right {get;protected set;} 
    public Expr  Left {get;protected set;}
    public override TokenType? Type {get; protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(Scope scope)
    {
       throw new  NotImplementedException();
    }

    public override object  Evaluate(Scope scope)
    {
        switch (Type)
        {
            case TokenType.OR:
            return (bool)Left.Evaluate(scope) || (bool)Right.Evaluate(scope);
            
            case TokenType.AND:
            return (bool)Left.Evaluate(scope) && (bool)Right.Evaluate(scope);

            case TokenType.EQUAL_EQUAL:
            return Left.Evaluate(scope).Equals(Right.Evaluate(scope));

            case TokenType.BANG_EQUAL:
            return !Left.Evaluate(scope).Equals(Right.Evaluate(scope));

            case TokenType.GREATHER:
            return (double)Left.Evaluate(scope) > (double) Right.Evaluate(scope);

            case TokenType.GREATHER_EQUAL:
            return (double)Left.Evaluate(scope) >= (double) Right.Evaluate(scope);

            case TokenType.LESS:
            return (double)Left.Evaluate(scope) < (double) Right.Evaluate(scope);

            case TokenType.LESS_EQUAL:
            return (double)Left.Evaluate(scope) <= (double) Right.Evaluate(scope);

            case TokenType.PLUS:
            return (double)Left.Evaluate(scope) + (double) Right.Evaluate(scope);

            case TokenType.MINUS:
            return (double)Left.Evaluate(scope) - (double) Right.Evaluate(scope);

            case TokenType.PRODUCT:
            return (double)Left.Evaluate(scope) * (double) Right.Evaluate(scope);

            case TokenType.DIVITION:
            return (double)Left.Evaluate(scope) / (double) Right.Evaluate(scope);

            case TokenType.POTENCE:
            return Math.Pow((double)Left.Evaluate(scope),(double)Right.Evaluate(scope));
            
            case TokenType.CONC:
            return (string)Left.Evaluate(scope) + (string) Right.Evaluate(scope);
            
            case TokenType.TWO_CONC:
            return (string)Left.Evaluate(scope)+" "+(string) Right.Evaluate(scope);

            default:
            throw new Exception();
        }
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
