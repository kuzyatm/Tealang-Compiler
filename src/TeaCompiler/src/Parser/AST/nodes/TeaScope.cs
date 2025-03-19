public class TeaScope {
    TeaVar[] vars;

    public TeaScope(params TeaVar[] vars)
    {
        this.vars = vars;
    }

    public string ToCode()
    {
        return "(\n    " + string.Join(",\n    ", vars.Select((v) => v.ToCode())) + "\n)";
    }

    // public override string ToString() => ToCode();
}