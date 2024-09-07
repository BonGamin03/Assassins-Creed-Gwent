namespace GWENT_DSL;

public class EffectAsig : Expr
{
    public AssigmentExpr Name{get; set;}
    public List<AssigmentExpr> ?TheAmounts{get; set;}
    public override TokenType? Type { get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

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

    public override object Evaluate(Scope scope) //revisar 
    {
          EffectNode effectAssigment = Declarations.Effects.Find(x => Name.RightSide.Evaluate(scope)!.ToString() == x.Name.RightSide.Evaluate(scope)!.ToString())!;

            if(effectAssigment is null) { throw new Exception("El efecto '"+Name.RightSide!.Evaluate(scope)!.ToString()+"' no se encuentra en el contexto actual"); }

            if (effectAssigment.Params is not null)
            {
                foreach (var exp in effectAssigment.Params.NameParam!)
                {
                    string nameVar = exp.ExpValue!;
                    if (TheAmounts!.Exists(x => nameVar == x.LeftSide.ExpValue))
                    {
                        AssigmentExpr x = TheAmounts!.Find(x => nameVar == x.LeftSide.ExpValue)!;
                        if (exp.VarType is not null)
                        {
                            if (x.RightSide!.Evaluate(scope) is double && exp.VarType == TokenType.NUMBER) { exp.VarValue = x.RightSide; }
                            else if (x.RightSide!.Evaluate(scope) is bool && exp.VarType == TokenType.BOOL) { exp.VarValue = x.RightSide; }
                            else if (x.RightSide!.Evaluate(scope) is string && exp.VarType== TokenType.STRING) { exp.VarValue = x.RightSide; }
                            else { throw new Exception("Wrong imput in Effect card param type"); }
                        }
                        else { exp.VarValue = x.RightSide; }

                    }else{
                        throw new Exception("Missing param in Effect ");
                    }
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

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
