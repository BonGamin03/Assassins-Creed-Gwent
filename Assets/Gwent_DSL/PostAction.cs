

using System;
using System.Collections.Generic;

public class PostAction : Expr
{
    public EffectAsig  EffectAsigment{get; set;}
    public Selector  SelectAsigment{get; set;}
    public PostAction  PostActionSon{get; set;}
    public override TokenType? Type {get; protected set;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

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
        return GetEffect(this,selector,new Dictionary<EffectNode,Selector>(),scope);
    }

    private object GetEffect(PostAction postAction, Selector selector, Dictionary<EffectNode, Selector> list, Scope scope)
    {
        var effect = ProgrNode.Effects.Find(x=> x.Name!.Evaluate(scope!).Equals(postAction.EffectAsigment!.Name.Evaluate(scope!) ));
       if(effect is null) throw new Exception($"Effect {postAction.EffectAsigment!.Name.Evaluate(scope!)} does not exist");
       if(effect.Params is not null)
       {
          foreach (ID item in effect.Params.NameParam!)
            {
              string varName=item.ExpValue;
              if(postAction.EffectAsigment!.TheAmounts!.Exists(x => varName == ((ID)x.LeftSide).ExpValue))
              {
                AssigmentExpr param = postAction.EffectAsigment.TheAmounts.Find(x => varName == ((ID)x.LeftSide).ExpValue)!;
                ID variable=new ID(varName);
                
                scope.VarExpresions.Add(variable);
                var right=param.RightSide.Evaluate(scope);
                if(item.VarType is null)
                {
                    variable.VarValue =  right;
                     
                } else if(item.VarType is not null)
                {   
                    
                     
                    if(item.VarType== TokenType.NUMBER_PARAM && right is double) variable.VarValue = right;
                    else if(item.VarType == TokenType.BOOL_PARAM && right is bool) variable.VarValue = right;
                    else if(item.VarType == TokenType.STRING_PARAM && right is string) variable.VarValue = right;
                    else throw new Exception($" Cannot convert from {right!.GetType()} to {variable.VarType}");

                        
                     
                }
              } else throw new Exception($"Missing Param {varName}");
            }
       }
       list.Add(effect,selector); 
       if(postAction.PostActionSon is not null) return GetEffect(postAction.PostActionSon,selector,list,scope);
       return list;
    }
    

    public override bool CheckSemantic(Scope scope)
    {
        if(EffectAsigment is null || !EffectAsigment.CheckSemantic(scope)) throw new Exception("Invalid or Missing Name Expression");
        if(SelectAsigment is not null) SelectAsigment.CheckSemantic(scope!);
        if(PostActionSon is not null) PostActionSon.CheckSemantic(scope!);
        return true;  
    }
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }
}
