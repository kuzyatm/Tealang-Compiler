public class TeaId : TeaValue {
    string id;

    public TeaId(string id)
    {
        this.id = id;
    }

    public string ToStr()
    {
        return $"id: {id}";
    }

    public string ToTea()
    {
        return id;
    }

    public string ToIR()
    {
        return id;
    }
}