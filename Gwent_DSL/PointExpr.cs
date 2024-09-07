namespace GWENT_DSL;

internal class PointExpr : Expr
{
    public Expr Left { get; }
    public Expr Right { get; }
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
   
    public override TokenType? Type{get; protected set;}

    public PointExpr (Expr left, Expr right)
    {
        Left = left;
        Right = right;
    }

    public override bool CheckSemantic(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        var left=Left.Evaluate(scope);
         var right=Right as FunctionExpression;
         return right!.Evaluate(scope,left);
       
         
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
