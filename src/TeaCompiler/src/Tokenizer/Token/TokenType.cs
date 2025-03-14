public enum TokenType
{
    None = 0,

    // Comment Section
    Comment,

    // Undefined operator
    Symbol,

    // str
    String,

    // class, switch, etc.
    Keyword,

    // Identifier
    Id,

    // 123
    // 1.2234
    // <string, NumberType>
    Number,
}