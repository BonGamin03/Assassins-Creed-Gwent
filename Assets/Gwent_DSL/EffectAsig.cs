

using System;
using System.Collections.Generic;

public class EffectAsig : Expr
{
    public AssigmentExpr Name{get; set;}
    public List<AssigmentExpr> TheAmounts{get; set;}
    public override TokenType? Type { get;protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public EffectAsig(AssigmentExpr name, List<AssigmentExpr> theAmounts)
    {
        Name = name;
        TheAmounts = theAmounts;
        Type = TokenType.EFFECT_CARD;
    }

    public EffectAsig()
    {
        TheAmounts = new();
        Type = TokenType.EFFECT_CARD;
    }

    public override object Evaluate(Scope scope) 
    {
          EffectNode effectAssigment = ProgrNode.Effects.Find(x => Name.RightSide.Evaluate(scope)!.ToString() == x.Name.RightSide.Evaluate(scope)!.ToString())!;

            if(effectAssigment is null) { throw new Exception("El efecto '"+Name.RightSide!.Evaluate(scope)!.ToString()+"' no se encuentra en el contexto actual"); }

            if (effectAssigment.Params is not null)
            {
                foreach (var exp in effectAssigment.Params.NameParam!)
                {
                    string nameVar = exp.ExpValue!;
                    if (TheAmounts.Exists(x => nameVar == ((ID)x.LeftSide).ExpValue))
                    {
                        AssigmentExpr x = TheAmounts!.Find(x => nameVar == ((ID)x.LeftSide).ExpValue)!;
                        ID variable=new ID(nameVar);
                        scope.VarExpresions.Add(variable);
                        var right=x.RightSide.Evaluate(scope);
                        if (exp.VarType is not null)
                        {   
                             
                            if (right is double && exp.VarType == TokenType.NUMBER_PARAM) { variable.VarValue = right; }
                            else if (right is bool && exp.VarType == TokenType.BOOL_PARAM) { variable.VarValue = right; }
                            else if (right is string && exp.VarType== TokenType.STRING_PARAM) { variable.VarValue = right; }
                            else { throw new Exception("Wrong imput in Effect card param type"); }

                            variable.VarType = VarTypeAssignation(right);
                             
                        }
                        else { variable.VarValue = right; variable.VarType = VarTypeAssignation(right); }

                    }else{ throw new Exception("Missing param in Effect "); }
                }
            }
            return effectAssigment;
    }

    public override bool CheckSemantic(Scope scope)
    {
        if(Name is null) throw new Exception("Effect card declaration must have a name");

        if (Name.RightSide.Evaluate(scope!) is not string) { throw new Exception("Name effect card declaration must have a name "); }
        return true;

        // aqui puede chequear semanticamente en caso que venga amount y no se llene al igual que 
        //si se le pasa un tipo de dato diferente al que lleva deberia explotar, pero se hara en el evaluate
        
    }

    public TokenType VarTypeAssignation(object right)
    {
        if(right is string){
            return TokenType.STRING;
        }else if(right is double){
            return TokenType.NUMBER;
        }else if(right is bool){
            return TokenType.BOOL;
        }

        throw new Exception("Invalid type for param effect");
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
