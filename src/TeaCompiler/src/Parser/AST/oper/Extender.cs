public static partial class Extender {

    /// ...
    /// <summary>
    /// Converts <see cref="TeaOper"/> to <see cref="String"/>
    /// </summary>
    /// <param name="oper">operator that will be converted</param>
    /// <returns></returns>
    public static string ToStringForCode(this TeaOper oper)
    {
        return oper switch {
            TeaOper.Assign               => " = ",
            TeaOper.Equals               => " == ",
            TeaOper.Inequals             => " != ",
            TeaOper.Plus                 => " + ",
            TeaOper.PlusAssign           => " += ",
            TeaOper.Increment            => " ++ ",
            TeaOper.Minus                => " - ",
            TeaOper.MinusAssign          => " -= ",
            TeaOper.Decrement            => " -- ",
            TeaOper.Multiply             => " * ",
            TeaOper.MultiplyAssign       => " *= ",
            TeaOper.DivideResult         => " / ",
            TeaOper.DivideResultAssign   => " /= ",
            TeaOper.DivideModulus        => " % ",
            TeaOper.DivideModulusAssign  => " %= ",
            TeaOper.Separ                => ", ",
            TeaOper.DoubleDot            => ": ",
            TeaOper.Bang                 => "!",
            TeaOper.GetMember            => ".",
            TeaOper.Range                => "..",
            TeaOper.Greater              => " > ",
            TeaOper.GreaterOrEquals      => " >= ",
            TeaOper.BitShiftR            => " >> ",
            TeaOper.BitShiftRAssign      => " >>= ",
            TeaOper.Less                 => " < ",
            TeaOper.LessOrEquals         => " <= ",
            TeaOper.BitShiftL            => " << ",
            TeaOper.BitShiftLAssign      => " <<= ",
            TeaOper.ArrowRight           => " -> ",
            TeaOper.BracketOpen          => "[",
            TeaOper.BracketClose         => "]",
            TeaOper.ParenthesisOpen      => "(",
            TeaOper.ParenthesisClose     => ")",
            TeaOper.BraceOpen            => "{",
            TeaOper.BraceClose           => "}",
            TeaOper.BitAnd               => " & ",
            TeaOper.LogAnd               => " && ",
            TeaOper.BitAndAssign         => " &= ",
            TeaOper.BitOr                => " | ",
            TeaOper.LogOr                => " || ",
            TeaOper.BitOrAssign          => " |= ",
            TeaOper.BitXor               => " ^ ",
            TeaOper.BitXorAssign         => " ^= ",
            TeaOper.BitNot               => " ~ ",
            TeaOper.BitNotAssign         => " ~= ",
            _                            => string.Empty,
        };
    }
}