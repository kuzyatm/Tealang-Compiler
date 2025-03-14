public struct TeaNumber
{
    public string Number;
    public TeaNumberType Type;

    public TeaNumber(string Number, TeaNumberType Type)
    {
        this.Number = Number;
        this.Type = Type;
    }

    public override string ToString()
    {
        return $"{Type}: {Number}";   
    }
}
// TODO: add specifier aka: 1.2d, 1.3f, 1i (int), 1l (long)
public enum TeaNumberType
{
    None = 0,
    Byte,
    Short,
    Int,
    Long,
    Float,
    Double
}