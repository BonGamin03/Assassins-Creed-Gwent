namespace GWENT_DSL;

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
    
    public ID LeftSide{get;set;}
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
        var right = RightSide.Evaluate(scope);

        if(!IsAlreadyAsig(scope)){
        
            if(Operator == "="){

                if(right is bool) scope.VarExpresions.Add(new ID(LeftSide.ExpValue!, TokenType.BOOL, right));
                else if(right is double) scope.VarExpresions.Add(new ID(LeftSide.ExpValue!, TokenType.NUMBER, right));
                else if(right is string) scope.VarExpresions.Add(new ID(LeftSide.ExpValue!, TokenType.STRING, right));
            }else{ throw new Exception(Operator+ " is not valid to no declarated variables");}        
        }else{
            ID identifier = null!;
                if(right is bool)   identifier = new ID(LeftSide.ExpValue!, TokenType.BOOL, right);
                if(right is double) identifier = new ID(LeftSide.ExpValue!, TokenType.NUMBER, right);
                if(right is string) identifier = new ID(LeftSide.ExpValue!, TokenType.STRING, right);
            
            ID id = GetID(scope)!;

            try
            {
                if(Operator == "="){id.VarValue = identifier.VarValue;}
                if(Operator == "+="){if(id.VarValue is double x && identifier.VarValue is double y){x+=y; id.VarValue = x;}}
                if(Operator == "-="){if(id.VarValue is double x && identifier.VarValue is double y){x-=y; id.VarValue = x;}}
                if(Operator == "*="){if(id.VarValue is double x && identifier.VarValue is double y){x*=y; id.VarValue = x;}}
                if(Operator == "/="){if(id.VarValue is double x && identifier.VarValue is double y){x/=y; id.VarValue = x;}}
            }
            catch (System.Exception)
            {
                
                throw new Exception("Invalid operation between ");
            }           

        }

        return null!;
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    public bool IsAlreadyAsig(Scope? scope)
    {
        if(scope is null ) return false;
        if(scope.VarExpresions.Exists(x => x.ExpValue!.Equals(LeftSide!.ExpValue))) { return true; }

        return IsAlreadyAsig(scope.Parent);
    }
    

    private ID ? GetID(Scope ? scope)
    {
        if(scope is null){return null!;}

        if(scope.VarExpresions.Any(x => x.ExpValue!.Equals(LeftSide!.ExpValue))){

          return scope.VarExpresions.Find(x => x.ExpValue!.Equals(LeftSide.ExpValue));
        }

        return GetID(scope!.Parent);
    }
}
