namespace GWENT_DSL;
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
        LambdaBody = new();;
    }

    public override object Evaluate(Scope scope)
    {
        AddingScope(VarExpressions,scope);

        if(Type == TokenType.PREDICATE)
        {
            Predicate<CardData> predicate = (CardData) => EvaluatPredicat(CardData,scope);
            return predicate;
        }else{

            Action<List<CardData>> action = (targets) => EvaluateAction(targets,scope);
            return action;
        }
    }

    private void AddingScope(List<ID> varExpressions, Scope scope)
    {
        foreach (var item in varExpressions)
        {
            scope.VarExpresions.Add(item);
        }
    }

    private void EvaluateAction(List<CardData> targets, Scope scope)
    {
        VarExpressions[0].VarValue = targets;
        LambdaBody.Evaluate(scope);
    }

    private bool EvaluatPredicat(CardData cardData, Scope scope)
    {
        VarExpressions[0].VarValue = cardData;
        return (bool)LambdaBody.Evaluate(scope);
    }

    public override bool CheckSemantic(Scope scope)
    {
        if(Type  == TokenType.PREDICATE)
        {
            if(VarExpressions!.Count != 1)
            {
                if(LambdaBody.Exprs.First().Evaluate(scope) is not bool ) // se puede poner en el metodo de arriba 
                    throw new Exception("Invalid argument in Predicate body");
            }else{throw new Exception("Predicate only take one element");}
        }else{
            if(VarExpressions!.Count != 2)
            {
                LambdaBody.CheckSemantic(scope);
            }else{throw new Exception("Action only take two arguments");}
        }

        foreach (var item in VarExpressions)
        {
            if(IsAlreadyDeclarated(scope, item.ExpValue)){throw new Exception("Already exist a variable with"+item.ExpValue+"name");}
        }

        return true;
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