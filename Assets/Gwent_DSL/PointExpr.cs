

using System;
using System.Collections.Generic;
using UnityEngine;

internal class PointExpr : Expr
{
    public Expr Left {get;}
    public Expr Right {get;}
    public Token Operator {get;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
   
    public override TokenType? Type{get; protected set;}

    public PointExpr (Expr left, Token _operator, Expr right)
    {
        Left = left;
        Operator = _operator;
        Right = right;
    }

    public override bool CheckSemantic(Scope scope)
    {
        if(Operator.TokenType == TokenType.OPEN_BRACKET){

            if(Left.Evaluate(scope) as List<GameObject> is not null)
            return true;
     
            throw new Exception("Left sido of Dot Expression is not an indexer type");
        }

        return true;
    }

    public override object Evaluate(Scope scope)
    {
        CheckSemantic(scope);

        if(Operator.TokenType == TokenType.OPEN_BRACKET && Right.Evaluate(scope) is double x){

            return ((List<GameObject>) Left.Evaluate(scope))[(int)x];

        }else if(Operator.TokenType == TokenType.POINT && Right is FuncExpr funcExpr){

            return funcExpr.Evaluate(scope,Left.Evaluate(scope));
        }

        throw new Exception();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
