[Flags]
public enum BuilderAction
{
    None  = 0,
    End   = 1 << 0,
    Write = 1 << 1,

    EndAndWrite = End | Write
}