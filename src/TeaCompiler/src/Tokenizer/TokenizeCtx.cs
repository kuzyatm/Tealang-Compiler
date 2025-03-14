using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Text;

public delegate (char[], int)? DataGetter();
public delegate BuilderAction CharIterator(char ch);
public delegate void DataConsumer(TokenizeCtx ctx);

public class TokenizeCtx
{
    List<Token> tokens;

    public char[] chars;
    
    public int id;
    public int len;

    public char? ch;
    
    DataGetter dataGetter;

    public StringBuilder builder;

    
    public TokenizeCtx(DataGetter dataGetter)
    : this(null!, 0, 0, dataGetter){}
    public TokenizeCtx(in char[] data, int id, int len, DataGetter dataGetter)
    {
        this.tokens = new ();
        this.chars = data;
        this.id = id;
        this.len = len;
        
        this.dataGetter = dataGetter;

        builder = new StringBuilder();
    }

    // False -> No data,  end
    // True  -> Got data, continue
    public bool MoveNext()
    {
        var getterResult = dataGetter.Invoke();

        // No Data
        if (getterResult is null) {
            return false;
        }

        (this.chars, this.len) = getterResult.Value;
        this.id = 0;
        
        return true;
    }
    
    public bool MoveNextChar()
    {
        // next < len
        if (id+1 < len)
        {
            id++;
            return true;
        }
        else {
            return MoveNext();
        }
    }

    public void SwitchOnCharsUntilFalse(CharIterator iterator)
    {
        // Iterate chars
        do
        {
            for (; id < len; id++)
            {
                ch = chars[id];

                // invoke member
                var retAction = iterator.Invoke((char)ch);

                if (retAction.HasFlag(BuilderAction.Write) && ch != null)
                    builder.Append(ch);

                if (retAction.HasFlag(BuilderAction.End))
                    return;
            }
        } while (MoveNext());
    }

    public void AddToken(Token token) => tokens.Add(token);
    public void AddToken(TokenType type, object? value)
        => AddToken(new Token(type, value));

    public void AddKeywordToken(TeaKeywordType type, object? value) => AddToken(TokenType.Keyword, (type, value));
    
    public void ClearTokens()         => tokens.Clear();
    public List<Token> GetTokens()    => tokens;

    public string GetStringAndClear()
    {
        var str = GetBuilderContent();
        ClearBuilder();
        return str;
    }

    public void ClearAndChange(DataGetter dataGetter)
    {
        ClearTokens();
        ClearBuilder();
        this.dataGetter = dataGetter;
    }

    public void PreventWrite() => ch = '0';
    public void RemoveLastFromBuilder() => builder.Length--;
    public void RemoveFromBuilderStart(int count) => builder.Remove(0, count);
    public void AppendCurrent() => builder.Append(chars[id]);

    public string GetBuilderContent() => builder.ToString();
    public void ClearBuilder() => builder.Clear();

    public void Init()
    {
        MoveNext();
    }
}