public class TeaMethod : TeaNode {
    string objType;
    // TeaScope scope;
    TeaFnBody body;
    string returnType;

    public TeaMethod(string id, string objType, TeaAccessMod mod, string returnType, TeaFnBody body)
    : base(id, mod)
    {
        this.objType = objType;
        this.body = body;
        this.returnType = returnType;
    }

    public override void WriteCodeBegin(CodeWriter wr)
    {
        string ret = returnType == string.Empty ? returnType : $"-> {returnType}";
        wr.Write($"{base.id}() {ret} {{}}");
    }

    public override void WriteCodeEnd(CodeWriter wr)
    {
        
    }
}