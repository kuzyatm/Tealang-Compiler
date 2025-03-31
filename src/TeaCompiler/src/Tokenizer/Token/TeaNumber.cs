public struct TeaNumber : TeaValue
{
    public string Number;
    public TeaNumberType Type;

    public TeaNumber(string Number, TeaNumberType Type)
    {
        this.Number = Number;
        this.Type = Type;
    }

    public string ToStr() {
        return $"{Type}: {Number}"; 
    }

    public string ToTea() {
        return $"{(Type is TeaNumberType.None ? "" : $"{Type.ToString().ToLower()}")}{Number}";
    }

    public string ToIR() {
        return ToTea();
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