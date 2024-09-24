

using System;

public class Number : AtomicExp
{   
    public string ExpValue{get;protected set;}
    public Number(string value)
    {
        ExpValue = value;
        Type = TokenType.NUMBER;
    }
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override object Evaluate(Scope scope )
    {
        return double.Parse(ExpValue);
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
