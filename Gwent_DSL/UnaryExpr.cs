namespace GWENT_DSL;

public class UnaryExpr : Expr {
    public Expr ? Content {get; protected set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

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

            default:
            throw new Exception();
        }
    }

    public override bool CheckSemantic(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
