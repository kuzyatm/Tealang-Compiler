public struct TeaError
{
	public TeaErrorType type;
	public TeaErrorSource src;
	public string summary;
	// public string message; // Formatted!

	public TeaError(TeaErrorType Type, string summary, TeaErrorSource src) {
		this.type    = Type;
		this.summary = summary;
		this.src     = src;
	}

	public static TeaError Create
	(TeaErrorType type, TeaErrorSource src, string summary)
	{
		return new (type, summary, src);
	}

	public static TeaError FileDoesNotExist
	(TeaErrorSource src, string filePath)
	{
		return Create(
			TeaErrorType.FileDoesNotExist, src,
			$"file \"{filePath}\","
		);
	}

	public static TeaError SymbolUndefined
	(TeaErrorSource src, TeaSymbol symbol, TeaOper prev)
	{
		var sym = Extender.ToString(symbol);
		var oper = prev is TeaOper.None
				? Extender.ToString(prev)
				: sym;
		
		return Create(
			TeaErrorType.SymbolUndefined, src,
			$"can't define `{oper}`");
	}

	public static TeaError InvalidSyntax
	(TeaErrorSource src, TokenType prevType, TokenType nextType)
	{
		var prev = prevType.ToString().ToLower();
		var next = nextType.ToString().ToLower();

		src.symbol--;

		return Create(
			TeaErrorType.InvalidSyntax, src,
			$"{prev} and {next}"
		);
	}

	public static string dec(short code)
	{
		return code.ToString("D4");
	}

	static string GetErrorCode(TeaErrorType errorType)
	{
		short num = errorType switch
		{
			TeaErrorType.None => 1, 
			TeaErrorType.FileDoesNotExist => 2,
			TeaErrorType.SymbolUndefined => 3,
			_ => 0, 
		};

		short code = num;
		return "T" + dec(code);
	}

	public string GetErrorCode() => TeaError.GetErrorCode(this.type);

	public override string ToString()
	{
		var errorCode = GetErrorCode();
		var errorSubcode = 0;

		var errorName = this.type.ToString();
		var filePathRel = src.File;
		var file = Path.GetFileName(filePathRel);
		var fileSource = src.FileSource;

		var num = src.line.ToString();
		var spaces = new string(' ', num.Length);
		var empty = $"{spaces} |";
		var spacesToError = new string(' ', src.symbol);

		var summary = this.summary;
		var code = src.GetCode();

		return $"""
		error[{errorCode}-{errorSubcode}]: {errorName}
		 ---> {file}({fileSource})
		{empty}
		{num} |  {code}
		{empty}{spacesToError}^ {summary}
		{empty}
		{spaces}----------


		""";
	}
}