

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Parser
{
    //Me falta poneer en el parser que las lis de Amount y esas tallas deben tener al menos un elemento 
    static void Main(){
        // List<Token> tokens = Lexer.Lexing("effect{ Name: \"Damage\", Params:{Amount : Number} }");
        //Parser parser = new Parser(new List<Token>(){new Token("effect",TokenType.EFFECT), new Token("{",TokenType.OPEN_KEY),new Token("Name",TokenType.NAME),new Token(":",TokenType.TWO_POINTS),new Token("Damage",TokenType.STRING),new Token(",",TokenType.COMMA),new Token("Params",TokenType.PARAMS),new Token(":",TokenType.TWO_POINTS),new Token("{",TokenType.OPEN_KEY),new Token("Amount",TokenType.ID),new Token(":",TokenType.TWO_POINTS),new Token("Number",TokenType.NUMBER_PARAM),new Token("}",TokenType.CLOSE_KEY),new Token(",",TokenType.COMMA),new Token("Action",TokenType.ACTION),new Token(":",TokenType.TWO_POINTS),new Token("(",TokenType.OPEN_PARENTHESIS),new Token("targets",TokenType.ID),new Token(",",TokenType.COMMA),new Token("context",TokenType.ID),new Token(")",TokenType.CLOSE_PARENTHESIS),new Token("=>",TokenType.LAMBDA_EXPR),new Token("{",TokenType.OPEN_KEY),new Token("for",TokenType.FOR),new Token("target",TokenType.ID),new Token("in",TokenType.IN),new Token("targets",TokenType.ID),new Token("{",TokenType.OPEN_KEY),new Token("i",TokenType.ID),new Token("=",TokenType.EQUAL),new Token("0",TokenType.NUMBER),new Token(";",TokenType.COMMA_POINT),new Token("while",TokenType.WHILE),new Token("(",TokenType.OPEN_PARENTHESIS),new Token("5",TokenType.NUMBER),new Token(">",TokenType.GREATHER),new Token("Amount",TokenType.ID),new Token(")",TokenType.CLOSE_PARENTHESIS),new Token("target",TokenType.ID),new Token("-=",TokenType.MINUS_EQUAL),new Token("1",TokenType.NUMBER),new Token(";",TokenType.COMMA_POINT),new Token("}",TokenType.CLOSE_KEY),new Token(";",TokenType.COMMA_POINT),new Token("}",TokenType.CLOSE_KEY),new Token("/0",TokenType.END)});
        // Parser parser = new Parser(new List<Token>(){new Token("card",TokenType.CARD), new Token("{",TokenType.OPEN_KEY), new Token("Type",TokenType.TYPE), new Token(":",TokenType.TWO_POINTS), new Token("Oro",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Name",TokenType.NAME), new Token(":",TokenType.TWO_POINTS), new Token("Beluga",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Faction",TokenType.FACTION), new Token(":",TokenType.TWO_POINTS), new Token("Assassins",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Power",TokenType.POWER), new Token(":",TokenType.TWO_POINTS), new Token("10",TokenType.NUMBER), new Token(",",TokenType.COMMA), new Token("Range",TokenType.RANGE), new Token(":",TokenType.TWO_POINTS), new Token("[",TokenType.OPEN_BRACKET), new Token("Melee",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Ranged",TokenType.STRING), new Token("]",TokenType.CLOSE_BRACKET), new Token(",",TokenType.COMMA), new Token("OnActivation",TokenType.ON_ACTIVATION), new Token(":",TokenType.TWO_POINTS), new Token("[",TokenType.OPEN_BRACKET), new Token("{",TokenType.OPEN_KEY), new Token("Effect",TokenType.EFFECT_CARD), new Token(":",TokenType.TWO_POINTS), new Token("{",TokenType.OPEN_KEY), new Token("Name",TokenType.NAME), new Token(":",TokenType.TWO_POINTS), new Token("Damage",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Amount",TokenType.ID), new Token(":",TokenType.TWO_POINTS), new Token("5",TokenType.NUMBER), new Token(",",TokenType.COMMA), new Token("}",TokenType.CLOSE_KEY), new Token(",",TokenType.COMMA), new Token("Selector",TokenType.SELECTOR), new Token(":",TokenType.TWO_POINTS), new Token("{",TokenType.OPEN_KEY), new Token("Source",TokenType.SOURCE), new Token(":",TokenType.TWO_POINTS), new Token("board",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Single",TokenType.SINGLE), new Token(":",TokenType.TWO_POINTS), new Token("false",TokenType.FALSE), new Token(",",TokenType.COMMA), new Token("Predicate",TokenType.PREDICATE), new Token(":",TokenType.TWO_POINTS), new Token("(",TokenType.OPEN_PARENTHESIS), new Token("unit",TokenType.ID), new Token(")",TokenType.CLOSE_PARENTHESIS), new Token("=>",TokenType.LAMBDA_EXPR), new Token("unit",TokenType.ID), new Token("==",TokenType.EQUAL_EQUAL), new Token("Assassins",TokenType.STRING), new Token("}",TokenType.CLOSE_KEY), new Token(",",TokenType.COMMA), new Token("PostAction",TokenType.POST_ACTION), new Token(":",TokenType.TWO_POINTS), new Token("{",TokenType.OPEN_KEY), new Token("Type",TokenType.TYPE), new Token(":",TokenType.TWO_POINTS), new Token("Return to deck",TokenType.STRING), new Token(",",TokenType.COMMA),new Token("Selector",TokenType.SELECTOR), new Token(":",TokenType.TWO_POINTS), new Token("{",TokenType.OPEN_KEY),new Token("Source",TokenType.SOURCE), new Token(":",TokenType.TWO_POINTS), new Token("board",TokenType.STRING), new Token(",",TokenType.COMMA), new Token("Single",TokenType.SINGLE), new Token(":",TokenType.TWO_POINTS), new Token("false",TokenType.FALSE), new Token(",",TokenType.COMMA), new Token("Predicate",TokenType.PREDICATE), new Token(":",TokenType.TWO_POINTS), new Token("(",TokenType.OPEN_PARENTHESIS), new Token("unit",TokenType.ID), new Token(")",TokenType.CLOSE_PARENTHESIS), new Token("=>",TokenType.LAMBDA_EXPR), new Token("unit",TokenType.ID), new Token("==",TokenType.EQUAL_EQUAL), new Token("Assassins",TokenType.STRING), new Token("}",TokenType.CLOSE_KEY), new Token("}",TokenType.CLOSE_KEY), new Token("}",TokenType.CLOSE_KEY), new Token(",",TokenType.COMMA), new Token("{",TokenType.OPEN_KEY), new Token("Effect",TokenType.EFFECT_CARD), new Token(":",TokenType.TWO_POINTS), new Token("Draw",TokenType.STRING), new Token("}",TokenType.CLOSE_KEY), new Token("]",TokenType.CLOSE_BRACKET), new Token("}",TokenType.CLOSE_KEY)});
        //ProgrNode ? Tree = parser.Parsing();

           //Expr expr = new BinaryExpr(new Number("5"),new Token("+",TokenType.PLUS),new Number("3"));
           //var Var= expr.Evaluate(null!);
           //System.Console.WriteLine(Var);
    }

    private List<Token>  Tokens {get; set;}
    private int Current;
    List<TokenType> Properties{get;set;}
    List<TokenType> Functions{get;set;}

    public Parser(string input)
    {
        Lexer lexer = new (input);
        Token token;
        Tokens = new();
        while (lexer.position < input.Length)
        {
            token = lexer.GetTokens();
            if(token.TokenType != TokenType.WHITE_SPACE)
            Tokens.Add(token);
        }


        Properties=new List<TokenType>{TokenType.DECK, TokenType.FIELD,
            TokenType.GRAVEYARD, TokenType.HAND, TokenType.NAME, TokenType.OWNER
            , TokenType.POWER, TokenType.TRIGGER_PLAYER, TokenType.TYPE, TokenType.BOARD,
            TokenType.FACTION, TokenType.RANGE};

         Functions=new List<TokenType>{ TokenType.DECK_OF_PLAYER, TokenType.FIELD_OF_PLAYER,
            TokenType.FIND, TokenType.GRAVEYARD_OF_PLAYER, TokenType.POP, TokenType.PUSH,
            TokenType.REMOVE, TokenType.SEND_BOTTOM, TokenType.SHUFFLE, TokenType.HAND_OF_PLAYER,
            TokenType.ADD};
    }

    
    internal void  Parsing()
    {
        while(!Match(TokenType.END)){

            if(Match(TokenType.EFFECT)){

                EffectNode effect = ParseEffect();
                ProgrNode.Effects?.Add(effect);

            }else if(Match(TokenType.CARD_DEC)){

                CardDec card = ParseCard();
                ProgrNode.Cards.Add(card);
                
            }else
            {
                throw new Exception("You are not defining a shit");
            }
        }
    }

    private CardDec ParseCard()
    {
        CardDec card = new ();
       
       if(Match(TokenType.OPEN_KEY)){

            card = MachineCard(card);
            CheckPKB(TokenType.CLOSE_KEY, "} EXPECTED");
       }else{
            throw new Exception("Open key is missing");
       }

       return card;
    }

    private EffectNode ParseEffect()
    {
       EffectNode  effect = new ();
       
       if(Match(TokenType.OPEN_KEY)){

            effect = MachineEffect(effect);
            CheckPKB(TokenType.CLOSE_KEY, "} EXPECTED"); // Se explota con esto 
       }else{
            throw new Exception("Open key is missing");
       }

       return effect;
    }

    private EffectNode MachineEffect(EffectNode effect)
    {
            if(Match(TokenType.NAME)){
                if(effect.Name is null){

                    effect.Name = ParseAssigmentTwoPoints();
                    CheckComma(effect);
                }else{

                    throw new Exception("Name of the effect is already declarated");
                }
            }else if(Match(TokenType.PARAMS)){
                if(effect.Params is null){

                    effect.Params = ParseParams();
                    CheckComma(effect);
                }else{

                    throw new Exception("Param of th effect is already declarated");
                }
             }else if(Match(TokenType.ACTION)){
                if(effect.Action is null){

                    effect.Action = ParsingLambdaExpr(Previous().TokenType);
                    CheckComma(effect);
                } // poner else exeption
                
             }else if(Tokens[Current].TokenType !=TokenType.END){
                throw new Exception("Unexpected token");
            }

            return effect;
    }

    private AssigmentExpr ParseAssigmentTwoPoints()
    {
        AssigmentExpr assigantion = new(new ID(Previous().Value!));

        if(Match(TokenType.TWO_POINTS))
        {
            assigantion.Operator = Previous().Value;
            assigantion.RightSide = Expretion();
            
        }else{
            throw new Exception(" Invalid token we expect a :");
        }
        return assigantion;
    }
    
    private ParamsExp ParseParams()
    {
        ParamsExp ParamsEffect = new();

        if(Match(TokenType.TWO_POINTS))
        {
            if(Match(TokenType.OPEN_KEY)){

                ParamsEffect.NameParam = AmountList();

            }
        }else{
            throw new Exception(" Invalid token we expect a :");
        }
        return ParamsEffect;
    }

    private LambdaExpr ParsingLambdaExpr(TokenType tokenType)
    {
        LambdaExpr lambda = new(tokenType);
        AddElemToLambdaParam(tokenType,lambda);

        if(Match(TokenType.LAMBDA_EXPR)){

            ParsingStatements(tokenType,lambda);
        }else{
            throw new Exception("=> EXPECTED");
        }

        return lambda;
    }
    private void AddElemToLambdaParam(TokenType tokenType, LambdaExpr lambda)
    {
        if(Match(TokenType.TWO_POINTS)){

            if(Match(TokenType.OPEN_PARENTHESIS)){

                while(!Match(TokenType.CLOSE_PARENTHESIS))
                {
                    if(Match(TokenType.ID)){
                        lambda.VarExpressions!.Add(new ID(Previous().Value!));
                    
                        if(Match(TokenType.COMMA)){
                            if(tokenType is TokenType.ACTION)
                                continue;

                            throw new Exception("Predicate param doesn't endure two arguments");
                        }else if(Peek().TokenType is not TokenType.CLOSE_PARENTHESIS){
                            throw new Exception("Invalid input");
                        }
                    }else{
                        throw new Exception("We expected an identifier");
                    }
                }
                
            }else{
                throw new Exception("{ EXPECTED");
            }
        }else{
            throw new Exception("Invalid token we expect a :");
        }
    }
    
    private void ParsingStatements(TokenType tokenType, LambdaExpr lambda)
    {
        var body = lambda.LambdaBody?.Exprs;
       if(tokenType is TokenType.ACTION){

            if(Match(TokenType.OPEN_KEY)){

                while (!Match(TokenType.CLOSE_KEY) && Tokens[Current].TokenType != TokenType.END)
                {
                     body?.Enqueue(CheckBodyStat());
                    //  CheckPKB(TokenType.CLOSE_KEY,"}");
                }
                

            }else{
                throw new Exception("{ EXPECTED");
            }
       }else{
            body?.Enqueue(Expretion());
       }
    }

    private Expr CheckBodyStat()
    {
       switch(Peek().TokenType)
       {
            case TokenType.FOR:
             Current++;
             return ParsingFor();
            
            case TokenType.WHILE:
             Current++;
             return ParsingWhile();
            
            case TokenType.ID:
             //Current++;
             return ParsingAsigAndDoc();
            
            default:
                throw new Exception("Invalid Token in the currrent context");
       }
    }

    private Expr ParsingWhile()
    {
       WhileExp whileExp = new();

       if(Match(TokenType.OPEN_PARENTHESIS)){

        whileExp.BoolPExpr = Expretion();
        CheckPKB(TokenType.CLOSE_PARENTHESIS,") EXPECTED");

        if(Match(TokenType.OPEN_KEY)){

            while(!Match(TokenType.CLOSE_KEY) && Tokens[Current].TokenType != TokenType.END)
            {
                whileExp.Body?.Exprs?.Enqueue(CheckBodyStat());
            }
            
            if(Match(TokenType.CLOSE_KEY)){
                CheckPKB(TokenType.COMMA_POINT,"; EXPECTED");
            }else{
                throw new Exception("} EXPECTED");
            }
        }else{

            whileExp.Body?.Exprs?.Enqueue(CheckBodyStat());
        }
       }else{
         throw new Exception("( EXPECTED");
       }

       return whileExp;
    }

    private Expr ParsingAsigAndDoc()
    {
        Expr expr = Or();

        if(Match(TokenType.EQUAL) || Match(TokenType.PLUS_EQUAL) || Match(TokenType.MINUS_EQUAL) || Match(TokenType.DIVITION_EQUAL) || Match(TokenType.PRODUCT_EQUAL)){

            expr = new AssigmentExpr(expr,Previous().Value!,Expretion());
        }

        CheckPKB(TokenType.COMMA_POINT,"; EXPECTED");

        return expr;
    }

    private Expr ParsingFor()
    {
        ForExp forExp = new();

        if(Match(TokenType.ID)){

            forExp.InExpression!.Element = new ID(Previous().Value!);
            if(Match(TokenType.IN))
            {
                if(Match(TokenType.ID)){

                    forExp.InExpression!.Collection = new ID(Previous().Value!);
                    if(Match(TokenType.OPEN_KEY)){

                        while(!Match(TokenType.CLOSE_KEY) && Tokens[Current].TokenType != TokenType.END)
                        {
                            forExp.ForBody?.Exprs?.Enqueue(CheckBodyStat());
                        }

                        CheckPKB(TokenType.COMMA_POINT,"; EXPECTED");
                    }else{
                        throw new Exception("{ EXPECTED");
                    }
                }else{
                    throw new Exception("Unexpected token");
                }
            }else{
                throw new Exception("You miss in Keyword");
            }
        }else{
            throw new Exception("Wrong token we expect a identifier token");
        }

        return forExp;
    }

    private List<ID> AmountList()
    {
        List<ID> theAmounts = new();

        while(!Match(TokenType.CLOSE_KEY))
        {
            theAmounts.Add(Amount());
        }
       if(theAmounts.Count()>0){

        return theAmounts;
       }else{

            throw new Exception("Invalid Augument");
        }
    }

    private ID Amount()
    {
        ID theAmount;
        
        if(Match(TokenType.ID)){

            theAmount = new(Previous().Value!);

            if(Match(TokenType.TWO_POINTS)){

                if(Match(TokenType.STRING_PARAM, TokenType.NUMBER_PARAM, TokenType.BOOL_PARAM)){
                    theAmount.VarType = Previous().TokenType;
                }else if(Match(TokenType.COMMA)){
                }else{
                    throw new Exception("You are not sending the identifier type");
                }
            }else if(Match(TokenType.COMMA)){}

        }else{
            throw new Exception("You are not sending an identifier");
        }

        return theAmount;
    }

    private Expr Expretion(){

        return Or();
    }

    private Expr Or (){

        Expr expr = And();
        
        while(Match(TokenType.OR)){

            
            Token token = Previous();
            Expr right = And();
            expr = new BinaryExpr(expr,token,right);
        }

        return expr;
    }

    //Card Declaration

     private CardDec MachineCard(CardDec card)
    {
            if(Match(TokenType.NAME)){
                if(card.Name is null){

                    card.Name = ParseAssigmentTwoPoints();
                    CheckComma(card);
                }else{

                    throw new Exception("Name of the card is already declarated");
                }
            }else if(Match(TokenType.TYPE)){
                if(card.TypeCard is null){

                    card.TypeCard = ParseAssigmentTwoPoints();
                    CheckComma(card);
                }else{

                    throw new Exception("Type of the card is already declarated");
                }
             
            }else if(Match(TokenType.FACTION)){
                if(card.Faction is null){

                    card.Faction = ParseAssigmentTwoPoints();
                    CheckComma(card);
                }else{

                    throw new Exception("Faction of the card is already declarated");
                }
              }else if(Match(TokenType.POWER)){
                if(card.Power is null){

                    card.Power = ParseAssigmentTwoPoints();
                    CheckComma(card);
                }else{

                    throw new Exception("Power of the card is already declarated");
                }
              }else if(Match(TokenType.RANGE)){
                if(card.Range is null){

                    card.Range = ParseRangeCard();
                    CheckComma(card);
                }else{

                    throw new Exception("Power of the card is already declarated");
                }
             }else if(Match(TokenType.ON_ACTIVATION)){
                if(card.OnActivation is null){

                    card.OnActivation = ParseOnActivation();
                    CheckComma(card);
                }else{

                    throw new Exception("Power of the card is already declarated");
                }
             }else if(Tokens[Current].TokenType !=TokenType.END){
                throw new Exception("Unexpected token");
            }

            return card;
    }

    private OnActivation ParseOnActivation()
    {   
         OnActivation onActivation = new();
        if(Match(TokenType.TWO_POINTS)){

            if(Match(TokenType.OPEN_BRACKET)){

                onActivation.OnActivationBody = EffectListCard(onActivation.OnActivationBody!);
                CheckPKB(TokenType.CLOSE_BRACKET,"] EXPECTED"); //posible problema
            }else{

                throw new Exception("[ EXPECTED");
            }
        }else{  

            throw new Exception(": EXPECTED");
        }
        
        return onActivation;
    }

    private List<OnActivationStat> EffectListCard(List<OnActivationStat> effectList)
    {
            effectList.Add(TheEffect(effectList));
        
        return effectList;
    }

    private OnActivationStat TheEffect(List<OnActivationStat> effectList)
    {
        OnActivationStat PrimaryEffect = new();
        
        if(Match(TokenType.OPEN_KEY)){

            TheEffectExt(PrimaryEffect);
            CheckPKB(TokenType.CLOSE_KEY,"} EXPECTED");
            CheckComma(effectList);

        }else{
            throw new Exception("{ EXPECTED");
        }
        
        return PrimaryEffect;
    }

    private void TheEffectExt(OnActivationStat onActivationStat)
    {
         
            if(Match(TokenType.EFFECT_CARD)){

                onActivationStat.EffectAsigment = ParsingEffectAsig();
                CheckComma(onActivationStat);

            }else if(Match(TokenType.SELECTOR)){

                onActivationStat.SelectAsigment = ParsingSelectAsig();
                CheckComma(onActivationStat);

            }else if(Match(TokenType.POST_ACTION)){

                onActivationStat.PostActionAsig = ParsingPosAction();
                CheckComma(onActivationStat);

            }
    }

    private PostAction ParsingPosAction()
    {
        PostAction postAction = new();

        if(Match(TokenType.TWO_POINTS)){

            if(Match(TokenType.OPEN_KEY)){

                ThePosAction(postAction);
                CheckPKB(TokenType.CLOSE_KEY,"} EXPECTED"); //problematica
            }else{
                throw new Exception("{ EXPECTED");
            }
        }else{
            throw new Exception(": EXPECTED");
        }
        return postAction;
    }

    private void ThePosAction(PostAction postAction)
    {
       
        if(Match(TokenType.TYPE) || Match(TokenType.ID)){

            ParsingPosActionParams(postAction.EffectAsigment!);
            CheckComma(postAction);

        }else if(Match(TokenType.SELECTOR)){

            postAction.SelectAsigment = ParsingSelectAsig();
            CheckComma(postAction);

        }else if(Match(TokenType.POST_ACTION)){

            postAction.PostActionSon = ParsingPosAction();
            CheckComma(postAction);

        }
           
    }

    private void ParsingPosActionParams(EffectAsig effectAsig)
    {
        var previous = Previous();

        if(Match(TokenType.TWO_POINTS)){
            
            if(previous.TokenType is TokenType.TYPE){
                effectAsig.Name = new AssigmentExpr(new ID(previous.Value!),Previous().Value!,Expretion());
            }else{
                effectAsig.TheAmounts!.Add(new AssigmentExpr(new ID(previous.Value!),Previous().Value!,Expretion()));
            }

        }else{
            throw new Exception(": EXPECTED");
        }
    }

    private EffectAsig ParsingEffectAsig()
    {
        
        EffectAsig effectAsig = new();

        if(Match(TokenType.TWO_POINTS)){
            
            if(Match(TokenType.OPEN_KEY)){
                
                while (!Match(TokenType.CLOSE_KEY))
                {
                    if(Match(TokenType.NAME)){  

                        if(effectAsig.Name is null )
                            effectAsig.Name = ParseAssigmentTwoPoints();
                        else
                            throw new Exception("Name declaration already exist");

                        if(!Check(TokenType.CLOSE_KEY))
                        CheckPKB(TokenType.COMMA, ", EXPECTED");
                        
                    }else if(Match(TokenType.ID)){  

                        effectAsig.TheAmounts?.Add(ParseAssigmentTwoPoints()!);

                        if(!Check(TokenType.CLOSE_KEY))
                        CheckPKB(TokenType.COMMA, ", EXPECTED");

                    }else{
                        throw new Exception("Invalid token in effect assigment");
                    }
                  
                }

            }else{
                effectAsig.Name = new AssigmentExpr(new ID("Name"),Previous().Value!,Expretion());
            }
        }else{
            throw new Exception(": EXPECTED");
        }

        return effectAsig;
    }

    private Selector ParsingSelectAsig()
    {
        Selector selectAsig = new();
        if(Match(TokenType.TWO_POINTS)){
            
            if(Match(TokenType.OPEN_KEY)){
                
                while(!Match(TokenType.CLOSE_KEY))
                {
                    if(Match(TokenType.SOURCE)){

                        if(selectAsig.Source is null)
                            selectAsig.Source = ParseAssigmentTwoPoints();
                        else
                            throw new Exception("Source Param is already asigned");

                    }else if(Match(TokenType.SINGLE)){

                        if(selectAsig.Single is null)
                            selectAsig.Single = ParseAssigmentTwoPoints();
                        else
                            throw new Exception("Single Param is already asigned");
                        
                    }else if(Match(TokenType.PREDICATE)){

                        if(selectAsig.Predicate is null)
                            selectAsig.Predicate = ParsingLambdaExpr(TokenType.PREDICATE);
                        else
                            throw new Exception("Prdicate Param is already asigned");
                    }else{
                        throw new Exception("Invalid input in Selector Param");
                    }

                    if(Match(TokenType.COMMA)|| Check(TokenType.CLOSE_KEY))
                        continue;
                    else
                        throw new Exception("Invalid input in Selector field");
                }
            }else{
                throw new Exception("{ EXPECTED");
            }
        }else{
            throw new Exception(": EXPECTED");
        }

        return selectAsig;
    }
    private List<Expr> ParseRangeCard()
    { 
        CheckPKB(TokenType.TWO_POINTS,": EXPECTED");
        List<Expr> range = new();

        if(Match(TokenType.OPEN_BRACKET)){

            while (!Match(TokenType.CLOSE_BRACKET))
            {
                range.Add(Expretion());
                if(Match(TokenType.COMMA) || Check(TokenType.CLOSE_BRACKET)){

                    continue;
                }else{
                    throw new Exception("Incorrect input in range card");
                }
            }
        }else{
            throw new Exception("] EXPECTED");
        }
        return range;
    }

    private Expr And()
    {
        Expr expr = Equality();
        
        while(Match(TokenType.AND)){

            Token token = Previous();
            Expr right = Equality();
            expr = new BinaryExpr(expr,token,right);
        }

        return expr;
    }

    private Expr Equality() {

        Expr expr = Comparation();

        while (Match(TokenType.BANG_EQUAL, TokenType.EQUAL_EQUAL)){

            Token token = Previous();
            Expr right = Comparation();
            expr = new BinaryExpr(expr, token , right);

        }
        return expr;
    }

    private Expr Comparation() {
        
        Expr expr = Term();
        
        while (Match(TokenType.GREATHER, TokenType.GREATHER_EQUAL, TokenType.LESS, TokenType.LESS_EQUAL)) {
        Token token = Previous();
        Expr right = Term();
        expr = new BinaryExpr(expr, token, right);
        }
        return expr;
    }

    private Expr Term() {

        Expr expr = Factor();
        
        while (Match(TokenType.MINUS, TokenType.PLUS, TokenType.CONC, TokenType.TWO_CONC)) {
            Token token = Previous();
            Expr right = Factor();
            expr = new BinaryExpr(expr, token, right);
        }
            return expr;
    }

    private Expr Factor() {
        
        Expr expr = Potence();
        
        while (Match(TokenType.PRODUCT, TokenType.DIVITION)) {

            Token token = Previous();
            Expr right = Potence();
            expr = new BinaryExpr(expr, token, right);
            }
        
        return expr;
    }

    private Expr Potence() {
        
        Expr expr = PpMm();
        
        while (Match(TokenType.POTENCE)) {

            Token token = Previous();
            Expr right =  PpMm();
            expr = new BinaryExpr(expr, token, right);
        }
        
        return expr;
    }
     private Expr PpMm() { 

        Expr left = NegMinUnary();

        if (Match(TokenType.PLUS_PLUS, TokenType.MINUS_MINUS)) {

            Token token = Previous(); 
            return new UnaryExpr(token,left);
        }
         return left;
    }
    private Expr NegMinUnary() {

        if (Match(TokenType.BANG, TokenType.MINUS, TokenType.PLUS)) {

            Token token = Previous(); 
            Expr right = NegMinUnary();
            return new UnaryExpr(token,right);
        }
         return ParsePoint();
    }
    private Expr ParsePoint()
    {
        Expr expr = Primary();
        
        while (Match(TokenType.POINT, TokenType.OPEN_BRACKET))
        {
            Token token = Previous();
            expr = token.TokenType == TokenType.POINT? new PointExpr(expr,token, ParseFunc()) : new PointExpr(expr,token,Or());
            if(token.TokenType == TokenType.OPEN_BRACKET){CheckPKB(TokenType.CLOSE_BRACKET,"] Expected");}

        }
        return expr;
    }

    private FuncExpr ParseFunc()
    {
        if(Properties.Exists(x => x.Equals(Peek().TokenType))){
            
            var type = Advance().TokenType;

            if(Peek().TokenType.Equals(TokenType.OPEN_BRACKET)){
                Advance();

                var something = Or();
                CheckPKB(TokenType.CLOSE_BRACKET,"] EXPECTED");

                return new FuncExpr(type,something);
            }

            return new FuncExpr(type);
        }

        if(Functions.Exists(x => x.Equals(Peek().TokenType))){
             var type = Advance().TokenType;

            if(type == TokenType.FIND){

                CheckPKB(TokenType.OPEN_PARENTHESIS,"( EXPECTED");
                var something = ParsingFindBody();
                CheckPKB(TokenType.CLOSE_PARENTHESIS, ") EXPECTED");

                return new FuncExpr(type,something);
            } else if(Peek().TokenType.Equals(TokenType.OPEN_PARENTHESIS)){
                Advance();
                
                var something = Peek().TokenType == TokenType.CLOSE_PARENTHESIS ? null : Or();
                CheckPKB(TokenType.CLOSE_PARENTHESIS,") EXPECTED");

                return new FuncExpr(type,something);
            }

            return new FuncExpr(type);
        }

        throw new Exception("Invalid signal expression");
    }

    public LambdaExpr ParsingFindBody(){

        LambdaExpr findBody = new(TokenType.PREDICATE);

        if(Match(TokenType.OPEN_PARENTHESIS)){

            var unit = Or();

            CheckPKB(TokenType.CLOSE_PARENTHESIS,") EXPECTED");
            CheckPKB(TokenType.LAMBDA_EXPR,"=> EXPECTED");

            var lambdaBody = Or();

            findBody.VarExpressions.Add((ID)unit);
            findBody.LambdaBody.Exprs.Enqueue(lambdaBody);
        }else{
           throw new Exception("( EXPECTED");
        }

        return findBody;
    }

    private Expr Primary () {

        if (Match(TokenType.NUMBER, TokenType.STRING, TokenType.ID, TokenType.TRUE, TokenType.FALSE)) {
            
            var tokenValue = Previous().Value!;
            
            return Previous().TokenType switch
            {
                TokenType.NUMBER => new Number(tokenValue),
                TokenType.STRING => new String(tokenValue),
                TokenType.ID => new ID(tokenValue),
                _ => new BooLean(tokenValue,Previous().TokenType),
            };
        }

        if(Match(TokenType.OPEN_PARENTHESIS))
        {
            Expr expr = Expretion();
            CheckPKB(TokenType.CLOSE_PARENTHESIS," ) EXPECTED");
            return new PKBExpr(expr);
        }
        
        throw new Exception("Damn:( ");
        
    }
    private bool Match (params TokenType[] values) {
        
        foreach (var type in values)
        {
              if(Check(type)){ 
                
                Advance();
                return true;
            }
        }
          
        return false;
    }

    private bool Check(TokenType type) {
        
        // if (Peek().TokenType == TokenType.END)
        //     return false;
        
        return Peek().TokenType == type;
    }

    private Token Advance() {

        if (!IsAtEnd())
            Current++;

        return Previous();
    }

    private bool IsAtEnd() {
        
        return Current == Tokens.Count;
    }
    private Token Peek() {

        return Current < Tokens.Count ?Tokens[Current] : new Token("/0",TokenType.END) ;
    }
    private Token Previous() {

        return Tokens[Current - 1];
    }

    private void CheckPKB(TokenType tokenType, string messageError)
    {
        if(Check(tokenType)){
            Advance();
        }else{
            throw new Exception(messageError);
        }
    } 

    private void CheckComma(object node)
    {
        if(Match(TokenType.COMMA)){

            if(node is EffectNode node1) 
                MachineEffect(node1);
            else if(node is CardDec node2)
                MachineCard(node2);
            else if(node is List<OnActivationStat> node5)
                EffectListCard(node5);
            else if(node is OnActivationStat node3)
                TheEffectExt(node3);
            else if(node is PostAction node4)
                ThePosAction(node4);
        }
    }
}
