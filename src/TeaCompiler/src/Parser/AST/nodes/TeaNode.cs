public abstract class TeaNode {
    protected TeaAccessMod mod;
    protected string id;
    protected List<TeaNode> children;

    public TeaNode(string id, TeaAccessMod mod, List<TeaNode> children) {
        this.id = id;
        this.mod = mod;
        this.children = children;
    }

    public TeaNode(string id, TeaAccessMod mod)
    : this(id, mod, new ())
    {
        
    }

    public TeaNode(string id)
    : this(id, TeaAccessMod.Priv, new ())
    {
        
    }

    public void Add(TeaNode node) => children.Add(node);

    public void WriteAllCode(CodeWriter wr)
    {
        
        this.WriteCodeBegin(wr);
        wr.IncrDepth();
        foreach (var node in children) {
            node.WriteAllCode(wr);
        }
        wr.DecrDepth();
        this.WriteCodeEnd(wr);
    }

    public abstract void WriteCodeBegin(CodeWriter wr);
    public abstract void WriteCodeEnd(CodeWriter wr);
}

public class TeaNodeType {

}