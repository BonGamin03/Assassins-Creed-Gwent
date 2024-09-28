using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class FuncExpr : Expr
{
    public FuncExpr(TokenType function)
    {
        Function = function;
        Type = TokenType.FUNCTION_EXPR;
    }

    public FuncExpr(TokenType function, Expr param)
    {
        Function = function;
        Param = param;
        Type = TokenType.FUNCTION_EXPR;
    }

    
    public Expr Param{get;}
    public override Scope Scope { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public TokenType Function { get; }
    public override TokenType? Type { get; protected set;}


    public override bool CheckSemantic(Scope scope)
    {
        return true;
    }

    public override object Evaluate(Scope scope)
    {
        return Param.Evaluate(scope);
    }

    public object Evaluate(Scope scope, object value){

        if(value is GameObject gameObject){
            
           GetTypeCard(gameObject,out object card);

           if(card is UnityCardScript card1){

                if(Function == TokenType.POWER){
                    return (double)card1.PointAttackCard;
                }

                if(Function == TokenType.NAME){
                    return card1.NameCard;
                }

                if(Function == TokenType.FACTION){
                    return card1.FactionCard;
                }

                if(Function == TokenType.TYPE){
                    return card1.TypeCard.ToString();
                }

                if(Function == TokenType.RANGE){
                    return card1.TypeAttackCard.ToString();
                }

                if(Function == TokenType.OWNER){
                    return card1.Owner;
                }
           }else if(card is WeatherCardScript card2){

                if(Function == TokenType.POWER){
                    return 0.0;
                }

                if(Function == TokenType.NAME){
                    return card2.NameCard;
                }

                if(Function == TokenType.FACTION){
                    return "";
                }

                if(Function == TokenType.TYPE){
                    return card2.TypeCard.ToString();
                }

                if(Function == TokenType.RANGE){
                    return card2.TypeClim.ToString();
                }

                if(Function == TokenType.OWNER){
                    return card2.Owner;
                }
           }else if(card is AumentCardScript card3){

                if(Function == TokenType.POWER){
                    return 0.0;
                }

                if(Function == TokenType.NAME){
                    return card3.NameCard;
                }

                if(Function == TokenType.FACTION){
                    return "";
                }

                if(Function == TokenType.TYPE){
                    return card3.TypeCard.ToString();
                }

                if(Function == TokenType.RANGE){
                    return card3.RowAument;
                }

                if(Function == TokenType.OWNER){
                    return card3.Owner;
                }
           }else if(card is BossesEfect card4){
                if(Function == TokenType.POWER){
                    return 0.0;
                }

                if(Function == TokenType.NAME){
                    return card4.NameCard;
                }

                if(Function == TokenType.FACTION){
                    return "";
                }

                if(Function == TokenType.TYPE){
                    return UnityCard.EnumTypeCard.Boss.ToString();
                }

                if(Function == TokenType.RANGE){
                    return "";
                }

                if(Function == TokenType.OWNER){
                    return card4.Owner;
                }
           }else if(card is CleanCardScript card5){
                if(Function == TokenType.POWER){
                    return 0.0;
                }

                if(Function == TokenType.NAME){
                    return card5.NameCard;
                }

                if(Function == TokenType.FACTION){
                    return "";
                }

                if(Function == TokenType.TYPE){
                    return UnityCard.EnumTypeCard.CleanCard.ToString();
                }

                if(Function == TokenType.RANGE){
                    return "";
                }

                if(Function == TokenType.OWNER){
                    return card5.Owner;
                }
           } else if(card is LureCardScript card6){
                if(Function == TokenType.POWER){
                    return 0.0;
                }

                if(Function == TokenType.NAME){
                    return card6.NameCard;
                }

                if(Function == TokenType.FACTION){
                    return "";
                }

                if(Function == TokenType.TYPE){
                    return UnityCard.EnumTypeCard.Lure.ToString();
                }

                if(Function == TokenType.RANGE){
                    return "";
                }

                if(Function == TokenType.OWNER){
                    return card6.Owner;
                }
           }
        }else if(value is List<GameObject> listCard){
    
            if(Function == TokenType.PUSH){

                CompilerManager.OperationPuSeRe operation = CompilerManager.Push;
                OperationToList(scope,listCard,operation);
                return true; //esto es para que no se ejecute la Exepcion en los metodos void 

            }else if(Function == TokenType.SEND_BOTTOM){

                CompilerManager.OperationPuSeRe operation = CompilerManager.SendBottom;
                OperationToList(scope,listCard,operation);
                return true;

            }else if(Function == TokenType.REMOVE){

                CompilerManager.OperationPuSeRe operation = CompilerManager.Remove;
                OperationToList(scope,listCard,operation);
                return true;

            }else if(Function == TokenType.POP){
    
                return CompilerManager.Pop(listCard);
                
            }else if(Function == TokenType.SHUFFLE){
                CompilerManager.Shuffle(listCard);
                return true;      
            }else if(Function == TokenType.FIND){
                
                if(Param is not null && Param.Evaluate(scope) is Predicate<GameObject> predicate){

                    return CompilerManager.Find(predicate,listCard);
                }else{throw new Exception("Missing predicate in function");}

            }else if(Function == TokenType.ADD){
                
                 if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    
                    listCard.Add(card);
                    return card;
                }else{throw new Exception("Missing card in Add function");}

            }
        }else if(value.ToString().ToLower() is "context"){

            if(Function == TokenType.TRIGGER_PLAYER){

                return CompilerManager.GetTriggerPlayer();

            }else if(Function == TokenType.BOARD){

               (Field, Field) board = (CompilerManager.GetPlayer().FieldOfPlayer(),CompilerManager.GetOtherPlayer().FieldOfPlayer());
               return board;

            }else if(Function == TokenType.HAND_OF_PLAYER){

                if(Param is not null && Param.Evaluate(scope) is double triggerPlayer){
                    
                    return CompilerManager.GetPlayerById(triggerPlayer).HandOfPlayer();

                }else{throw new Exception("Invalid input in HandOfPlayer");}
            }else if(Function == TokenType.FIELD_OF_PLAYER){
                if(Param is not null && Param.Evaluate(scope) is double triggerPlayer){
                    
                    return CompilerManager.GetPlayerById( triggerPlayer).FieldOfPlayer();

                }else{throw new Exception("Invalid input in FieldOfPlayer");}
                
            }else if(Function == TokenType.GRAVEYARD_OF_PLAYER){

                if(Param is not null && Param.Evaluate(scope) is double triggerPlayer){
                    
                    return CompilerManager.GetPlayerById( triggerPlayer).GraveyardPlayer();

                }else{throw new Exception("Invalid input in GraveyardOfPlayer");}

            }else if(Function == TokenType.DECK_OF_PLAYER){
                if(Param is not null && Param.Evaluate(scope) is double triggerPlayer){
                    
                    return CompilerManager.GetPlayerById( triggerPlayer).DeckOfPlayer();

                }else{throw new Exception("Invalid input in DeckOfPlayer");}
            }else if(Function == TokenType.HAND){

                return CompilerManager.GetPlayerById(CompilerManager.GetTriggerPlayer()).HandOfPlayer();
            }else if(Function == TokenType.FIELD){

                return CompilerManager.GetPlayerById(CompilerManager.GetTriggerPlayer()).FieldOfPlayer();
            }else if(Function == TokenType.GRAVEYARD){

                return CompilerManager.GetPlayerById(CompilerManager.GetTriggerPlayer()).GraveyardPlayer();
            }else if(Function == TokenType.DECK){

                return CompilerManager.GetPlayerById(CompilerManager.GetTriggerPlayer()).DeckOfPlayer();
            }
        }else if(value is (Field,Field)){
            
            if(Function == TokenType.REMOVE){

                if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    
                    GetTypeCard(card, out object card1 );
                    (Field,Field) value1 = ((Field,Field))value;
                    value1.Item1.Remove(card);
                    value1.Item2.Remove(card);
                    return true;
                }else{throw new Exception("Invalid input in Remove function");}
                
            }else if(Function == TokenType.FIND){

                if(Param is not null && Param.Evaluate(scope) is Predicate<GameObject> card){
                    
                    (Field,Field) value1 = ((Field,Field))value;
                    value1.Item1.Find(card);
                    value1.Item2.Find(card);
                    return true;
                }else{throw new Exception("Invalid input in Remove function");}
            }
        }else if(value is Field y){
            
            if(Function == TokenType.REMOVE){

                if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    GetTypeCard(card, out object card1 );
                    y.Remove(card);
                    return true;
                }else{throw new Exception("Invalid input in Remove function");}
                
            }else if(Function == TokenType.FIND){

                if(Param is not null && Param.Evaluate(scope) is Predicate<GameObject> card){
                    y.Find(card);
                    return true;
                }else{throw new Exception("Invalid input in Remove function");}
            }
        }else if(value is Hand x){
            if(Function == TokenType.REMOVE){

                if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    
                    GetTypeCard(card, out object card1 );
                    x.Remove(card);
                    return true;
                }else{throw new Exception("Invalid input in Remove function");}
                
            }else if(Function == TokenType.FIND){

                if(Param is not null && Param.Evaluate(scope) is Predicate<GameObject> predicate){
                    x.Find(predicate);
                    return true;
                }else{throw new Exception("Invalid input in Find function");}
            }else if(Function == TokenType.PUSH){

                if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    
                    GetTypeCard(card, out object card1 );
                    x.Push(card);
                    return true;
                }else{throw new Exception("Invalid input in Pushfunction");}
                
            }else if(Function == TokenType.SEND_BOTTOM){

                if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    
                    GetTypeCard(card, out object card1 );
                    x.SendBottom(card);
                    return true;
                }else{throw new Exception("Invalid input in SendBottom function");}
                
            } else if(Function == TokenType.ADD){

                if(Param is not null && Param.Evaluate(scope) is GameObject card){
                    
                    GetTypeCard(card, out object card1 );
                    x.Add(card);
                    return true;
                }else{throw new Exception("Invalid input in Add function");}
                
            }else if(Function == TokenType.POP){

                 x.Pop();
                return true;
            }else if(Function == TokenType.SHUFFLE){
                x.Shuffle();
                return true;
            }
        }

        throw new Exception("Invalid Function acces");
    }
        
    public override void GetScope(Scope scope)
    {
        throw new NotImplementedException();
    }

    public UnityCard.EnumTypeCard GetTypeCard(GameObject gameObject, out object card)
    {
          if(gameObject.TryGetComponent(out UnityCardScript unityCard))
       {
         
          card = unityCard;
          return UnityCard.EnumTypeCard.UnityCard;

       } if(gameObject.TryGetComponent(out WeatherCardScript weatherCard)){
         
          card = weatherCard;
          return UnityCard.EnumTypeCard.WeatherCard;

       }else if(gameObject.TryGetComponent(out AumentCardScript aumentCard)){
         
         card = aumentCard;
         return UnityCard.EnumTypeCard.AumentCard;

       }else if(gameObject.TryGetComponent(out BossesEfect bossCard)){
          
          card = bossCard;
          return UnityCard.EnumTypeCard.Boss;
       }else if(gameObject.TryGetComponent(out LureCardScript lureCard)){
          
          card = lureCard;
          return UnityCard.EnumTypeCard.Lure;
       }else if(gameObject.TryGetComponent(out CleanCardScript cleanCard)){
          
          card = cleanCard;
          return UnityCard.EnumTypeCard.CleanCard;
       }

        throw new Exception("Invalid Compiler Card");
    }

    private void OperationToList(Scope scope, List<GameObject> listCard, CompilerManager.OperationPuSeRe operation)
    {
        if(Param is not null && Param.Evaluate(scope) is GameObject card ){
                    
            GetTypeCard(card, out object card2); // esto es para que en caso de que se pase cualquier game object que no sea una carta devuelva un error
            operation(listCard,card);
        }else{throw new Exception("Missing or invalid object for Push");}
    }


}

