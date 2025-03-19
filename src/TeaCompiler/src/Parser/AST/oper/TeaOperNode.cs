public class TeaOperNode {
    TeaOperNodeType type;

    TeaOperNode? left;
    TeaOper oper;
    TeaOperNode? right;

    object? value; // const | id -> string,  -> string,
    
    public TeaOperNode(TeaOperNodeType type = TeaOperNodeType.Oper, TeaOperNode? left = null, TeaOper oper = TeaOper.None, TeaOperNode? right = null) {
        this.type  = type;
        this.oper  = oper;
        this.left  = left;
        this.right = right;
    }
    
    public TeaOperNode Left(TeaOperNode left){
        this.left = left;
        return this;
    }
    public TeaOperNode Value(object value) {
        this.value = value;
        return this;
    }
    public TeaOperNode Right(TeaOperNode right) {
        this.right = right;
        return this;
    }
    public TeaOperNode Type(TeaOperNodeType type)
    {
        this.type = type;
        return this;
    }
    public TeaOperNode Oper(TeaOper oper) {
        this.oper = oper;
        return this;
    }
    
    public static TeaOperNode New() => new TeaOperNode();
    public static TeaOperNode Id(object value)
    {
        return New().Type(TeaOperNodeType.Id).Value(value);
    }
    public static TeaOperNode Const(object value)
    {
        return New().Type(TeaOperNodeType.Const).Value(value);
    }
    public static TeaOperNode Oper(TeaOperNode left, TeaOper oper, TeaOperNode right)
    {
        return New().Left(left).Oper(oper).Right(right);
    }
    public static TeaOperNode Invoke(TeaOperNode name, params TeaOperNode[] args) {
        return New().Type(TeaOperNodeType.Invoke).Value((name, args));
    }

    public static TeaOperNode VarValue(TeaOperNode type, TeaOperNode name, TeaOperNode value) {
        return Oper(Oper(type, TeaOper.DoubleDot, name), TeaOper.Assign, value);
    }
    public static TeaOperNode ValueEmpty(TeaOperNode type, TeaOperNode name) {
        return Oper(type, TeaOper.DoubleDot, name);
    }
    public static TeaOperNode GetMember(TeaOperNode left, TeaOperNode right) {
        return Oper(left, TeaOper.GetMember, right);
    }



    public override string ToString()
    {
        string res;
        if (type is TeaOperNodeType.Invoke)
        {
            (TeaOperNode name, TeaOperNode[] args) = (ValueTuple<TeaOperNode, TeaOperNode[]>)value!;
            var strArgs = string.Join(", ", args.Select((arg) => arg.ToString()));
            res = $"{name} invoke ({strArgs})";

            return res;
        }

        if (value is null) {
            var left  = this.left!.ToString();
            var right = this.right!.ToString();
            var oper = Extender.ToString(this.oper);
            res = $"[{left} {oper} {right}]";
        } else {
            res = $"{(value is string ? value : value.ToString())}";
        }

        return res;
    }
    public string ToCode()
    {
        string res;
        if (type is TeaOperNodeType.Invoke)
        {
            (TeaOperNode name, TeaOperNode[] args) = (ValueTuple<TeaOperNode, TeaOperNode[]>)value!;
            var strArgs = string.Join(", ", args.Select((arg) => arg.ToCode()));
            res = $"{name.ToCode()}({strArgs})";

            return res;
        }

        if (value is null) {
            var left  = this.left!.ToCode();
            var right = this.right!.ToCode();
            var oper = Extender.ToStringForCode(this.oper);
            res = $"{left}{oper}{right}";
        } else {
            res = $"{(value is string ? value : value.ToString())}";
        }

        return res;
    }
    
    public void WriteCode(CodeWriter wr) => wr.Write(this.ToCode());
    public void WriteString(CodeWriter wr) => wr.Write(this.ToString());
}

public enum TeaOperNodeType {
    Oper,
    Const,
    Id,
    Invoke,
}