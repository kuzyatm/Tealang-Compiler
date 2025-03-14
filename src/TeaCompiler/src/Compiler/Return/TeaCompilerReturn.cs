public class TeaCompilerReturn
{
    public List<TeaError>   errors;
    public List<TeaWarning> warnings;
    public List<TeaMessage> messages;

    public TeaCompilerReturn()
    {
        errors   = new ();
        warnings = new ();
        messages = new ();
    }

    public void Error(TeaError err)    => errors  .Add(err);
    public void Warn (TeaWarning warn) => warnings.Add(warn);
    public void Msg  (TeaMessage msg)  => messages.Add(msg);  

    public bool HasErrors  () => errors.Count   != 0;
    public bool HasWarnings() => warnings.Count != 0;
    public bool HasMessages() => messages.Count != 0;
}