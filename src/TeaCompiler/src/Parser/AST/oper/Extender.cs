public static partial class Extender {

    /// ...
    /// <summary>
    /// Converts <see cref="TeaOperType"/> to <see cref="String"/>
    /// </summary>
    /// <param name="oper">operator that will be converted</param>
    /// <returns></returns>
    public static string ToStringForCode(this TeaOperType oper)
    {
        return oper switch {
            TeaOperType.Assign               => " = ",
            TeaOperType.Equals               => " == ",
            TeaOperType.Inequals             => " != ",
            TeaOperType.Plus                 => " + ",
            TeaOperType.PlusAssign           => " += ",
            TeaOperType.Increment            => " ++ ",
            TeaOperType.Minus                => " - ",
            TeaOperType.MinusAssign          => " -= ",
            TeaOperType.Decrement            => " -- ",
            TeaOperType.Multiply             => " * ",
            TeaOperType.MultiplyAssign       => " *= ",
            TeaOperType.DivideResult         => " / ",
            TeaOperType.DivideResultAssign   => " /= ",
            TeaOperType.DivideModulus        => " % ",
            TeaOperType.DivideModulusAssign  => " %= ",
            TeaOperType.Separ                => ", ",
            TeaOperType.DoubleDot            => ": ",
            TeaOperType.Bang                 => "!",
            TeaOperType.GetMember            => ".",
            TeaOperType.Range                => "..",
            TeaOperType.Greater              => " > ",
            TeaOperType.GreaterOrEquals      => " >= ",
            TeaOperType.BitShiftR            => " >> ",
            TeaOperType.BitShiftRAssign      => " >>= ",
            TeaOperType.Less                 => " < ",
            TeaOperType.LessOrEquals         => " <= ",
            TeaOperType.BitShiftL            => " << ",
            TeaOperType.BitShiftLAssign      => " <<= ",
            TeaOperType.ArrowRight           => " -> ",
            TeaOperType.BracketOpen          => "[",
            TeaOperType.BracketClose         => "]",
            TeaOperType.ParenthesisOpen      => "(",
            TeaOperType.ParenthesisClose     => ")",
            TeaOperType.BraceOpen            => "{",
            TeaOperType.BraceClose           => "}",
            TeaOperType.BitAnd               => " & ",
            TeaOperType.LogAnd               => " && ",
            TeaOperType.BitAndAssign         => " &= ",
            TeaOperType.BitOr                => " | ",
            TeaOperType.LogOr                => " || ",
            TeaOperType.BitOrAssign          => " |= ",
            TeaOperType.BitXor               => " ^ ",
            TeaOperType.BitXorAssign         => " ^= ",
            TeaOperType.BitNot               => " ~ ",
            TeaOperType.BitNotAssign         => " ~= ",
            _                            => "",
        };
    }
}