public class TeaErrorSource
{
	public string File;
	public int line, symbol;

	public string FileSource => $"{line}:{symbol}";

	public string GetCode() {
		return ReadFileCode();
	}

	public static readonly TeaErrorSource NonFile = new (string.Empty, 0, 0);

	public TeaErrorSource(string file, int line, int symbol)
	{
		this.File = file;
		this.line = line;
		this.symbol = symbol;
	}

	string ReadFileCode()
	{
		if (line + symbol == 0)
			return string.Empty;
		string str;
		int line_ = 0;

		using (var sr = new StreamReader(File))
		{
			while ((str = sr.ReadLine()) != null)
			{
				line_++;
				if (line_ == line)
				{
					return str;
				}
			}
		}

		// QF
		return string.Empty;
	}

	public void Reset(string file) {
		this.File   = file;
		this.line   = 0;
		this.symbol = 0;
	}
}