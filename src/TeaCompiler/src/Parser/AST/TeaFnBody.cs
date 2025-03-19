public class TeaFnBody {
    public TeaStatement[] lines;

    public static TeaFnBody Empty => new TeaFnBody();


    public TeaFnBody(params TeaStatement[] lines) {
        this.lines = lines;
    }

    public string ToCode(string depth) {
        return string.Join($"{depth}\n", lines.Select((l) => l.ToCode()));
    }
}