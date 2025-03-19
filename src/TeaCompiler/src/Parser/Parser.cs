using System.Collections.ObjectModel;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using static System.Console;

public static class Parser
{
    public static TeaAssembly asm = null!;
    public static TeaErrorStackTrace stack = null!;

    public static TeaAssembly Parse (
        string relativePathToFile,
        List<Token> tokens,
        TeaCompilerReturn ret
    ) {
        if (asm is null) {
            asm   = new TeaAssembly();
            stack = new TeaErrorStackTrace(relativePathToFile);
        } else {
            asm.Reset();
            stack.Reset(relativePathToFile);
        }

        var oper     = TeaOper.None;
        var prevType = TokenType.None;

        void CheckTypeError(TokenType nextType) {
            switch (nextType) {
                case TokenType.Id:
                case TokenType.Number:
                case TokenType.String:
                    if (prevType
                        is TokenType.Id
                        or TokenType.Number
                        or TokenType.String
                    )
                        ret.Error(
                            TeaError.InvalidSyntax(stack.GetErrorSource(), prevType, nextType));
                break;
                
                default:
                break;
            }
        }

        foreach (var token in tokens)
        {
            // [ DEBUG LOG ]
            DebugLogToken(token);

            switch (token.type)
            {
                case TokenType.Keyword:
                    var (kwType, kwValue) = (ValueTuple<TeaKeywordType, object?>)token.value;
                    var len = SwitchKeyword(kwType, kwValue);
                    stack.Move(len);
                break;


                case TokenType.Id:
                    oper = TeaOper.None;
                    var id = (string)token.value;

                    stack.Move(id!.Length);

                    CheckTypeError(token.type);
                break;

                
                case TokenType.String:
                    var str = (string)token.value;

                    CheckTypeError(token.type);
                break;


                case TokenType.Number:
                    var number = (TeaNumber)token.value;

                    stack.Move(number.Number.Length);

                    CheckTypeError(token.type);
                break;


                // Construct operator
                case TokenType.Symbol:
                    var symbol = (TeaSymbol)token.value;

                    stack.NextChar();

                    // Update operator
                    TeaOper? res = Extender.TeaSymbolToOperCombine(oper, symbol); 
                    
                    // Handle result
                    if (res == null) {
                        ret.Error(TeaError.SymbolUndefined(stack.GetErrorSource(), symbol, oper));
                    } else {
                        oper = (TeaOper)res;
                    }
                break;

                case TokenType.Comment:
                    var (comment, multiline, comLen) = (ValueTuple<string, bool, TeaObjLength>)token.value;
                    if (multiline) {
                        stack.NextLine(comLen.lines);
                        stack.Move(comLen.symbols);
                    } else {
                        stack.NextLine();
                    }
                break;
            }

            prevType = token.type;
        }

        return asm;
    }

    private static void DebugLogToken(Token token)
    {
        if (token.type is TokenType.Symbol) {
            var value = (TeaSymbol)token.value;
            switch (value) {
                case TeaSymbol.NewLine:
                case TeaSymbol.Space:
                break;

                default:
                    Console.WriteLine(token);
                break;
            }
            return;
        }

        Console.WriteLine(token);
    }

    public static TeaObjLength SwitchKeyword(TeaKeywordType type, object? value_)
    {
        var len = new TeaObjLength();

        // Cast value
        T Cast<T>()
        {
            return (T)value_;
        }

        switch (type)
        {
            case TeaKeywordType.AccessModifier:
                var access = Cast<AccessModifierType>();
            
                // [ Length ]
                if (access is AccessModifierType.Local)
                {
                    len.symbols = 5;
                } else {
                    len.symbols = 4;
                }

            break;

            case TeaKeywordType.Typedef:
                var typ = Cast<TypedefType>();

                len.symbols = typ.ToString().Length;
            break;

            case TeaKeywordType.PkgDef:
                len.symbols = 3;
            break;

            case TeaKeywordType.PkgUse:
                len.symbols = 3;
            break;
            
            case TeaKeywordType.Var:
                len.symbols = 3;
            break;

            case TeaKeywordType.Alias:
                len.symbols = 5;
            break;

            

            // case TeaKeywordType.:
            // var typ = Cast<>();
            // break;
        }

        return len;
    }
}