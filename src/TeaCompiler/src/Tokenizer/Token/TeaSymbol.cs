
using System.Dynamic;

public enum TeaSymbol
{
    None = 0,   

    // =
    Equal = '=',

    // +
    Plus = '+',

    // -
    Minus = '-',

    // /
    Divide = '/',

    // %
    Modulus = '%',

    // *
    Multiply = '*',

    // $
    Dollar = '$',

    // @
    At = '@',

    // !
    Exclamation = '!',
    
    // ;
    Semicolon = ';',

    // ,
    Comma = ',',

    // :
    Colon = ':',

    // ~
    Tilde = '~',

    // ^
    Caret = '^',

    // |
    Pipe = '|',

    // &
    Ampersand = '&',

    // <
    LessThan = '<',

    // >
    GreaterThan = '>',

    // "
    DoubleQuote = '"',

    // '
    SingleQuote = '\'',

    // ?
    Question = '?',

    // .
    Dot = '.',

    // (
    ParenthesisOpen = '(',

    // )
    ParenthesisClose = ')',

    // [
    BracketOpen = '[',

    // ]
    BracketClose = ']',

    // {
    BraceOpen = '{',

    // }
    BraceClose = '}',

    // `\n`
    NewLine = '\n',

    // `\t`
    Tab = '\t',

    // ` `
    Space = ' ',
}

public static partial class Extender
{
    public static TeaSymbol CharToTeaSymbol(this char ch)
    {
        return (TeaSymbol)(ch);
    }

    public static string ToString(this TeaSymbol symbol)
    {
        if (symbol == TeaSymbol.NewLine)
            return "\\n";
        else
            return char.ToString((char)symbol);
    }

