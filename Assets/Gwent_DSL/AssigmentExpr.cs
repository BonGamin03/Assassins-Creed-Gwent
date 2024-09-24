

using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AssigmentExpr : Expr
{
    public AssigmentExpr(ID leftSide)
    {
        LeftSide = leftSide;

        if(leftSide.VarType is not null){ TypeAsig = leftSide.VarType; }
    }

    public AssigmentExpr(ID leftSide, string _operator, Expr rightSide)
    {
        LeftSide = leftSide;
        Operator = _operator;
        RightSide = rightSide;
        Type = TokenType.ASSIGMENT_EXP;

        if(leftSide.VarType is not null){ TypeAsig = leftSide.VarType; }
    }

    public AssigmentExpr(Expr leftSide, string _operator, Expr rightSide)
    {
        LeftSide = leftSide;
        Operator = _operator;
        RightSide = rightSide;
        Type = TokenType.ASSIGMENT_EXP;

    }
    
    public Expr LeftSide{get;set;}
    public string Operator{get;set;}
    public Expr RightSide{get; set;}
    public TokenType? TypeAsig{get;set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override bool CheckSemantic(Scope scope)
    {
        return true;
    }
    public override object Evaluate(Scope scope)
    {
        if(LeftSide is PointExpr left && RightSide.Evaluate(scope!) is double z){

            if(((FuncExpr)left.Right).Function == TokenType.POWER && left.Left.Evaluate(scope!) is GameObject card){
                
                if(card.TryGetComponent(out UnityCardScript unityCard)){   //si no funciona acceder al componente power y modificarlo
                    
                    switch (Operator)
                    {
                        case "+=":
                            unityCard.PointAttackCard += (int)z;
                        return null;

                        case "-=":
                            unityCard.PointAttackCard -= (int)z;
                        return null;

                        case "/=":
                            unityCard.PointAttackCard /= (int)z;
                        return null;

                        case "*=":
                            unityCard.PointAttackCard *= (int)z;
                        return null;

                        default:
                        throw new Exception("Invalid operator");
                    }
                }
                else throw new Exception("The Card you chose doesn't have power field");
            }
        }


        var right = RightSide.Evaluate(scope);
        var Left = (ID)LeftSide;
        if(!IsAlreadyAsig(scope)){
        
            if(Operator == "=" || Operator == ":"){

                if(right is bool) scope.VarExpresions.Add(new ID(Left.ExpValue!, TokenType.BOOL, right));
                else if(right is double) scope.VarExpresions.Add(new ID(Left.ExpValue!, TokenType.NUMBER, right));
                else if(right is string) scope.VarExpresions.Add(new ID(Left.ExpValue!, TokenType.STRING, right));
                else if(right is GameObject) scope.VarExpresions.Add(new ID(Left.ExpValue!, TokenType.CARD_GAME_OBJECT,right));
                else if(right is List<GameObject>) scope.VarExpresions.Add(new ID(Left.ExpValue!, TokenType.LIST_CARD_GA_OB,right)); 
            }else{ throw new Exception(Operator+ " is not valid to no declarated variables");}        
        }else{
            ID identifier = null!;
                if(right is bool)   identifier = new ID(Left.ExpValue!, TokenType.BOOL, right);
                if(right is double) identifier = new ID(Left.ExpValue!, TokenType.NUMBER, right);
                if(right is string) identifier = new ID(Left.ExpValue!, TokenType.STRING, right);
                if(right is GameObject) identifier = new ID(Left.ExpValue!, TokenType.CARD_GAME_OBJECT, right);
                if(right is List<GameObject>) identifier = new ID(Left.ExpValue!, TokenType.LIST_CARD_GA_OB,right);
                 
            ID id = GetID(scope)!;
    
           
                if(Operator == "=" || Operator == ":") {id.VarValue = identifier.VarValue;}
                else if(Operator == "+="){if(id.VarValue is double x && identifier.VarValue is double y){x+=y; id.VarValue = x;}}
                else if(Operator == "-="){if(id.VarValue is double x && identifier.VarValue is double y){x-=y; id.VarValue = x;}}
                else if(Operator == "*="){if(id.VarValue is double x && identifier.VarValue is double y){x*=y; id.VarValue = x;}}
                else if(Operator == "/="){if(id.VarValue is double x && identifier.VarValue is double y){x/=y; id.VarValue = x;}}       
                else{throw new Exception("Invaid operation between types diferents");}
        }

        return right; 
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool IsAlreadyAsig(Scope? scope)
    {
        if(scope is null ) return false;
        if(scope.VarExpresions.Exists(x => x.ExpValue!.Equals(((ID)LeftSide!).ExpValue))) { return true; }

        return IsAlreadyAsig(scope.Parent);
    }
    

    private ID ? GetID(Scope ? scope)
    {
        if(scope is null){return null!;}

        if(scope.VarExpresions.Any(x => x.ExpValue!.Equals(((ID)LeftSide!).ExpValue))){

          return scope.VarExpresions.Find(x => x.ExpValue!.Equals(((ID)LeftSide!).ExpValue));
        }

        return GetID(scope!.Parent);
    }
}
