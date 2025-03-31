public class TeaString : TeaValue {
    object[] members;

    public TeaString(params object[] members) {
        this.members = members;
    }

    public string ToStr() {
        return string.Join(' ', members);
    }

    public string ToTea() {
        return $"\"{this.ToStr()}\"";
    }

    public string ToIR() {
        return $"";
    }
}