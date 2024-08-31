
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace GWENT_DSL;
public abstract class ASTNode{
    public abstract TokenType ? Type {get; protected set;}
}

public class ProgrNode : ASTNode{
    public List<EffectNode> ? EffectNodes {get; protected set;}
    public List<CardNode> ? CardNodes {get; protected set;}
    public override TokenType? Type {get;protected set;}

    public ProgrNode()
    {
        EffectNodes = [];
        CardNodes   = [];
    }

   
}
public class CardNode : ASTNode // I'll do it later 
{
    public AssigmentExpr? Name {get; set;}
    public AssigmentExpr? TypeCard {get;set;}
    public AssigmentExpr? Faction {get;set;}
    public AssigmentExpr? Power {get; set;}
    public List<Expr>? Range {get;set;}
    public OnActivation ?OnActivation {get;set;}
    public override TokenType? Type {get;protected set;}

    public CardNode (AssigmentExpr name, AssigmentExpr typeCard, AssigmentExpr faction, AssigmentExpr? power, List<Expr> range, OnActivation onActivation)
    {
        Name = name;
        TypeCard = typeCard;
        Faction = faction;
        Power = power;
        Range = range;
        OnActivation = onActivation;
        Type = TokenType.CARD;
    }

    public CardNode()
    {
        Type = TokenType.CARD;
    }
}

