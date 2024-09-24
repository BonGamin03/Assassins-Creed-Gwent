

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InExp : Expr
{
    public ID  Element{get; set;}
    public ID  Collection{get;set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public InExp ()
    {
        Type = TokenType.IN;
    }

    public InExp (ID element, ID collection)
    {
        Element    = element;
        Collection = collection;
        Type       = TokenType.IN;
    }

    public override object Evaluate(Scope scope)
    {
       throw new NotImplementedException();
    }

    public object Evaluate (Scope scope, int index)
    {
        IEnumerable<GameObject> cards = ReturnAnExpecElement(scope,Collection.ExpValue).VarValue is List<GameObject> x ? x : throw new Exception("Semantic error in for collection");

        if(!IsAlreadyAsig(scope)) 
        {
            scope.VarExpresions.Add(new ID(Element.ExpValue, TokenType.CARD_GAME_OBJECT));
        }

        cards = cards.Skip(index);
        IEnumerator<GameObject> enumerator = cards.GetEnumerator();

        while(enumerator.MoveNext())
        {
            ID target = ReturnElement(scope);
            target.VarValue = enumerator.Current;
            
            return true;
        }

        return false;
    }

    public override bool CheckSemantic(Scope scope)
    {
        if(Collection is not IEnumerable<CardDec>){throw new Exception("the identifier "+ Collection.ExpValue+" is not a collection of cards");}

        return true;
    }
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool IsAlreadyAsig(Scope? scope) // esta repetido el metodo en assigment expresions 
    {
        if(scope is null ) return false;
        if(scope.VarExpresions.Exists(x => x.ExpValue!.Equals(Element.ExpValue))) { return true; }

        return IsAlreadyAsig(scope.Parent);
    }

    public ID ReturnElement (Scope ? scope )
    {
        if(scope is null){return null!;}
        else if(scope.VarExpresions.Exists(x => x.ExpValue.Equals(Element.ExpValue))){

            return scope.VarExpresions.Find(x=> x.ExpValue.Equals(Element.ExpValue))!;
        }

        return ReturnElement(scope!.Parent);
    }

    public ID ReturnAnExpecElement(Scope ? scope, string nameVar )
    {
        if(scope is null){return null!;}
        else if(scope.VarExpresions.Exists(x => x.ExpValue.Equals(nameVar))){

            return scope.VarExpresions.Find(x=> x.ExpValue.Equals(nameVar))!;
        }

        return ReturnAnExpecElement(scope!.Parent,nameVar);
    }

    
}
