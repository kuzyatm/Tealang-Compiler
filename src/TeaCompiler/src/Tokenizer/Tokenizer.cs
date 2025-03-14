using System.Data.Common;
using System.Net;
using System.Text;

public static class Tokenizer
{
    public static TokenizeCtx ctx;

    static Tokenizer()
    {
        ctx = null!;
    }

    // Returns Tokens until DataGetter
    public static List<Token> GetTokens(DataGetter dataGetter)
    {
        // [Init Context]
        // Don't create new object, clear and reuse
        if (ctx is null)
            ctx = new TokenizeCtx(null!, 0, 0, dataGetter);
        else {
            ctx.ClearAndChange(dataGetter);
        }

        // Fun part xdd
        ctx.SwitchOnCharsUntilFalse(
            TokenizerCharSwitch.CharSwitch
        );
        // TODO: check last object!

        return ctx.GetTokens();
    }
}