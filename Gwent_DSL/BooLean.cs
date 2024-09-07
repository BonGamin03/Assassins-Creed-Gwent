namespace GWENT_DSL;

public class BooLean : AtomicExp
{
    public BooLean(string value,TokenType type)
    {
        ExpValue = value;
        Type = type;
    }

    public override TokenType? Type {get; protected set;}
    public string ExpValue {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        return bool.Parse(ExpValue);
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
