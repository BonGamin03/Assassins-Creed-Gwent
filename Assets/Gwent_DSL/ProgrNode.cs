

using System.Collections.Generic;

public class ProgrNode : ASTNode{
    public static List<EffectNode>  Effects = new();
    public static List<CardDec> Cards = new();
    public override TokenType? Type {get;protected set;}

   
    public static List<CardData> CompiledCards(string input){

        var listOfCards = new List<CardData>();
        var parser = new Parser(input);
        parser.Parsing();

        foreach (var item in Cards)
        {
            Scope scope = new();
            listOfCards.Add((CardData)item.Evaluate(scope!));
        }

        return listOfCards;
    }
    
   
}
