

using System;

public class OnActivationStat : Expr
{
    public EffectAsig ? EffectAsigment{get; set;}
    public Selector ? SelectAsigment{get; set;}
    public PostAction ? PostActionAsig{get; set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public OnActivationStat()
    {
        Type = TokenType.ON_ACTIVATION_EFFECT;
    }
    
    public OnActivationStat(EffectAsig effectAsigment, Selector selectAsigment, PostAction postActionAsig)
    {
        EffectAsigment = effectAsigment;
        SelectAsigment = selectAsigment;
        PostActionAsig = postActionAsig;
        Type = TokenType.ON_ACTIVATION_EFFECT;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
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
