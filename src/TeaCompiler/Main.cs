const string DIR = @"../../shared/";
string GetDir(string path) => DIR + path;


// Compiling Options
TeaCompilerOptions opts;

// Get Options
#if DEBUG
TeaCompilerOptions GetDebugCompilerOptions()
{
    var opts = TeaCompilerOptions.GetDefault();
    // opts.SetCompilingStage(TeaCompilingStage.Tokenize);

    opts.AddFile(GetDir("Main.tea"));
    // opts.AddFile(GetDir("NotMain.tea"));
    
    return opts;
}
opts = GetDebugCompilerOptions();
#else
opts = TeaCompilerOptions.GetCompilerOptionsFromArgs(args)
#endif


TeaCompiler.SetCompilerOptions(opts);
var ret = TeaCompiler.Compile();

TeaCompiler.HandleCompileReturn(ret);