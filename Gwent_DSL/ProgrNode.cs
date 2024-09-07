namespace GWENT_DSL;

public class ProgrNode : ASTNode{
    public List<EffectNode> ? EffectNodes {get; protected set;}
    public List<CardDec> ? CardNodes {get; protected set;}
    public override TokenType? Type {get;protected set;}

    public ProgrNode()
    {
        EffectNodes = [];
        CardNodes   = [];
    }

   
}
