using static TeaNode;
public class ASTBuilder {
    TeaNode node;
    TeaNode root;

    public ASTBuilder() {
        root = New();
        node = root;
    }
    public void Reset()
    {
        root = New();
        node = root;
    }
    public void AddOper(TeaOperType oper)
    {
        if (oper is TeaOperType.None)
            return;
        
        node.Oper(new (oper));
        var nodeNew = New();
        node.Right(nodeNew);

        node = nodeNew;
    }
    public void AddId(string id) {
        var nodeNew = Id(new (id));
        node.Left(nodeNew);
    }

    public void AddNumber(TeaNumber num) {
        var nodeNew = Number(num);
        node.Left(nodeNew);
    }
    public void AddStr(string str) {
        var nodeNew = String(new TeaString(str));
        node.Left(nodeNew);
    }

    public void DebugLog() {
        root.WriteCode(CodeWriter.Console);
        root.WriteString(CodeWriter.Console);
    }
}