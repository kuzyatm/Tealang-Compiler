public static class TeaCompiler
{
    static TeaCompilerOptions? options;
    static TeaErrorStackTrace stack = new (string.Empty);

    public static void
    SetCompilerOptions
    (TeaCompilerOptions opts)
    => options = opts;

    public static void LogAfterProcessing(object value)
    {
        if (options == null)
            return;
        
        switch (options.CompilingStage)
        {
            case TeaCompilingStage.Tokenize:
            
            break;
            case TeaCompilingStage.Parse:
            
            break;
            case TeaCompilingStage.Analyze:
            
            break;
            case TeaCompilingStage.Optimize:
            
            break;
        }
    }


    // return:
    // null    -> no errors, ignore
    // nonnull -> log errors, stop compiling, delete already compiled
    public static TeaCompilerReturn Compile()
    {
        // INIT:
        var ret = new TeaCompilerReturn();

        // PROCESS:

        // Check options; null -> GetDefault();
        if (options is null) {
            options = TeaCompilerOptions.GetDefault();
        }

        // [[ process each file ]]
        foreach (var filePath in options.Files)
        {
            // FileDoesNotExist
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                ret.Error(TeaError.FileDoesNotExist(TeaErrorSource.NonFile, filePath));
                goto END;
            }

            stack.Reset(filePath);

            // Tokenize:
        #region Tokenizing
            var fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileReader = new FileReader(fileStream);

            var tokens
                = Tokenizer.GetTokens(fileReader.GetData);
            fileReader.Dispose();

            // DebugLogTokens(tokens);
            // continue;
            
            if (options.HaveToQuitOnTokenize())
            {
                continue;
            }
        #endregion


            // Parse:
            var prog = Parser.Parse(
                Path.GetRelativePath
                    (Environment.CurrentDirectory, fileInfo.FullName).Replace('\\', '/'),
                tokens, ret);
        }
        
        
        // END:
        END:
        return ret;
    }

    private static void DebugLogTokens(List<Token> tokens)
    {
        foreach (var (type, value) in tokens)
        {
            Console.WriteLine(
                value is null
                ?
                $"{type}":
                $"{type} : \'{value}\'"
            );
        }
    }


    // Here we handle return of Compiling
    public static void HandleCompileReturn(TeaCompilerReturn ret) {
        if (ret.HasErrors())
        {
            ret.errors.ForEach((err) =>
            {
                LogError(err, null!);
            });
        }
    }

    public static void LogError(TeaError err, TeaErrorStackTrace stack) {
        Console.WriteLine(err.ToString());
    }
}