public class OnActivation : Expr
{
    public List<OnActivationStat>? OnActivationBody{get; set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public OnActivation(List<OnActivationStat> onActivationBody)
    {
        OnActivationBody = onActivationBody;
        Type = TokenType.ON_ACTIVATION;
    }

    public OnActivation()
    {
        OnActivationBody = new(); //cuidado
        Type = TokenType.ON_ACTIVATION;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class OnActivationStat : Expr
{
    public EffectAsig ? EffectAsigment{get; set;}
    public Selector ? SelectAsigment{get; set;}
    public PostAction ? PostActionAsig{get; set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public OnActivationStat()
    {
        Type = TokenType.ON_ACTIVATION_EFFECT;
    }
    
    public OnActivationStat(EffectAsig effectAsigment, Selector selectAsigment, PostAction postActionAsig)
    {
        EffectAsigment = effectAsigment;
        SelectAsigment = selectAsigment;
        PostActionAsig = postActionAsig;
        Type = TokenType.ON_ACTIVATION_EFFECT;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class PostAction : Expr
{
    public EffectAsig ? EffectAsigment{get; set;}
    public Selector ? SelectAsigment{get; set;}
    public PostAction ? PostActionAsig{get; set;}
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
        PostActionAsig = postActionAsig;
        Type = TokenType.POST_ACTION;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class Selector : Expr
{
    public AssigmentExpr ? Source{get; set;}
    public AssigmentExpr ? Single{get; set;}
    public LambdaExpr ? Predicate{get;set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public Selector(AssigmentExpr source, AssigmentExpr single, LambdaExpr predicate)
    {
        Source = source;
        Single = single;
        Predicate = predicate;
        Type = TokenType.SELECTOR;
    }

    public Selector()
    {
        Type = TokenType.SELECTOR;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class EffectAsig : Expr
{
    public AssigmentExpr? Name{get; set;}
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

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class EffectNode : ASTNode {
    public AssigmentExpr ? Name {get; set;}
    public ParamsExp ? Params{get; set;}
    public LambdaExpr ? Action{get; set;}
 
    public override TokenType? Type {get;protected set;}
}
public class LambdaExpr : Expr
{
    public List<ID> ? VarExpressions{get; protected set;}
    public Stat ? LambdaBody{get;private set; }
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
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class ParamsExp : Expr
{
    public ParamsExp()
    {
        Type = TokenType.PARAMS;
    }

    public List<ID> ? NameParam {get; set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class InExp : Expr
{
    public ID ? Element{get; set;}
    public ID ? Collection{get;set;}
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

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class ForExp : Expr
{
    public InExp ? InExpression {get; private set;}
    public Stat ?  ForBody {get; private set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public ForExp()
    {
        Type = TokenType.FOR;
        ForBody = new();
        InExpression = new();
    }

    public ForExp(InExp inExression, Stat forBody)
    {
        InExpression = inExression;
        ForBody = forBody;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }
    
    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

   

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class WhileExp : Expr
{
    public Expr? BoolPExpr {get;set;}
    public Stat? Body {get;set;}
    public override TokenType? Type {get; protected set;}
    public override Scope ?Scope {get; protected set; }

    public WhileExp()
    {
        Type = TokenType.WHILE;
        Body = new();
    }

    public WhileExp(Expr boolPExpr, Stat body)
    {
        BoolPExpr = boolPExpr;
        Body      = body;
    }

    public override object Evaluate(Scope scope)
    {
       Scope BodyScope = scope.CreateChild();
       if(BoolPExpr?.Evaluate(scope) is bool x)
       {
            while (x)
            {
                BoolPExpr.Evaluate(BodyScope);
            }
       }else{ throw new Exception(""); }

       return null!;
    }
    
    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
public abstract class Expr : ASTNode
{
    public abstract object ? Evaluate(Scope scope);
    public abstract void CheckSemantic();
    public abstract void GetScope(Scope scope);
    public abstract Scope ? Scope{get;protected set;} 
}

public class Scope
{
    public List<ID> VarExpresions {get; set;}
    public Scope ? Parent{get; set;}

    public Scope()
    {
        VarExpresions = [];
    }

    public Scope(params ID[] varExpressions)
    {
        VarExpresions = [.. varExpressions];
    }
    
    public Scope CreateChild()
    {
        Scope child = new();
        child.Parent = this;

        return child;
    }
}

public class Stat : Expr{

    public List<Expr> ? Exprs {get; private set;}
    public override TokenType? Type { get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public Stat()
    {
        Exprs = [];
        Type = TokenType.STATEMENT;
    }

    public override object Evaluate(Scope scope)
    {
        throw new NotImplementedException();
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class AssigmentExpr : Expr
{
    public AssigmentExpr(ID leftSide)
    {
        LeftSide = leftSide;
    }

    public AssigmentExpr(ID leftSide, string _operator, Expr rightSide)
    {
        LeftSide = leftSide;
        Operator = _operator;
        RightSide = rightSide;
        Type = TokenType.ASSIGMENT_EXP;
    }
    
    public ID LeftSide{get;set;}
    public string ?Operator{get;set;}
    public Expr RightSide{get; set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override void CheckSemantic()
    {
        if(Operator != "="){}

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
        if(scope.VarExpresions.Any(x => x.ExpValue!.Equals(LeftSide!.ExpValue))) { return true; }

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
public class BinaryExpr : Expr {
    public BinaryExpr(Expr left, Token token, Expr right)
    {
        Right = right;
        Left  = left;
        Type = token.TokenType;
    }

    public Expr  Right {get;protected set;} 
    public Expr  Left {get;protected set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override void CheckSemantic()
    {
       throw new  NotImplementedException();
    }

    public override object ? Evaluate(Scope scope)
    {
        switch (Type)
        {
            case TokenType.OR:
            return (bool)Left.Evaluate(scope)! || (bool)Right.Evaluate(scope)!;
            
            case TokenType.AND:
            return (bool)Left.Evaluate(scope)! && (bool)Right.Evaluate(scope)!;

            case TokenType.EQUAL_EQUAL:
            return Left.Evaluate(scope) == Right.Evaluate(scope);

            case TokenType.BANG_EQUAL:
            return Left.Evaluate(scope) != Right.Evaluate(scope);

            case TokenType.GREATHER:
            return (double)Left.Evaluate(scope)! > (double) Right.Evaluate(scope)!;

            case TokenType.GREATHER_EQUAL:
            return (double)Left.Evaluate(scope)! >= (double) Right.Evaluate(scope);

            case TokenType.LESS:
            return (double)Left.Evaluate(scope)! < (double) Right.Evaluate(scope)!;

            case TokenType.LESS_EQUAL:
            return (double)Left.Evaluate(scope)! <= (double) Right.Evaluate(scope)!;

            case TokenType.PLUS:
            return (double)Left.Evaluate(scope)! + (double) Right.Evaluate(scope)!;

            case TokenType.MINUS:
            return (double)Left.Evaluate(scope)! - (double) Right.Evaluate(scope)!;

            case TokenType.PRODUCT:
            return (double)Left.Evaluate(scope)! * (double) Right.Evaluate(scope)!;

            case TokenType.DIVITION:
            return (double)Left.Evaluate(scope)! / (double) Right.Evaluate(scope)!;

            case TokenType.POTENCE:
            return (long)Left.Evaluate(scope)!   ^ (long) Right.Evaluate(scope)!;

            default:
            throw new Exception();
        }
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class UnaryExpr : Expr {
    public Expr ? Content {get; protected set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public UnaryExpr(Token token, Expr content){
        Type = token.TokenType;
        Content = content;
    }

    public override object Evaluate(Scope scope)
    {
        switch(Type)
        {
            case TokenType.MINUS:
            return -(double)Content!.Evaluate(scope);

            case TokenType.BANG:
            return !(bool)Content!.Evaluate(scope);

            default:
            throw new Exception();
        }
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public abstract class AtomicExp : Expr {
}

public class ID : AtomicExp
{
    public string ?ExpValue{ get; protected set;}
    public TokenType ? VarType{get;set;}
    public object ? VarValue{get;set;}
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope{ get; protected set;}

    public ID(string? varName)
    {
         ExpValue = varName;
         Type = TokenType.ID;
    }

    public ID (string? varName, TokenType? varType, object? varValue)
    {
        ExpValue = varName;
        Type = TokenType.ID;
        VarType = varType;
        VarValue = varValue;

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

    public override void CheckSemantic() // hacer
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class Number : AtomicExp
{   
    public string ExpValue{get;protected set;}
    public Number(string value)
    {
        ExpValue = value;
        Type = TokenType.NUMBER;
    }
    public override TokenType? Type {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override object Evaluate(Scope scope )
    {
        return double.Parse(ExpValue);
    }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class PKBExpr : AtomicExp
{
    public PKBExpr( Expr  content, TokenType type)
    {
  
        Content = content;
        Type = type; // Esto devuelve una mierda tal vez utilizando recorrido in order puedo guardar en el value lo que hay en el parentesis
    }

    public PKBExpr(Expr  content){
        Content = content;
    }

    public Expr  Content{get; private set;}
    public override TokenType? Type {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override object? Evaluate(Scope scope)
    {
        return Content.Evaluate(scope);
    }

    public override void CheckSemantic()
    {
        throw new Exception();
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}


public class String : AtomicExp
{
    public String(string value)
    {
        Type = TokenType.STRING;
        ExpValue = value;
    }
    public override TokenType? Type {get;protected set;}
    public string ExpValue {get;protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        return ExpValue;
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}

public class BooLean : AtomicExp
{
    public BooLean(string value,TokenType type)
    {
        ExpValue = value;
        Type = type;
    }

     public override TokenType? Type {get; protected set;}
    public string ExpValue {get; protected set;}
    public override Scope? Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override void CheckSemantic()
    {
        throw new NotImplementedException();
    }

    public override object Evaluate(Scope scope)
    {
        return bool.Parse(ExpValue);
    }

    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }
}
public class Token{
    public Token(string _value, TokenType tokenType)
    {
        Value     = _value;
        TokenType = tokenType;
    }

    public string ? Value {get; private set;}
    public TokenType  TokenType {get; private set;}    
}

public interface IScope
{
    Scope Scope {get;set;}
}

public  enum TokenType
{
    END,
    AND,
    OR,
    EQUAL_EQUAL,
    BANG_EQUAL,
    GREATHER,
    GREATHER_EQUAL,
    LESS,
    LESS_EQUAL,
    MINUS,
    MINUS_MINUS,
    PLUS,
    PLUS_PLUS,
    PRODUCT,
    DIVITION,
    BANG,
    FALSE,
    TRUE,
    NUMBER,
    STRING,
    OPEN_PARENTHESIS,
    CLOSE_PARENTHESIS,
    ID,
    EFFECT,
    CARD,
    OPEN_KEY,
    CLOSE_KEY,
    NAME,
    PARAMS,
    ACTION,
    TWO_POINTS,
    COMMA,
    NUMBER_PARAM,
    STRING_PARAM,
    BOOL_PARAM,
    ASSIGMENT_EXP,
    LAMBDA_EXPR,
    FOR,
    WHILE,
    COMMA_POINT,
    EQUAL,
    PLUS_EQUAL,
    MINUS_EQUAL,
    IN,
    LOGICAL_OPEARATOR,
    ARITHMETICAL_OPERATORS,
    COMPARATION_OPERATOR,
    SIGNAL_OPERATOR,
    CONCATENATION,
    QUOTATION_MARKS,
    NoState,
    STATEMENT,
    TYPE,
    FACTION,
    POWER,
    RANGE,
    OPEN_BRACKET,
    CLOSE_BRACKET,
    ON_ACTIVATION,
    EFFECT_CARD,
    SELECTOR,
    ON_ACTIVATION_EFFECT,
    POST_ACTION,
    SOURCE,
    SINGLE,
    PREDICATE,
    BOOL,
    POTENCE,
    CONTEXT,
}
