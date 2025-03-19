public class TeaErrorSource
{
	public string File;
	public int line, symbol;

	public string FileSource => $"{line}:{symbol}";

	public string GetCode() {
		return ReadFileCode();
	}

	public TeaErrorSource(string file, int line, int symbol)
	{
		this.File = file;
		this.line = line;
		this.symbol = symbol;
	}

	string ReadFileCode()
	{
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
}