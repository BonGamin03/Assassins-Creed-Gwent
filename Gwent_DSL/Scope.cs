namespace GWENT_DSL;

public class Scope
{
    public List<ID> VarExpresions {get; set;}
    public Scope ? Parent{get; set;}

    public Scope()
    {
        VarExpresions = [];
    }

    public Scope(params ID[] varExpressions)
    {
        VarExpresions = [.. varExpressions];
    }
    
    public Scope CreateChild()
    {
        Scope child = new();
        child.Parent = this;

        return child;
    }
}
