

public class Token{
    public Token(string _value, TokenType tokenType)
    {
        Value     = _value;
        TokenType = tokenType;
    }

    public string ? Value {get; private set;}
    public TokenType  TokenType {get; private set;}    
}
