namespace GWENT_DSL;

public class WhileExp : Expr
{
    public Expr? BoolPExpr {get;set;}
    public Stat? Body {get;set;}
    public override TokenType? Type {get; protected set;}
    public override Scope ?Scope {get; protected set; }

    public WhileExp()
    {
        Type = TokenType.WHILE;
        Body = new();
    }

    public WhileExp(Expr boolPExpr, Stat body)
    {
        BoolPExpr = boolPExpr;
        Body      = body;
    }

    public override object Evaluate(Scope scope)
    {
       Scope BodyScope = scope.CreateChild();
       if(BoolPExpr?.Evaluate(scope) is bool x)
       {
            while (x)
            {
                BoolPExpr.Evaluate(BodyScope);
            }
       }else{ throw new Exception(""); }

       return null!;
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
