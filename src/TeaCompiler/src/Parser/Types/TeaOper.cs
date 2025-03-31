public class TeaOper : TeaValue {
    TeaOperType type;

    public TeaOper(TeaOperType type) {
        this.type = type;
    }

    public string ToStr() {
        return Extender.ToString(type);
    }

    public string ToTea() {
        return Extender.ToStringForCode(type);
    }

    public string ToIR() {
        return ToTea();
    }
}