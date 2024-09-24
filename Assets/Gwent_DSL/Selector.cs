

using System;
using System.Linq;
using UnityEngine;

public class Selector : Expr
{
    public AssigmentExpr ? Source{get; set;}
    public AssigmentExpr ? Single{get; set;}
    public LambdaExpr ? Predicate{get;set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    private static string[] theSources = new[]{"parent","hand","board","otherDeck","deck","otherHand","field","otherField"};
    public Selector(AssigmentExpr source, AssigmentExpr single, LambdaExpr predicate)
    {
        Source = source;
        Single = single;
        Predicate = predicate;
        Type = TokenType.SELECTOR;
    }

    public Selector(AssigmentExpr source)
    {
        Type = TokenType.SELECTOR;
    }

    public Selector()
    {
    }

    public override object Evaluate(Scope scope)
    {
        string source = Source!.Evaluate(scope).ToString()!;
        bool single = (Single is null) ? false : (bool)Single.RightSide!.Evaluate(scope)!;
        Predicate<GameObject> predicate = Predicate is not null? (Predicate<GameObject>)Predicate!.Evaluate(scope) : null;

        return(source,single,predicate);
    }

    public override bool CheckSemantic(Scope scope)
    {
         if(Source is not null)
            {
                if(Source.RightSide!.Evaluate(scope!) is string x)
                {
                    if(theSources.Contains(x))
                    {
                        if(Predicate is null) { return true; }
                        return true && Predicate.CheckSemantic(scope!);
                    }
                    else { throw new Exception("Invalid value "+"'"+Source.RightSide.Evaluate(scope!)!.ToString()+"'"); }
                }
                else { throw new Exception("Invalid value type"); }
            }
            else { throw new Exception("Source is null"); }
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
