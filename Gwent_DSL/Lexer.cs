namespace GWENT_DSL;

public static class Lexer
{
   
    private static readonly Dictionary<List<char>,TokenType> GenericDiccionary = new()
     {
        {new List<char>(){'+','-','*','/','^', '+'},TokenType.ARITHMETICAL_OPERATORS},
        {new List<char>(){'&','|'}, TokenType.LOGICAL_OPEARATOR},
        {new List<char>(){'>','<','='},TokenType.COMPARATION_OPERATOR},
        {new List<char>(){'.',',',';',':','[',']','{','}','(',')'},TokenType.SIGNAL_OPERATOR},
        {new List<char>(){'@'},TokenType.CONCATENATION},
        {new List<char>(){'"'},TokenType.QUOTATION_MARKS}
    };
    
    public static List<Token> Lexing (string input)
    {
        string buffer = "";
        int index = 0;
        TokenType genericToken = TokenType.NoState;
        List<Token> tokens = [];

        while(index < input.Length)
        {
            if(char.IsWhiteSpace(input[index])){
                index++;
                continue;
            }
            
            if(char.IsLetter(input[index])){

                genericToken = TokenType.ID;
                (genericToken,buffer,index) =IsKeyword(buffer,input,index, genericToken);
                

            }else if(char.IsDigit(input[index])){

                genericToken = TokenType.NUMBER;
                (buffer,index) = IsNumber(input,index,buffer);
               

            }else
            {
                (genericToken,buffer)= GenericToken(buffer,input[index],genericToken,GenericDiccionary);

                if(genericToken  == TokenType.ARITHMETICAL_OPERATORS)
                {
                    genericToken = ArithmeticalOperator(buffer,genericToken);
                    index ++;

                    if (genericToken == TokenType.PLUS || genericToken == TokenType.LESS)
                    { 
                        (genericToken,buffer,index) = TheAditionsOperatorAndSubstraction(input[index],index,buffer,genericToken);
                        
                    }
                    else if(genericToken == TokenType.PRODUCT || genericToken == TokenType.DIVITION){

                        (genericToken,buffer,index) = ProductAndDivisionOperator(input[index],index,buffer,genericToken);
                        (genericToken,buffer) = SendToken(tokens,buffer,genericToken);
                        continue;
                    }
                }else if(genericToken == TokenType.COMPARATION_OPERATOR)
                {
                    genericToken = ComparingOperator(buffer,genericToken); // repetido 
                    index ++;

                    (genericToken,buffer,index) = ComparationOperator(input[index],index,buffer,genericToken);
                 

                }else if(genericToken == TokenType.LOGICAL_OPEARATOR)
                {
                    genericToken = ComparingOperator(buffer, genericToken);
                    index ++;
                    (genericToken,buffer,index) = LogicalOperator(input[index],index,buffer,genericToken);

                }else if(genericToken == TokenType.SIGNAL_OPERATOR)
                {
                    (genericToken,index) = SignalOperator(buffer,index,genericToken);

                }else if(genericToken == TokenType.CONCATENATION)
                {
                    index++;
                    (genericToken,buffer,index) = ConcatenationToken(input[index],index,buffer,genericToken);
                }else if (genericToken == TokenType.QUOTATION_MARKS)
                {
                    //falta
                }
            // Aqui hay que organizar algunas cosas:
            //-Primero hay que poner la funcion de las senales ya que como esta es un token unario no hace
            //falta sumar al index y a las demas si les hace falta por tantpo la ponemos de primera y despues
            //sumamos fuera de la condicion con el objetivo de no repetir lineas de codigo 
            //-Hay que organixar el manejo de errores
            //-Hay que poner los indices en los tokens para devolver la ubicacion especifica del token 
            }

            (genericToken,buffer) = SendToken(tokens,buffer,genericToken);
            continue;
        }

        return tokens;
    }

    //First DF
    //Next function is for check buffer is a Keyword or just an idenftifier 
    private static (TokenType, string, int)IsKeyword(string buffer, string input, int index, TokenType tokenType)
    {
        while(char.IsLetterOrDigit(input[index]) || input[index] is '_') // Es probable que si la palabra termina con una palabra reservada y no un espacio se rompa por el indice
        {
            buffer+=input[index];
            tokenType = ChekingKeywords(buffer);

            index ++;
        }

        return (tokenType,buffer,index);
    }

    //Next function is for tokenize numbers

    private static (string,int)IsNumber(string input, int index, string buffer)
    {
        while (char.IsDigit(input[index]))
        {
            buffer += input[index];
            index++;
        }

        return (buffer,index);
    }



    //Next function is for encapsulate the process of send a token 

    private static (TokenType,string) SendToken(List<Token> tokens, string buffer, TokenType tokenType)
    {
        tokens.Add(new Token(buffer,tokenType));
        buffer = "";
        tokenType = TokenType.NoState;

        return (tokenType,buffer);
    }


    //Next function is for check the type of keyword
    private static TokenType ChekingKeywords(string buffer)
    {
        switch(buffer)
        {
            case "effect":
            return TokenType.EFFECT;

             case "card":
            return TokenType.CARD;

             case "Name":
            return TokenType.NAME;

             case "Params":
            return TokenType.PARAMS;

             case "Action":
            return TokenType.ACTION;

             case "Number":
            return TokenType.NUMBER;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            // case "Number":
            // return TokenType.NumberKeyWordToken;

            default:
            return TokenType.ID;
        }
    }

