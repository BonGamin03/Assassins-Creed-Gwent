

using System;
using System.Collections.Generic;
using GWENT_DSL;
using Unity.VisualScripting;

public class Lexer{
    
     private string text;

     public int position; 

     private char currentChar { get{ 
        if(position>= text.Length) return '\0'; 
     return text[position];}
     }

     

    public Lexer(string text)
    {
        this.text = text;
    }  
    
     private void Advanced(){
        position++;
     }
    private string Doubles(){
   string result="";
  while(currentChar!='\0'&& (char.IsDigit(currentChar) || currentChar == '.')){
     result+=currentChar;
     Advanced();
  }  
  return result;
 } 

 
   
   public Token GetTokens()
      {  
          if(currentChar=='\0')
          {
              return new Token("\0",TokenType.END);
          }
          else if(char.IsWhiteSpace(currentChar)){
             var start=position;
             while(char.IsWhiteSpace(currentChar))  Advanced();
             
             var length=position-start;
             var result=text.Substring(start,length);
            return new Token(result,TokenType.WHITE_SPACE);
          } 
          else if(currentChar==';'){
            Advanced();
            return new (";",TokenType.COMMA_POINT);
           }
           
        else if(char.IsDigit(currentChar)){
            string result=Doubles();
           return new (result.ToString(),TokenType.NUMBER);
         }  
            
           else if(char.IsLetter(currentChar)){
               int start=position;
               string result="";
               while(char.IsLetter(currentChar)|| char.IsDigit(currentChar)){
                  result+=currentChar;
                  Advanced();
               }
               TokenType type= GetKeyword(result);
               return new Token(result,type);
               
            } else if(currentChar=='\"'){
                 Advanced();
                 var start=position;
                while(currentChar!='\"')  Advanced();
             
             var length=position-start;
             var result=text.Substring(start,length);
             Advanced();
            return new Token(result,TokenType.STRING);
            }
          
         else  if(currentChar == '+'){
             Advanced();
             if(currentChar=='='){

              Advanced();
               return new Token("+=",TokenType.PLUS_EQUAL);
             }
               if(currentChar=='+'){
                Advanced();
               return new Token("++",TokenType.PLUS_PLUS);
             }
             return new Token("+",TokenType.PLUS);
           } 
          else  if(currentChar == '-'){
             Advanced();
             if(currentChar=='='){
              Advanced();
               return new Token("-=",TokenType.MINUS_EQUAL);
             }
              if(currentChar=='-'){
                Advanced();
               return new Token("--",TokenType.MINUS_MINUS);
             }
             return new Token("-",TokenType.MINUS);
           } 
         else   if(currentChar == '*'){
            Advanced();
             if(currentChar == '='){
            Advanced();
             return new Token("*=", TokenType.PRODUCT_EQUAL);
             }
             return new Token("*",TokenType.PRODUCT);
           }
          else  if(currentChar == '/'){
            Advanced();
             if(currentChar == '='){
            Advanced();
             return new Token("/=", TokenType.DIVITION_EQUAL);
             }
            return new Token("/",TokenType.DIVITION);
           } 
            else  if(currentChar == '^'){
              Advanced();
               return new Token("^",TokenType.POWER);
           }
          else if(currentChar == '('){
            Advanced();
             return new Token("(",TokenType.OPEN_PARENTHESIS);
           } 
         else  if(currentChar == ')'){
          Advanced();
             return new Token(")",TokenType.CLOSE_PARENTHESIS);
           } 
            else  if(currentChar == '{'){
              Advanced();
             return new Token("{",TokenType.OPEN_KEY);
           } 
            else  if(currentChar == '}'){
              Advanced();
             return new Token("}",TokenType.CLOSE_KEY);
           } 
            else  if(currentChar == '['){
              Advanced();
             return new Token("[",TokenType.OPEN_BRACKET);
           } 
             else  if(currentChar == ']'){
              Advanced();
             return new Token("]",TokenType.CLOSE_BRACKET);
           } 
             else  if(currentChar == ','){
              Advanced();
             return new Token(",",TokenType.COMMA);
           } 
             else  if(currentChar == ':'){
              Advanced();
             return new Token(":",TokenType.TWO_POINTS);
           } 
            else  if(currentChar == '.'){
              Advanced();
             return new Token(".",TokenType.POINT);
           } 
          else if(currentChar=='!'){
            Advanced();
            if(currentChar=='='){
              Advanced();
              return new Token("!=",TokenType.BANG_EQUAL);
            }
            return new Token("!",TokenType.BANG);
          }
          else if(currentChar=='>'){
             Advanced();
            if(currentChar=='='){
              Advanced();
              return new Token(">=",TokenType.GREATHER_EQUAL);
            }
            int pos=position-1;
            return new Token(">",TokenType.GREATHER);
          }
          else if(currentChar=='<'){
              Advanced();
            if(currentChar=='='){
              Advanced();
              return new Token("<=", TokenType.LESS_EQUAL);
            }
            int pos=position-1;
            return new Token("<",TokenType.LESS);
          }
          else if(currentChar== '&'){
             Advanced();
             if(currentChar=='&'){
              Advanced();
               return new Token("&&",TokenType.AND);
             }
          }
          else if(currentChar== '|'){
             Advanced();
             if(currentChar=='|'){
              Advanced();
               return new Token("||",TokenType.OR);
             }
          } 
            else if(currentChar== '='){
             Advanced();
             if(currentChar=='='){
              Advanced();
               return new Token("==", TokenType.EQUAL_EQUAL);
             }
              if(currentChar=='>'){
                Advanced();
                 return new Token("=>", TokenType.LAMBDA_EXPR);
              }
             int pos=position-1;
             return new Token("=",TokenType.EQUAL);
          } 
            else if(currentChar== '@'){
             Advanced();
             if(currentChar=='@'){
              Advanced();
               return new Token("@@", TokenType.TWO_CONC);
             }
             int pos=position-1;
             return new Token("@", TokenType.CONC);
          } 
        throw new Exception("Invalid character");
      }

