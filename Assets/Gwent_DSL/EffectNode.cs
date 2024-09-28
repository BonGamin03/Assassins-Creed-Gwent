

using System;

public class EffectNode : Expr {
    public EffectNode()
    {
    }

    public EffectNode(AssigmentExpr name, LambdaExpr action)
    {
        Name   = name;
        Action = action;
    }

    public EffectNode(AssigmentExpr name, ParamsExp _params, LambdaExpr action)
    {
        Name = name;
        Params = _params;
        Action = action;
    }
    

    public AssigmentExpr Name {get; set;}
    public ParamsExp Params{get; set;}
    public LambdaExpr Action{get; set;}
 
    public override TokenType? Type {get;protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(Scope scope)
    {
        if(Name!.RightSide!.Evaluate(scope!) is not string) { throw new Exception("Se esperaba un string como nombre del efecto "); }
        if(Action is null) { throw new Exception("Action can't be null"); }
        
        if(Params is not null )Params!.CheckSemantic(scope);
        Action.CheckSemantic(scope);

        return true;
    }

    public override object Evaluate(Scope scope)
    {
        CheckSemantic(scope);
        if(Params is not null){Params.Evaluate(scope!);}

        return Action!.Evaluate(scope);
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
