using System.Reflection.Metadata.Ecma335;

public struct Token
{
    public TokenType type;
    public object? value;

    public Token(TokenType type, object? value)
    {
        this.type  = type;
        this.value = value;
    }

    public void Deconstruct(out TokenType type, out object? value)
    {
        type  = this.type;
        value = this.value;
    }

    public override string ToString()
    {
        return value is null ? $"{type}" : $"{type} : \'{value}\'";
    }
}