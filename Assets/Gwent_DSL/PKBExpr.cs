

using System;

public class PKBExpr : AtomicExp
{
    public PKBExpr( Expr  content, TokenType type)
    {
  
        Content = content;
        Type = type; // Esto devuelve una mierda tal vez utilizando recorrido in order puedo guardar en el value lo que hay en el parentesis
    }

    public PKBExpr(Expr  content){
        Content = content;
    }

    public Expr  Content{get; private set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override object? Evaluate(Scope scope)
    {
        return Content.Evaluate(scope);
    }

    public override bool CheckSemantic(Scope scope)
    {
        throw new Exception();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
