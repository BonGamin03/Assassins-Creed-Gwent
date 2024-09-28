

using System;

public class ForExp : Expr
{
    public InExp  InExpression {get; private set;}
    public Stat   ForBody {get; private set;}
    public override TokenType? Type {get; protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public ForExp()
    {
        Type = TokenType.FOR;
        ForBody = new();
        InExpression = new();
    }

    public ForExp(InExp inExression, Stat forBody)
    {
        InExpression = inExression;
        ForBody = forBody;
    }

    public override object Evaluate(Scope scope)
    {
        int index = 0;

        Scope scopeForBody = scope.CreateChild();
        
        while(InExpression.Evaluate(scope,index) is bool condition && condition )
        {
            index++;
            ForBody.Evaluate(scopeForBody);
        }

        return null!;
    }
    
    public override bool CheckSemantic(Scope scope)
    {
       if(InExpression.CheckSemantic(scope) && ForBody.CheckSemantic(scope)){return true;}
       else{ throw new Exception("Invalid for condition"); }
    }

   

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
