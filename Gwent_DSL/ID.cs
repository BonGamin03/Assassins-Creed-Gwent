namespace GWENT_DSL;

public class ID : AtomicExp
{
    public string ExpValue{ get; protected set;}
    public TokenType ? VarType{get;set;}
    public object ? VarValue{get;set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope{ get; protected set;}

    public ID(string varName)
    {
         ExpValue = varName;
         Type = TokenType.ID;
    }

    public ID (string varName, TokenType? varType, object? varValue)
    {
        ExpValue = varName;
        Type = TokenType.ID;
        VarType = varType;
        VarValue = varValue;

    }

  public ID (string varName, TokenType varType)
  {
    ExpValue = varName;
    VarType = varType;
  }

    public override object? Evaluate(Scope scope)
    {
        if(ValueID(scope,out object? varExp))
        {
            return varExp;
        }else{
            throw new Exception(ExpValue + " don't exist in the current context");
        }
    }

    private bool ValueID(Scope scope, out object? varExp)
    {
        if(scope is null){ varExp = null ; return false;}
        if(scope.VarExpresions.Any( varExp => varExp.ExpValue == ExpValue)){
            
            foreach (var item in scope.VarExpresions)
            {
                if(item.ExpValue == ExpValue && item.Type == TokenType.CONTEXT)
                {
                    varExp = item.ExpValue!;
                    return true;
                }else{
                    varExp = TokenType.CONTEXT; return true;
                }
            }
        }

        return ValueID(scope.Parent!,out varExp);
    }

    public override bool CheckSemantic(Scope scope) // hacer 
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
