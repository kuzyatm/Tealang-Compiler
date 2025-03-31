public delegate void CodeWriterDg(string str);

public class CodeWriter
{
    CodeWriterDg dg;
    int depth;

    public static readonly CodeWriter Console = new (System.Console.WriteLine);

    public CodeWriter(CodeWriterDg dg)
    {
        this.dg = dg;
        depth = 0;
    }

    public void IncrDepth() => depth++;
    public void DecrDepth() => depth--;

    public void Write(string str) {
        var pref = new string(' ', 4*depth);
        dg(pref + str);
    }
}