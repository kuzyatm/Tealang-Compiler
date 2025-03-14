using System.Data.Common;
using System.Net;
using static Tokenizer;

// Made to separate logic of tokenizer and its switch.
// And ofc for clearer code purposes
public static class TokenizerCharSwitch
{

#region Getters

    static string GetString()
    {
        ctx.ClearBuilder();
        ctx.id++;

        ctx.SwitchOnCharsUntilFalse(
            (ch) =>
            {
                
                if (ch is '"')
                    return BuilderAction.End;

                return BuilderAction.Write;
                
                // Console.Write(ch);
                // return BuilderAction.None;
            }
        );

        return ctx.GetStringAndClear();
    }

    static (string, bool, TeaObjLength) GetComment(bool multiline)
    {
        ctx.ClearBuilder();
        ctx.id++;

        var len = new TeaObjLength(0, 1);
        len.symbols++;

        BuilderAction GetCommentLine(char ch)
        {
            switch (ch)
            {
                case '\n':
                return BuilderAction.End;

                case '\r':
                return BuilderAction.None;
            }

            len.symbols++;
            return BuilderAction.Write;
        }

        bool dot = false;
        BuilderAction GetCommentMultiLine(char ch)
        {
            len.symbols++;
            switch (ch)
            {
                case '/':
                    if (dot)
                    {
                        // remove * trailing symbol
                        ctx.RemoveLastFromBuilder();
                        return BuilderAction.End;
                    }
                break;
                
                case '*':
                    dot = true;
                break;

                case '\n':
                    len.lines++;
                    len.symbols = 0;
                break;
            }

            return BuilderAction.Write;
        }
        CharIterator func;
        if (multiline) {
            func = GetCommentMultiLine;
        } else {
            func = GetCommentLine;
            len.lines++;
        }
        
        ctx.SwitchOnCharsUntilFalse(
            func
        );

        ctx.PreventWrite();
        return (ctx.GetStringAndClear(), multiline, len);
    }
    
    static void KeywordSwitch(string keyword)
    {
        switch (keyword)
        {
            // Ignore Empty, Quickfix
            case "":
                break;

        #region Access Modifiers
            case "publ":
                ctx.AddKeywordToken(TeaKeywordType.AccessModifier, AccessModifierType.Publ);
                break;
            case "priv":
                ctx.AddKeywordToken(TeaKeywordType.AccessModifier, AccessModifierType.Priv);
                break;
            case "prot":
                ctx.AddKeywordToken(TeaKeywordType.AccessModifier, AccessModifierType.Prot);
                break;
            case "local":
                ctx.AddKeywordToken(TeaKeywordType.AccessModifier, AccessModifierType.Local);
                break;
        #endregion

        #region Typedef
            case "struct":
                ctx.AddKeywordToken(TeaKeywordType.Typedef, TypedefType.Struct);
                break;
            case "class":
                ctx.AddKeywordToken(TeaKeywordType.Typedef, TypedefType.Class);
                break;
            case "enum":
                ctx.AddKeywordToken(TeaKeywordType.Typedef, TypedefType.Enum);
                break;
            case "lambda":
                ctx.AddKeywordToken(TeaKeywordType.Typedef, TypedefType.Lambda);
                break;
        #endregion

            // Alias
            case "alias":
                ctx.AddKeywordToken(TeaKeywordType.Alias, null);
                break;

            // Packages
            case "pkg":
                ctx.AddKeywordToken(TeaKeywordType.PkgDef, null);
                break;
            case "use":
                ctx.AddKeywordToken(TeaKeywordType.PkgUse, null);
                break;

            // Mutability Modifiers
            case "const":
                ctx.AddKeywordToken(TeaKeywordType.Mutability, false);
                break;
            case "mut":
                ctx.AddKeywordToken(TeaKeywordType.Mutability, true);
                break;

            case "switch":
                ctx.AddKeywordToken(TeaKeywordType.Switch, null);
                break;

            case "var":
                ctx.AddKeywordToken(TeaKeywordType.Var, null);
            break;

            default:
                ctx.AddToken(TokenType.Id, keyword);
                break;
        }
    }

