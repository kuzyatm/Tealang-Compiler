
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

    public static TeaOper? TeaSymbolToOperCombine(TeaOper oper, TeaSymbol symbol)
    {
        TeaOper? ret = TeaOper.None;
        // TODO: mark error
        void Error()
        {
            ret = null;
        }
        void Set(TeaOper set) => ret = set;

        switch (symbol)
        {
            // ,
            case TeaSymbol.Comma:
                switch (symbol)
                {
                    case TeaSymbol.None:
                        Set(TeaOper.Separ);
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
                    case TeaOper.None:
                        Set(TeaOper.GetMember);
                    break;
                    
                    // ..
                    case TeaOper.GetMember:
                        Set(TeaOper.Range);
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
                    case TeaOper.None:
                        Set(TeaOper.Assign);
                    break;
                    
                    // ==
                    case TeaOper.Assign:
                        Set(TeaOper.Equals);
                    break;

                    // +=
                    case TeaOper.Plus:
                        Set(TeaOper.PlusAssign);
                    break;

                    // -=
                    case TeaOper.Minus:
                        Set(TeaOper.MinusAssign);
                    break;

                    // *=
                    case TeaOper.Multiply:
                        Set(TeaOper.MultiplyAssign);
                    break;

                    // /=
                    case TeaOper.DivideResult:
                        Set(TeaOper.DivideResultAssign);
                    break;

                    // %=
                    case TeaOper.DivideModulus:
                        Set(TeaOper.DivideModulusAssign);
                    break;

                    // &=
                    case TeaOper.BitAnd:
                        Set(TeaOper.BitAndAssign);
                    break;

                    // |=
                    case TeaOper.BitOr:
                        Set(TeaOper.BitOrAssign);
                    break;

                    // ^=
                    case TeaOper.BitXor:
                        Set(TeaOper.BitXorAssign);
                    break;

                    // ~=
                    case TeaOper.BitNot:
                        Set(TeaOper.BitNotAssign);
                    break;

                    // >>=
                    case TeaOper.BitShiftR:
                        Set(TeaOper.BitShiftRAssign);
                    break;

                    // >=
                    case TeaOper.Greater:
                        Set(TeaOper.GreaterOrEquals);
                    break;

                    // <<=
                    case TeaOper.BitShiftL:
                        Set(TeaOper.BitShiftLAssign);
                    break;

                    // <=
                    case TeaOper.Less:
                        Set(TeaOper.LessOrEquals);
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
                    case TeaOper.None:
                        Set(TeaOper.Plus);
                    break;

                    // ++
                    case TeaOper.Plus:
                        Set(TeaOper.Increment);
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
                    case TeaOper.None:
                        Set(TeaOper.Minus);
                    break;

                    // --
                    case TeaOper.Minus:
                        Set(TeaOper.Decrement);
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
                    case TeaOper.None:
                        Set(TeaOper.Multiply);
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
                    case TeaOper.None:
                        Set(TeaOper.DivideResult);
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
                    case TeaOper.None:
                        Set(TeaOper.DivideModulus);
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
                    case TeaOper.None:
                        Set(TeaOper.BitAnd);
                    break;

                    // &&
                    case TeaOper.BitAnd:
                        Set(TeaOper.LogAnd);
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
                    case TeaOper.None:
                        Set(TeaOper.BitOr);
                    break;

                    // ||
                    case TeaOper.BitOr:
                        Set(TeaOper.LogOr);
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
                    case TeaOper.None:
                        Set(TeaOper.BitXor);
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
                    case TeaOper.None:
                        Set(TeaOper.BitNot);
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
                    case TeaOper.None:
                        Set(TeaOper.Greater);
                    break;

                    // >>
                    case TeaOper.Greater:
                        Set(TeaOper.BitShiftR);
                    break;
                    
                    // ->
                    case TeaOper.Minus:
                        Set(TeaOper.ArrowRight);
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
                    case TeaOper.None:
                        Set(TeaOper.Less);
                    break;

                    // <<
                    case TeaOper.Less:
                        Set(TeaOper.BitShiftL);
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
                    case TeaOper.None:
                        Set(TeaOper.DoubleDot);
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
                    case TeaOper.None:
                        Set(TeaOper.BraceOpen);
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
                    case TeaOper.None:
                        Set(TeaOper.BraceClose);
                    break;
                    case TeaOper.BraceOpen:
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
                    case TeaOper.None:
                        Set(TeaOper.ParenthesisOpen);
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
                    case TeaOper.None:
                        Set(TeaOper.ParenthesisClose);
                    break;
                    case TeaOper.ParenthesisOpen:
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
                    case TeaOper.None:
                        Set(TeaOper.BracketOpen);
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
                    case TeaOper.None:
                        Set(TeaOper.BracketClose);
                    break;
                    case TeaOper.BracketOpen:
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

    public static string ToString(TeaOper prev)
    {
        return prev switch {
            TeaOper.Assign               => "=",
            TeaOper.Equals               => "==",
            TeaOper.Inequals             => "!=",
            TeaOper.Plus                 => "+",
            TeaOper.PlusAssign           => "+=",
            TeaOper.Increment            => "++",
            TeaOper.Minus                => "-",
            TeaOper.MinusAssign          => "-=",
            TeaOper.Decrement            => "--",
            TeaOper.Multiply             => "*",
            TeaOper.MultiplyAssign       => "*=",
            TeaOper.DivideResult         => "/",
            TeaOper.DivideResultAssign   => "/=",
            TeaOper.DivideModulus        => "%",
            TeaOper.DivideModulusAssign  => "%=",
            TeaOper.Separ                => ",",
            TeaOper.DoubleDot            => ":",
            TeaOper.Bang                 => "!",
            TeaOper.GetMember            => ".",
            TeaOper.Range                => "..",
            TeaOper.Greater              => ">",
            TeaOper.GreaterOrEquals      => ">=",
            TeaOper.BitShiftR            => ">>",
            TeaOper.BitShiftRAssign      => ">>=",
            TeaOper.Less                 => "<",
            TeaOper.LessOrEquals         => "<=",
            TeaOper.BitShiftL            => "<<",
            TeaOper.BitShiftLAssign      => "<<=",
            TeaOper.ArrowRight           => "->",
            TeaOper.BracketOpen          => "[",
            TeaOper.BracketClose         => "]",
            TeaOper.ParenthesisOpen      => "(",
            TeaOper.ParenthesisClose     => ")",
            TeaOper.BraceOpen            => "{",
            TeaOper.BraceClose           => "}",
            TeaOper.BitAnd               => "&",
            TeaOper.LogAnd               => "&&",
            TeaOper.BitAndAssign         => "&=",
            TeaOper.BitOr                => "|",
            TeaOper.LogOr                => "||",
            TeaOper.BitOrAssign          => "|=",
            TeaOper.BitXor               => "^",
            TeaOper.BitXorAssign         => "^=",
            TeaOper.BitNot               => "~",
            TeaOper.BitNotAssign         => "~=",
            _                            => string.Empty,
        };
    }
}