    public static TeaOperType? TeaSymbolToOperCombine(TeaOperType oper, TeaSymbol symbol)
    {
        TeaOperType? ret = TeaOperType.None;
        // TODO: mark error
        void Error()
        {
            ret = null;
        }
        void Set(TeaOperType set) => ret = set;

        switch (symbol)
        {
            // ,
            case TeaSymbol.Comma:
                switch (symbol)
                {
                    case TeaSymbol.None:
                        Set(TeaOperType.Separ);
                    break;

                    default:
                        Error();
                    break;
                }
            break;
            
            //. ..
            case TeaSymbol.Dot:
                switch (oper)
                {
                    // .
                    case TeaOperType.None:
                        Set(TeaOperType.GetMember);
                    break;
                    
                    // ..
                    case TeaOperType.GetMember:
                        Set(TeaOperType.Range);
                    break;

                    // error
                    default:
                        Error();
                    break;
                }
            break;

            // = ==    += -= *= /= %=    &= |= ^= ~=    >>= >=  <<= <=
            case TeaSymbol.Equal:
                switch (oper)
                {
                    // =
                    case TeaOperType.None:
                        Set(TeaOperType.Assign);
                    break;
                    
                    // ==
                    case TeaOperType.Assign:
                        Set(TeaOperType.Equals);
                    break;

                    // +=
                    case TeaOperType.Plus:
                        Set(TeaOperType.PlusAssign);
                    break;

                    // -=
                    case TeaOperType.Minus:
                        Set(TeaOperType.MinusAssign);
                    break;

                    // *=
                    case TeaOperType.Multiply:
                        Set(TeaOperType.MultiplyAssign);
                    break;

                    // /=
                    case TeaOperType.DivideResult:
                        Set(TeaOperType.DivideResultAssign);
                    break;

                    // %=
                    case TeaOperType.DivideModulus:
                        Set(TeaOperType.DivideModulusAssign);
                    break;

                    // &=
                    case TeaOperType.BitAnd:
                        Set(TeaOperType.BitAndAssign);
                    break;

                    // |=
                    case TeaOperType.BitOr:
                        Set(TeaOperType.BitOrAssign);
                    break;

                    // ^=
                    case TeaOperType.BitXor:
                        Set(TeaOperType.BitXorAssign);
                    break;

                    // ~=
                    case TeaOperType.BitNot:
                        Set(TeaOperType.BitNotAssign);
                    break;

                    // >>=
                    case TeaOperType.BitShiftR:
                        Set(TeaOperType.BitShiftRAssign);
                    break;

                    // >=
                    case TeaOperType.Greater:
                        Set(TeaOperType.GreaterOrEquals);
                    break;

                    // <<=
                    case TeaOperType.BitShiftL:
                        Set(TeaOperType.BitShiftLAssign);
                    break;

                    // <=
                    case TeaOperType.Less:
                        Set(TeaOperType.LessOrEquals);
                    break;

                    // error
                    default:
                        Error();
                    break;
                }
            break;

            // + ++
            case TeaSymbol.Plus:
                switch (oper)
                {
                    // +
                    case TeaOperType.None:
                        Set(TeaOperType.Plus);
                    break;

                    // ++
                    case TeaOperType.Plus:
                        Set(TeaOperType.Increment);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // - --
            case TeaSymbol.Minus:
                switch (oper)
                {
                    // -
                    case TeaOperType.None:
                        Set(TeaOperType.Minus);
                    break;

                    // --
                    case TeaOperType.Minus:
                        Set(TeaOperType.Decrement);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // *
            case TeaSymbol.Multiply:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.Multiply);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // /
            case TeaSymbol.Divide:
                switch (oper)
                {
                    // /
                    case TeaOperType.None:
                        Set(TeaOperType.DivideResult);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // %
            case TeaSymbol.Modulus:
                switch (oper)
                {
                    // %
                    case TeaOperType.None:
                        Set(TeaOperType.DivideModulus);
                    break;

                    default:
                        Error();
                    break;
                }
            break;
            
            // & &&
            case TeaSymbol.Ampersand:
                switch (oper)
                {
                    // &
                    case TeaOperType.None:
                        Set(TeaOperType.BitAnd);
                    break;

                    // &&
                    case TeaOperType.BitAnd:
                        Set(TeaOperType.LogAnd);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // | ||
            case TeaSymbol.Pipe:
                switch (oper)
                {
                    // |
                    case TeaOperType.None:
                        Set(TeaOperType.BitOr);
                    break;

                    // ||
                    case TeaOperType.BitOr:
                        Set(TeaOperType.LogOr);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // ^
            case TeaSymbol.Caret:
                switch (oper)
                {
                    // ^
                    case TeaOperType.None:
                        Set(TeaOperType.BitXor);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // ~
            case TeaSymbol.Tilde:
                switch (oper)
                {
                    // ~
                    case TeaOperType.None:
                        Set(TeaOperType.BitNot);
                    break;

                    default:
                        Error();
                    break;
                }
            break;
            
            // > >> ->
            case TeaSymbol.GreaterThan:
                switch (oper)
                {
                    // >
                    case TeaOperType.None:
                        Set(TeaOperType.Greater);
                    break;

                    // >>
                    case TeaOperType.Greater:
                        Set(TeaOperType.BitShiftR);
                    break;
                    
                    // ->
                    case TeaOperType.Minus:
                        Set(TeaOperType.ArrowRight);
                    break;

                    default:
                        Error();
                    break;
                }
            break;
            
            // < <<
            case TeaSymbol.LessThan:
                switch (oper)
                {
                    // <
                    case TeaOperType.None:
                        Set(TeaOperType.Less);
                    break;

                    // <<
                    case TeaOperType.Less:
                        Set(TeaOperType.BitShiftL);
                    break;

                    default:
                        Error();
                    break;
                }
            break;
 
            // :
            case TeaSymbol.Colon:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.DoubleDot);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // {
            case TeaSymbol.BraceOpen:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.BraceOpen);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // }
            case TeaSymbol.BraceClose:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.BraceClose);
                    break;
                    case TeaOperType.BraceOpen:
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // (
            case TeaSymbol.ParenthesisOpen:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.ParenthesisOpen);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // )
            case TeaSymbol.ParenthesisClose:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.ParenthesisClose);
                    break;
                    case TeaOperType.ParenthesisOpen:
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // [
            case TeaSymbol.BracketOpen:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.BracketOpen);
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // ]
            case TeaSymbol.BracketClose:
                switch (oper)
                {
                    case TeaOperType.None:
                        Set(TeaOperType.BracketClose);
                    break;
                    case TeaOperType.BracketOpen:
                    break;

                    default:
                        Error();
                    break;
                }
            break;

            // ;         
            case TeaSymbol.Semicolon:
            break;
            
            case TeaSymbol.NewLine:
                Parser.stack.NextLine();
            break;

            case TeaSymbol.Space:
            break;

            default:
                Error();
            break;
        }
        return ret;
    }

    public static string ToString(TeaOperType oper)
    {
        return oper switch {
            TeaOperType.Assign               => "=",
            TeaOperType.Equals               => "==",
            TeaOperType.Inequals             => "!=",
            TeaOperType.Plus                 => "+",
            TeaOperType.PlusAssign           => "+=",
            TeaOperType.Increment            => "++",
            TeaOperType.Minus                => "-",
            TeaOperType.MinusAssign          => "-=",
            TeaOperType.Decrement            => "--",
            TeaOperType.Multiply             => "*",
            TeaOperType.MultiplyAssign       => "*=",
            TeaOperType.DivideResult         => "/",
            TeaOperType.DivideResultAssign   => "/=",
            TeaOperType.DivideModulus        => "%",
            TeaOperType.DivideModulusAssign  => "%=",
            TeaOperType.Separ                => ",",
            TeaOperType.DoubleDot            => ":",
            TeaOperType.Bang                 => "!",
            TeaOperType.GetMember            => ".",
            TeaOperType.Range                => "..",
            TeaOperType.Greater              => ">",
            TeaOperType.GreaterOrEquals      => ">=",
            TeaOperType.BitShiftR            => ">>",
            TeaOperType.BitShiftRAssign      => ">>=",
            TeaOperType.Less                 => "<",
            TeaOperType.LessOrEquals         => "<=",
            TeaOperType.BitShiftL            => "<<",
            TeaOperType.BitShiftLAssign      => "<<=",
            TeaOperType.ArrowRight           => "->",
            TeaOperType.BracketOpen          => "[",
            TeaOperType.BracketClose         => "]",
            TeaOperType.ParenthesisOpen      => "(",
            TeaOperType.ParenthesisClose     => ")",
            TeaOperType.BraceOpen            => "{",
            TeaOperType.BraceClose           => "}",
            TeaOperType.BitAnd               => "&",
            TeaOperType.LogAnd               => "&&",
            TeaOperType.BitAndAssign         => "&=",
            TeaOperType.BitOr                => "|",
            TeaOperType.LogOr                => "||",
            TeaOperType.BitOrAssign          => "|=",
            TeaOperType.BitXor               => "^",
            TeaOperType.BitXorAssign         => "^=",
            TeaOperType.BitNot               => "~",
            TeaOperType.BitNotAssign         => "~=",
            _                            => "{oper}",
        };
    }
}