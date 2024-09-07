namespace GWENT_DSL;

public class String : AtomicExp
{
    public String(string value)
    {
        Type = TokenType.STRING;
        ExpValue = value;
    }
    public override TokenType? Type {get;protected set;}
    public string ExpValue {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        return ExpValue;
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
