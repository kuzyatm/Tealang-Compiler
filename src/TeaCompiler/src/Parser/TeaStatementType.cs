public enum TeaStatementType : byte
{
    None = 0,
    Define,   // var, class, scope, etc
    Invoke,   // invoke member
    Operator, // operator
}