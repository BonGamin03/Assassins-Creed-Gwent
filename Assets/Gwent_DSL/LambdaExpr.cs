
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LambdaExpr : Expr
{
    public List<ID>  VarExpressions{get; protected set;}
    public Stat  LambdaBody{get;private set; }
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public LambdaExpr(List<ID> varExpressions, Stat lambdaBody, TokenType type){

        VarExpressions = varExpressions;
        LambdaBody = lambdaBody;
        Type       = type ;
    }

    public LambdaExpr(TokenType type){
        Type = type;
        VarExpressions = new();
        LambdaBody = new();
    }

    public override object Evaluate(Scope scope)
    {
        AddingScope(VarExpressions,scope);

        if(Type == TokenType.PREDICATE)
        {
            Predicate<GameObject> predicate = (CardData) => EvaluatPredicat(CardData,scope); // Cambiar en todos los lugares de CardData a GameObject
            return predicate;
        }else{

            Action<List<GameObject>> action = (targets) => EvaluateAction(targets,scope);
            return action;
        }
    }

    private void AddingScope(List<ID> varExpressions, Scope scope)
    {
        foreach (var item in varExpressions)
        {   
            if(!IsAlreadyDeclarated(scope,item.ExpValue))
            scope.VarExpresions.Add(item);
        }
    }

    private void EvaluateAction(List<GameObject> targets, Scope scope)
    {
        VarExpressions[0].VarValue = targets;
        scope.VarExpresions.Find(x => x.ExpValue == "targets").VarValue = targets;
        LambdaBody.Evaluate(scope);
    }

    private bool EvaluatPredicat(GameObject cardData, Scope scope)
    {
        VarExpressions[0].VarValue = cardData;
        scope.VarExpresions.Find(x => x.ExpValue == VarExpressions[0].ExpValue).VarValue = cardData;
        return Stat.EvalBodyPred(scope,LambdaBody.Exprs);
    }

    public override bool CheckSemantic(Scope scope)
    {
        if(Type  == TokenType.PREDICATE)
        {
            if(VarExpressions!.Count != 1 || !CheckOperator(LambdaBody.Exprs.Peek().Type)){throw new Exception("Invalid input in predicate");}

        }else{
            if(VarExpressions!.Count == 2)
            {
                LambdaBody.CheckSemantic(scope);
            }else{throw new Exception("Action only take two arguments");}
        }

        // foreach (var item in VarExpressions) // si se declran dos targets y dos context esto da error
        // {
        //     if(IsAlreadyDeclarated(scope, item.ExpValue)){throw new Exception("Already exist a variable with"+item.ExpValue+"name");}
        // }

        return true;
    }

    private bool CheckOperator(TokenType? type)
    {
       switch (type)
       {
            case TokenType.AND:
            case TokenType.OR:
            case TokenType.EQUAL_EQUAL:
            case TokenType.BANG_EQUAL:
            case TokenType.GREATHER_EQUAL:
            case TokenType.GREATHER:
            case TokenType.LESS_EQUAL:
            case TokenType.LESS:
            case TokenType.TRUE:
            case TokenType.FALSE:
            return true;

            default:
            return false;
       }
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    private bool IsAlreadyDeclarated(Scope scope, string name )
    {
        if(scope is null){return false;}
        else if(scope.VarExpresions.Exists(x=> x.ExpValue == name)){return true;}

        return IsAlreadyDeclarated(scope.Parent!,name);
    }


}