    static BuilderAction GetNumberSwitch(char ch)
    {
        // [Is Number]
        if (char.IsDigit(ch)) {
            return BuilderAction.Write;
        }

        // [Is Symbol | Letter]
        else {

            // [Letter]
            // explicit type definer
            if (char.IsLetter(ch))
            {   
                switch (ch)
                {
                    case 'b':
                        type = TeaNumberType.Byte;
                    break;
                    
                    case 's':
                        type = TeaNumberType.Short;
                    break;

                    case 'i':
                        type = TeaNumberType.Int;
                    break;
                    
                    case 'l':
                        type = TeaNumberType.Long;
                    break;
                    
                    case 'f':
                        type = TeaNumberType.Float;
                    break;

                    case 'd':
                        type = TeaNumberType.Double;
                    break;

                    // Read keyword
                    default:
                    return BuilderAction.None;
                }

                if (type != TeaNumberType.None)
                {
                    var num = ctx.GetStringAndClear();
                    ctx.AddToken(TokenType.Number, new TeaNumber(num, type));
                    return BuilderAction.End;
                }
            }
            
            // [Symbol]
            else {
                if (ch is '.')
                {
                    return BuilderAction.Write;
                }

                var num = ctx.GetStringAndClear();
                ctx.AddToken(TokenType.Number, new TeaNumber(num, TeaNumberType.None));

                SymbolCase(ch);
            }
            return BuilderAction.End;
        }
    }

    static void GetNumber(TokenizeCtx ctx)
    {
        type = TeaNumberType.None;
        ctx.ClearBuilder();

        ctx.SwitchOnCharsUntilFalse(
            GetNumberSwitch
        );
        var num = ctx.GetStringAndClear();
        if (num != string.Empty)
            ctx.AddToken(TokenType.Number, num);
    }

    static BuilderAction GetKeywordOrId(char ch)
    {
        // [Symbol]
        // Here we check if id ends.
        // id: a120, d12, dD, etc.
        // not id: 120dda, 4faa

        if (!char.IsLetterOrDigit(ch))
        {
            var keywordOrId = ctx.GetStringAndClear();
            KeywordSwitch(keywordOrId);

            SymbolCase(ch);
            return BuilderAction.End;
        }

        switch (ch)
        {
            // Ignore carriage return
            case '\r': break;

            case ' ' or '\n':
            return BuilderAction.End;
        }

        return BuilderAction.Write;
    }

    static void LetterCase()
    {
        ctx.ClearBuilder();

        ctx.SwitchOnCharsUntilFalse(
            GetKeywordOrId
        );

        var keywordOrId = ctx.GetStringAndClear();
        if (keywordOrId != string.Empty)
            KeywordSwitch(keywordOrId);
    }
    
    static void AddDivAndCh(char ch)
    {
        ctx.AddToken(TokenType.Symbol, TeaSymbol.Divide);
        var s = ch.CharToTeaSymbol();
        ctx.AddToken(TokenType.Symbol, s);
    }
    
    static void SymbolCase(char ch)
    {
        switch (ch)
        {
            case ' ':
            ctx.AddToken(TokenType.Symbol, ch.CharToTeaSymbol());
            break;
            // Fuck CRLF, LF rules
            case '\r': break;

            case '"':
                var str = GetString();
                ctx.AddToken(TokenType.String, str);
            break;

            case '/':
                if (tildaMet) {
                    var commentTuple = GetComment(false);

                    ctx.AddToken(TokenType.Comment, commentTuple);
                    
                    tildaMet = false;
                }
                else {
                    tildaMet = true;
                }
                
            break;

            case '*':
                if (tildaMet) {
                    var commentTuple = GetComment(true);
                    ctx.AddToken(TokenType.Comment, commentTuple);
                    tildaMet = false;
                }
                else {
                    AddDivAndCh(ch);
                }
            break;
            
            default:
                var teaSymbol = ch.CharToTeaSymbol();
                ctx.AddToken(TokenType.Symbol, teaSymbol);
            break;
        }
    }

    static void NumberCase(char ch) =>
        GetNumber(ctx);

#endregion

    static BuilderAction builderAction;
    static TeaNumberType type;
    static bool tildaMet = false;
    
    public static BuilderAction CharSwitch(char ch)
    {
        // [Is Letter or Number]
        if (char.IsLetterOrDigit(ch)) {
            if (tildaMet) {
                tildaMet = false;
                ctx.AddToken(TokenType.Symbol, TeaSymbol.Divide);
            }
            // [Is Number]
            if (char.IsNumber(ch)) {
                NumberCase(ch);    
            }

            // [Is Letter]
            else {
                // Prevent writing special symbol
                builderAction = BuilderAction.None;
                
                LetterCase();
            }
        }
        // [Is Special Symbol]
        else {
            SymbolCase(ch);
        }

        return builderAction;
    }
}