using Err = TeaErrorType;

public enum TeaErrorType
{
    // just for default
    // if got it on compile -> mark error, it should appear
    None = 0,
    FileDoesNotExist,
    SymbolUndefined,
    InvalidSyntax,
}