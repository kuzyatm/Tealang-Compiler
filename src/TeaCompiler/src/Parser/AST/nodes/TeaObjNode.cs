/*
public class TeaObjNode : TeaNode {
    TeaObjType type;
    TeaScope scope;
    
    public TeaObjNode(string id, TeaObjType type, TeaScope scope, TeaAccessMod mod, List<TeaNode> children)
    : base(id, mod, children)
    {
        this.type = type;
        this.scope = scope;
    }
    
    public TeaObjNode(string id, TeaObjType type, TeaAccessMod mod)
    : this(id, type, new TeaScope(), mod, new ())
    {

    }

    public void Scope(TeaScope scope)
    {
        this.scope = scope;
    }

    public override void WriteCodeBegin(CodeWriter wr)
    {
        var access = mod.ToString().ToLower();
        var type = this.type.ToString().ToLower();
        wr.Write($"{access} {type} {base.id} {scope.ToCode()} {{");
    }

    public override void WriteCodeEnd(CodeWriter wr)
    {
        wr.Write("}");
    }

    public TeaObjNode Clone()
    {
        return new TeaObjNode(id, type, scope, mod, children);
    }
}

public enum TeaObjType {
    None,
    Struct,
    Class,
    Enum
}
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
*/