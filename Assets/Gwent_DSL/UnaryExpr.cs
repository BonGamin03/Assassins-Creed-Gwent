
using System;
using System.Linq;

public class UnaryExpr : Expr {
    public Expr  Content {get; protected set;}
    public override TokenType? Type {get;protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public UnaryExpr(Token token, Expr content){
        Type = token.TokenType;
        Content = content;
    }

    public override object Evaluate(Scope scope)
    {
        switch(Type)
        {
            case TokenType.MINUS:
            return -(double)Content!.Evaluate(scope);

            case TokenType.BANG:
            return !(bool)Content!.Evaluate(scope);

            case TokenType.PLUS:
            return (double)Content!.Evaluate(scope);
            
            case TokenType.PLUS_PLUS :
            case TokenType.MINUS_MINUS:
            var result = Content.Evaluate(scope);
            IncreaseID(scope);
            return result;

            default:
            throw new Exception("Invalid operator");
        }
    }

    public override bool CheckSemantic(Scope scope)
    {
          var operand = Content.Evaluate(scope!);
        if( Type == TokenType.PLUS ||Type== TokenType.MINUS)
        {
            if(operand is double  ) return true;
        }
        else if(Type == TokenType.PLUS_PLUS || Type == TokenType.MINUS_MINUS)
        {
            if(operand is double && Content is ID) return true;
        }
         else if(Type == TokenType.BANG)
         {
            if(operand is bool ) return true;
         }

            throw new Exception($"Unary operator is not define for {operand.GetType()}");
    }
    

    private void IncreaseID (Scope scope)
    {
        if(scope is null){ throw new Exception("The variable doesn't exist in the cuurrent context"); }

        if(scope.VarExpresions.Any( x => x.ExpValue == ((ID)Content).ExpValue)){
            
            foreach (var item in scope.VarExpresions)
            {
                if(item.ExpValue == ((ID)Content).ExpValue)
                {
                   if(Type == TokenType.PLUS_PLUS && item.VarValue is double x){ x+=1; item.VarValue = x; return;}
                   else if(Type == TokenType.MINUS_MINUS && item.VarValue is double y){y-=1; item.VarValue = y; return;}
                }
            }
        }

        IncreaseID(scope.Parent);
    }


    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
