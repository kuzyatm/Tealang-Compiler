public class TeaErrorStackTrace
{
    int line, symbol;
    string file;

    public TeaErrorStackTrace(string file)
    {
        line = 1;
        symbol = 1;
        this.file = file;
    }

    public void NextChar()
    {
        symbol++;
    }
    public void NextLine(int lines = 1) 
    {
        line += lines;
        symbol = 1;
    }

    public void Reset(string file)
    {
        this.line   = 1;
        this.symbol = 1;
        this.file   = file;
    }

    public TeaErrorSource GetErrorSource()
    {
        return new TeaErrorSource(file, line, symbol);
    }

    public void Move(TeaObjLength len)
    {
        if (len.lines != 0)
        {
            this.line   += len.lines;
            this.symbol  = len.symbols;
        }
        else {
            this.symbol += len.symbols;
        }
    }
    public void Move(int symbols)
    {
        this.symbol += symbols;
    }
}