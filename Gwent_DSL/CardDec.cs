using System.Linq.Expressions;

namespace GWENT_DSL;

public class CardDec : Expr // I'll do it later 
{
    public AssigmentExpr Name {get; set;}
    public AssigmentExpr TypeCard {get;set;}
    public AssigmentExpr Faction {get;set;}
    public AssigmentExpr? Power {get; set;}
    public List<Expr> Range {get;set;}
    public OnActivation ?OnActivation {get;set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }
    private static string[] theTypeOfCards = {"Oro", "Plata", "Lider", "Clima", "Aumento", "Despeje", "Señuelo"};  

    public CardDec (AssigmentExpr name, AssigmentExpr typeCard, AssigmentExpr faction, AssigmentExpr? power, List<Expr> range, OnActivation onActivation)
    {
        Name = name;
        TypeCard = typeCard;
        Faction = faction;
        Power = power;
        Range = range;
        OnActivation = onActivation;
        Type = TokenType.CARD_DEC;
    }

    public CardDec()
    { 
        Type = TokenType.CARD_DEC;
    }

    public override object? Evaluate(Scope scope)
    {
        CheckSemantic(scope);

        CardData card = new(
             (string)Name.RightSide.Evaluate(scope)!
            ,(string)TypeCard.RightSide.Evaluate(scope)!
            ,(string)Faction.RightSide.Evaluate(scope)!
            ,(double)Power!.RightSide.Evaluate(scope)!
            ,GetRange(Range,scope));

        return card;
    }

    public override bool CheckSemantic(Scope scope)
    {
      
        if(Name is null || Name.Evaluate(scope!) is not string) throw new Exception("Name does not exist");
        if(TypeCard is null || !theTypeOfCards.Contains(TypeCard.Evaluate(scope!))) throw new Exception("Type does not exist");

        if(TypeCard.Evaluate(scope!) is string type)
        { 
            
            if(type== "Oro"|| type=="Plata" || type == "Aumento")
            {
               if(Faction is null || Faction.Evaluate(scope!) is not string) throw new Exception("Faction does not exist");
               if(Power is null || Power.Evaluate(scope!) is not double x || x<=0) throw new Exception("Power does not exist");
               if(Range.Count>3 || !Range.Any() ) throw new Exception("Params Overload or Missing");
               GetRange(Range,scope);
            }
            else if(type=="Clima")
            {
                if(Power is not null|| Power!.Evaluate(scope!) is not double x|| x!=0) throw new Exception("This Card do not have");
                if(Range.Count>3 || Range.Count == 0 ) throw new Exception("Params Overload or Missing");
                GetRange(Range,scope);
            }
            else if(type=="Lider")
            {
              if(Power is null || Power.Evaluate(scope!) is not double x || x<=0) throw new Exception("Power does not exist");
              if(Range.Any()) throw new Exception("Lider do not have Range");

            } else throw new Exception($"Invalid Card Type {type}");
        } 
          OnActivation.CheckSemantic(scope!);
          return true;
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    private bool[] GetRange(List<Expr> expressions, Scope scope)
        {
            bool[] result = new bool[3];
            foreach(Expr expression in expressions)
            {
                string x =(expression.Evaluate(scope!) is string y ) ? y: throw new Exception("");

                if (x == "Melee" ) 
                {
                    if (!result[0]) { result[0] = true; }
                    else { throw new Exception("Melee is already declared"); }
                }
                else if (x == "Ranged")
                {
                    if (!result[1]) { result[1] = true; }
                    else { throw new Exception("Ranged is already declared "); }
                }
                else if (x == "Siege")
                {
                    if (!result[2]) { result[2] = true; }
                    else { throw new Exception("Siege is already declared"); }
                }
                else { throw new Exception(" 'Melee', 'Ranged' or 'Siege' expected "); }
            }

            return result;
        }
}