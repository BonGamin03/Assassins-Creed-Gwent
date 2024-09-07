namespace GWENT_DSL;

public abstract class Expr : ASTNode
{
    public abstract object ? Evaluate(Scope scope);
    public abstract bool CheckSemantic(Scope scope);
    public abstract void GetScope(Scope scope);
    public abstract Scope ? Scope{get;protected set;} 
}
