
using System.Data.SqlTypes;
using System.Linq.Expressions;

public abstract class ASTNode{
    public abstract TokenType ? Type {get; protected set;}
}

public abstract class AtomicExp : Expr {
}
