

using System.Collections.Generic;
using System.Linq;

public class Scope
{
    public List<ID> VarExpresions {get; set;}
    public Scope ? Parent{get; set;}

    public Scope()
    {
        VarExpresions = new();
    }

    public Scope(params ID[] varExpressions)
    {
        VarExpresions = varExpressions.ToList();
    }
    
    public Scope CreateChild()
    {
        Scope child = new();
        child.Parent = this;

        return child;
    }
}
