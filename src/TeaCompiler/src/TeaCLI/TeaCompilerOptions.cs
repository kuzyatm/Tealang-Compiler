public class TeaCompilerOptions
{
    public List<string> Files;
    public TeaCompilingStage CompilingStage;


    public TeaCompilerOptions()
    {
        Files = new List<string>();
        CompilingStage = TeaCompilingStage.FullCompile;
    }

    // Return Default Compiling Options
    public static TeaCompilerOptions GetDefault()
    { 
        var opts = new TeaCompilerOptions();
        return opts;
    }

    // Get TeaCompilerOptions from cli args
    public TeaCompilerOptions
    GetCompilerOptionsFromArgs(string[] args)
    {
        // Get Default
        var opts = GetDefault();

        // Parse Args, Modify Default Opts

        // Return
        return opts;
    }
    
    public bool HaveToQuitOnTokenize() => CompilingStage == TeaCompilingStage.Tokenize;
    public bool HaveToQuitOnParse   () => CompilingStage == TeaCompilingStage.Parse;
    public bool HaveToQuitOnAnalyze () => CompilingStage == TeaCompilingStage.Analyze;
    public bool HaveToQuitOnOptimize() => CompilingStage == TeaCompilingStage.Optimize;


    
    public void AddFile(string fPath)
    {
        Files.Add(fPath);
    }

    public void SetCompilingStage(TeaCompilingStage stage)
    => CompilingStage = stage;
}