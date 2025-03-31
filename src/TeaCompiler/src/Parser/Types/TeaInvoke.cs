public class TeaInvoke : TeaValue {
    TeaNode   name;
    TeaNode[] args;
    
    public TeaInvoke(TeaNode name, TeaNode[] args)
    {
        this.name = name;
        this.args = args;
    }

    public string ToStr()
    {
        var strArgs = string.Join(", ", args.Select((arg) => arg.ToTea()));
        return $"{name.ToTea()}({strArgs})";
    }

    public string ToTea()
    {
        return "invoke code";
    }

    public string ToIR() {
        return $"";
    }
}