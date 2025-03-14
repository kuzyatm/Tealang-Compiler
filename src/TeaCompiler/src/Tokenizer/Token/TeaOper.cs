// TODO: implement flag system for operators
public enum TeaOper
{
	// None for error handling
	None = 0,

	// =
	Assign,
	// ==
	Equals,
	// !=
	Inequals,

	// +
	Plus,
	// +=
	PlusAssign,
	// ++
	Increment,
	
	// -
	Minus,
	// -=
	MinusAssign,
	// --
	Decrement,

	// *
	Multiply,
	// *=
	MultiplyAssign,

	// /
	DivideResult,
	// /=
	DivideResultAssign,

	// %
	DivideModulus,
	// %=
	DivideModulusAssign,

	// ,
	Separ,

	// :
	DoubleDot,
	
	// !
	Bang,

	// .
	GetMember,
	// ..
	Range,


	// >
	Greater,
	// >=
	GreaterOrEquals,
	
	// >>
	BitShiftR,
	// >>=
	BitShiftRAssign,

	// <
	Less,
	// <=
	LessOrEquals,

	// <<
	BitShiftL,
	// <<=
	BitShiftLAssign,

	// ->
	ArrowRight,

	// [
	BracketOpen,
	// ]
	BracketClose,

	// (
	ParenthesisOpen,
	// )
	ParenthesisClose,

	// {
	BraceOpen,
	// }
	BraceClose,


	// &
	BitAnd,
	// &&
	LogAnd,
	// &=
	BitAndAssign,

	// |
	BitOr,
	// ||
	LogOr,
	// |=
	BitOrAssign,

	// ^
	BitXor,
	// ^=
	BitXorAssign,

	// ~
	BitNot,
	// ~=
	BitNotAssign,
}
