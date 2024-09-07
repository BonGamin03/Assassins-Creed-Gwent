namespace GWENT_DSL;

public class PostAction : Expr
{
    public EffectAsig ? EffectAsigment{get; set;}
    public Selector ? SelectAsigment{get; set;}
    public PostAction ? PostActionSon{get; set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public PostAction()
    {
        Type = TokenType.POST_ACTION;
        EffectAsigment = new();
    }
    
    public PostAction (EffectAsig effectAsigment, Selector selectAsigment, PostAction postActionAsig)
    {
        EffectAsigment = effectAsigment;
        SelectAsigment = selectAsigment;
        PostActionSon = postActionAsig;
        Type = TokenType.POST_ACTION;
    }

    public object Evaluate(Scope scope, Selector parent)
    {
        if (SelectAsigment is not null && SelectAsigment.Source!.Evaluate(scope!) is string source && source == "parent")
        {
            SelectAsigment.Source = parent.Source;
        }
        Selector selector;
        if (SelectAsigment is not null) selector = SelectAsigment;
        else selector = parent;
        return GetEffect(this,selector,new List<(EffectNode,Selector)>(),scope);
    }

    private object GetEffect(PostAction postAction, Selector selector, List<(EffectNode, Selector)> list, Scope scope)
    {
        var effect = Declarations.Effects.Find(x=> x.Name!.Evaluate(scope!).Equals(postAction.EffectAsigment!.Evaluate(scope!) ));
       if(effect is null) throw new Exception($"Effect {postAction.EffectAsigment!.Name.Evaluate(scope!)} does not exist");
       if(effect.Params is not null)
       {
          foreach (ID item in effect.Params.NameParam!)
            {
              string varName=item.ExpValue;
              if(EffectAsigment!.TheAmounts!.Exists(x=>varName==x.LeftSide.ExpValue))
              {
                AssigmentExpr param = EffectAsigment.TheAmounts.Find(x=>varName==x.LeftSide.ExpValue)!;
                scope.VarExpresions.Add(item);
                if(item.VarType is null)
                {
                    item.VarValue = param.RightSide;
                     
                } else if(item.VarType is not null)
                {   
                    
                    var right = param.RightSide.Evaluate(scope!);
                    if(item.VarType== TokenType.NUMBER && right is double) item.VarValue = right;
                    else if(item.VarType == TokenType.BOOL && right is bool) item.VarValue = right;
                    else if(item.VarType == TokenType.STRING && right is string) item.VarValue = right;
                    else throw new Exception($" Cannot convert from {right!.GetType()} to {item.VarType}");

                        
                     
                }
              } else throw new Exception($"Missing Param {varName}");
            }
       }
       list.Add((effect,selector)); 
       if(postAction.PostActionSon is not null) return GetEffect(postAction.PostActionSon,selector,list,scope);
       return list;
    }
    

    public override bool CheckSemantic(Scope scope)
    {
        if(EffectAsigment is null || EffectAsigment.Evaluate(scope!) is not string) throw new Exception("Invalid or Missing Name Expression");
        if(SelectAsigment is not null) SelectAsigment.CheckSemantic(scope!);
        if(PostActionSon is not null) PostActionSon.CheckSemantic(scope!);
        return true;  
    }
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override object? Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }
}