    // Next function is for take of the generic token and work with him
    //Importante : puede que si haya un caracter desonocido dado como esta creado el siguiente metodo es probable que lo omita
    private static (TokenType, string) GenericToken (string buffer, char letter,TokenType tokenType, Dictionary<List<char>, TokenType> generic)
    {
        buffer += letter;
        foreach (var item in generic)
        {
            if(item.Key.Contains(letter))
            {
                tokenType = item.Value;
                break;
            }
        }
        
        return (tokenType,buffer);
    }

    // Next function is for identify the kind of arithmetical operator
    private static TokenType ArithmeticalOperator(string buffer, TokenType tokenType)
    {
        switch(buffer)
        {
            case "+":
            return TokenType.PLUS;
        
            case "-":
            return TokenType.MINUS;

            case "/":
            return TokenType.DIVITION;
            

            // case "^":
            // return TokenType.PotenceToken;
            

            // case "++":
            // return TokenType.PlusPlusToken;
            

            case "+=":
            return TokenType.PLUS_EQUAL;
            

            case "-=":
            return TokenType.MINUS_EQUAL;
            

            // case "*=":
            // return TokenType.PlusEqualToken;
            

            // case "/=" :
            // return TokenType.PlusEqualToken;
            
            default:
            return tokenType;
        }
    }

    //Next fuction is for identify what kind of comparation operator is it
    private static TokenType ComparingOperator(string buffer, TokenType tokenType)
    {
        switch(buffer)
        {
            case "=":
            return TokenType.EQUAL;
            
            
            case ">":
            return TokenType.GREATHER;  
            

            case "<":
            return TokenType.LESS;
            

            case "==":
            return TokenType.EQUAL_EQUAL;
            

            case ">=":
            return TokenType.GREATHER_EQUAL;
            

            case "<=":
            return TokenType.LESS_EQUAL;
            

            case "=>":
            return TokenType.LAMBDA_EXPR;

            default : 
            return tokenType; 
        }
    }
    //Next function is for unary tokens 
    private static TokenType UnaryOperator(string buffer, TokenType tokenType)
    {
        switch(buffer)
        {
            case ",":
            return TokenType.COMMA;
            
            
            // case ".":
            // return TokenType.PointToken;  
            

            case ";":
            return TokenType.COMMA_POINT;
            

            case ":":
            return TokenType.TWO_POINTS;
            

            case "{":
            return TokenType.OPEN_KEY;
            

            case "}":
            return TokenType.CLOSE_KEY;
            

            // case "[":
            // return TokenType.OpenBracketToken;
            

            // case "]":
            // return TokenType.CloseBracketToken;
            

            case "(":
            return TokenType.OPEN_PARENTHESIS;
            

            case ")":
            return TokenType.CLOSE_PARENTHESIS;

            default :
            return tokenType;
        }
    }

    //Next function is for identify if the token is a && or a || 
     private static TokenType LogicOperator(string buffer, TokenType tokenType)
    {
        switch(buffer)
        {
            case "||":
            return TokenType.OR;
             
            case "&&":
            return TokenType.AND;  
            
            default :
            return tokenType;
        }
    }
    
    //Este metodo y el de abajo se pueden hacer uno, temporalmente lo mantendremos asi 
    //Next fuction is for identify if the token is ++(--),+=(-=) or just a +(-)
    
    private static (TokenType, string, int) TheAditionsOperatorAndSubstraction(char letter,int index, string buffer, TokenType tokenType)
    {
        if(letter.ToString() == buffer || letter == '=')
        {
            buffer += letter;
            tokenType = ArithmeticalOperator(buffer, tokenType);
            index ++;
        }
        return (tokenType,buffer,index);
    }
    //Next fuction is for identify if the is *=(/=) or just a *(/)
    private static (TokenType, string, int) ProductAndDivisionOperator (char letter, int index, string buffer, TokenType tokenType)
    {
        if(letter == '=')
        {
            buffer+= letter ; 
            tokenType = ArithmeticalOperator(buffer, tokenType);
            index ++;
        }

        return (tokenType,buffer,index);
    }
    //Next function es for identify what kind of comparation token is it

    private static (TokenType, string, int) ComparationOperator(char letter, int index, string buffer, TokenType tokenType)
    {
        if(letter == '=')
        {
            buffer+= letter; 
            tokenType = ComparingOperator(buffer, tokenType);
            index ++;
        }

        return (tokenType,buffer,index);
    }

    //Next fuction is for identify what kind of logic token is it
     private static (TokenType, string, int) LogicalOperator(char letter, int index, string buffer, TokenType tokenType)
    {
        if((char.Parse(buffer) == '&' && letter ==  '&') || (char.Parse(buffer) == '|' && letter ==  '|')){
            buffer += letter;
            tokenType = LogicOperator(buffer,tokenType);
            index++;
        }else
        {
            throw new Exception("LEXICAL MISTAKE!!!");
        }

        return (tokenType,buffer,index);
    }
    
    //Next function is for tokinize the Symbols language 
    private static (TokenType, int) SignalOperator(string buffer, int index, TokenType tokenType)
    {
        tokenType = UnaryOperator(buffer,tokenType);
        index++;

        return (tokenType,index);
    }

    private static (TokenType,string, int) ConcatenationToken(char letter, int index, string buffer, TokenType tokenType)
    {
        if(letter == '@')
        {
            buffer += letter;
            //tokenType = TokenType.ConcatenationWithSpaceToken;
            index ++;  
        }
        return(tokenType,buffer,index);
    }

    private static (TokenType, string, int) StringToken(string input, int index, string buffer, TokenType tokenType)
    {
        tokenType = TokenType.STRING;
        
        while (input[index] is not '"') //Aqui con un try catch tratare de coger la exepcion de que no cerro comilla y lanzarla porque se puede ir de index 
        {

            buffer += input[index];
            index++;

        }
        return (tokenType,buffer,index);
    }
}
