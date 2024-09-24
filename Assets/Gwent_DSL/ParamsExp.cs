

using System;
using System.Collections.Generic;

public class ParamsExp : Expr
{
    public ParamsExp()
    {
        Type = TokenType.PARAMS;
    }

    public List<ID> ? NameParam {get; set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(Scope scope)
    {
        return true;
    }

    public override object Evaluate(Scope scope)
    {
        foreach (var item in NameParam!)
        {
            item.Evaluate(scope);
        }

        return null!;
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
