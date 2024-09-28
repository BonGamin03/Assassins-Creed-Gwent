

using System;
using System.Collections.Generic;
using System.Linq;

public class OnActivation : Expr
{
    public List<OnActivationStat> OnActivationBody{get; set;}
    public override TokenType? Type {get;protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public OnActivation(List<OnActivationStat> onActivationBody)
    {
        OnActivationBody = onActivationBody;
        Type = TokenType.ON_ACTIVATION;
    }

    public OnActivation()
    {
        OnActivationBody = new(); //cuidado
        Type = TokenType.ON_ACTIVATION;
    }

    public override object Evaluate(Scope scope)
    {
        List<(EffectNode,Selector)> evaluations = new();

        foreach (var onActivationStat in OnActivationBody)
        {
            EffectNode effectAsig = (EffectNode)onActivationStat.EffectAsigment!.Evaluate(scope);
            Selector selector = onActivationStat.SelectAsigment!;

            evaluations.Add((effectAsig,selector));

            if(onActivationStat.PostActionAsig is not null)
            {
                evaluations.Concat((List<(EffectNode,Selector)>)onActivationStat.PostActionAsig.Evaluate(scope,selector));// chequear que el concat funcione satisfactoriamente 
            }
        }

        return evaluations;
    }

    public override bool CheckSemantic(Scope scope)
    {
          foreach (var efComp in OnActivationBody!)
            {
                if(efComp.EffectAsigment is not null){efComp.EffectAsigment.CheckSemantic(scope);} else throw new Exception("Effect Card declaration can't be null");
                if(efComp.SelectAsigment is not null){efComp.SelectAsigment.CheckSemantic(scope);} //else throw new Exception("Selector Card declaration can't be null");
                if(efComp.PostActionAsig is not null){efComp.PostActionAsig.CheckSemantic(scope);} 
            }
            return true;
    }
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    

}
