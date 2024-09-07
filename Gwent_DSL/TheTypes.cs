
using System.Data.SqlTypes;
using System.Linq.Expressions;

namespace GWENT_DSL;
public abstract class ASTNode{
    public abstract TokenType ? Type {get; protected set;}
}

public abstract class AtomicExp : Expr {
}
