public class TeaVar {
    string name;
    string type;

    public TeaVar(string type, string name) {
        this.type = type;
        this.name = name;
    }

    public string ToCode()
    {
        return $"{type}: {name}";
    }
}