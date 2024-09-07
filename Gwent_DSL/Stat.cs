namespace GWENT_DSL;

public class Stat : Expr{

    public Queue<Expr>  Exprs {get; private set;}
    public override TokenType? Type { get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public Stat()
    {
        Exprs = [];
        Type = TokenType.STATEMENT;
    }

    public override object Evaluate(Scope scope)
    {
        CheckSemantic(scope);

        foreach (var item in Exprs)
        {
            item.Evaluate(scope);
        }
        
        return null!;
    }

    public override bool CheckSemantic(Scope scope)
    {
        foreach(var exp in Exprs)
        {
            if(exp is AssigmentExpr || exp is WhileExp || exp is ForExp || exp is UnaryExpr || exp is PointExpr)
            {
                if(exp is UnaryExpr && !(exp.Type == TokenType.PLUS_PLUS) || !(exp.Type == TokenType.MINUS_MINUS))
                    throw new Exception("Invalid declaraction in the Statement body");
            }else{
                throw new Exception("Invalid declaraction in the Statement body");
            }
        }

        return true; 
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