    private TokenType GetKeyword(string result)
    {
        switch (result)
        { 
          case "true":
          return  TokenType.TRUE;
          case "false" :
           return  TokenType.FALSE;
           case "in" :
           return  TokenType.IN;
           case "while":
           return  TokenType.WHILE;
           case "for" :
           return  TokenType.FOR;
           case "effect" :
           return  TokenType.EFFECT;
           case "Effect":
           return TokenType.EFFECT_CARD;
           case "Name" :
           return  TokenType.NAME;
           case "Params" :
           return  TokenType.PARAMS;
            case "card":
            return TokenType.CARD_DEC;
           case "Number" :
           return  TokenType.NUMBER_PARAM;
           case "String" :
           return  TokenType.STRING_PARAM;
           case "Bool" :
           return  TokenType.BOOL_PARAM;
           case "Action" :
           return  TokenType.ACTION;
            
           
           case "Power" :
           return  TokenType.POWER;
           case "Deck" :
           return  TokenType.DECK;
           case "Hand" :
           return  TokenType.HAND;
           case "Owner" :
           return  TokenType.OWNER;
           case "Board" :
           return  TokenType.BOARD;
           case "Add" :
           return  TokenType.ADD;
           case "DeckOfPlayer" :
           return  TokenType.DECK_OF_PLAYER;
           case "HandOfPlayer":
           return TokenType.HAND_OF_PLAYER;
           case "Push" :
           return  TokenType.PUSH;
           case "Remove" :
           return  TokenType.REMOVE;
           case "Pop" :
           return  TokenType.POP;
           case "Shuffle" :
           return  TokenType.SHUFFLE;
           case "Type" :
           return  TokenType.TYPE;
           case "Faction" :
           return  TokenType.FACTION;
           case "Range" :
           return  TokenType.RANGE;
           case "OnActivation" :
           return  TokenType.ON_ACTIVATION;
           case "Selector" :
           return  TokenType.SELECTOR;
           case "Source" :
           return  TokenType.SOURCE;
           case "Single" :
           return  TokenType.SINGLE;
           case "Predicate" :
           return  TokenType.PREDICATE;
           case "PostAction" :
           return  TokenType.POST_ACTION;
           case "TriggerPlayer" :
           return  TokenType.TRIGGER_PLAYER;
           case "FieldOfPlayer" :
           return  TokenType.FIELD_OF_PLAYER;
           case "GraveyardOfPlayer" :
           return  TokenType.GRAVEYARD_OF_PLAYER;
           case "Field" :
           return  TokenType.FIELD;
           case "Graveyard" :
           return  TokenType.GRAVEYARD;
           case "Find" :
           return  TokenType.FIND;
           case "SendBottom" :
           return  TokenType.SEND_BOTTOM;
           case "Melee" :
           return  TokenType.MELEE;
           case "Ranged" :
           return  TokenType.RANGED;
           case "Siege" :
           return  TokenType.SIEGE;
            
           default:
           return  TokenType.ID;
        }
    }
}