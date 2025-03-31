public class TeaNode : TeaValue {
    TeaNode? left;
    TeaNode? right;

    TeaNodeType type;
    TeaValue?   value;
    // id     -> TeaNode
    // num    -> TeaNumber
    // invoke -> TeaInvoke
    // str    -> TeaString
    // oper   -> TeaOper
    
    /// Constructor
    public TeaNode
    (
        TeaNodeType type  = TeaNodeType.None,
        TeaValue?   value = null,
        TeaNode?    left  = null,
        TeaNode?    right = null
    )
    {
        this.type  = type;
        this.left  = left;
        this.right = right;
        this.value = value;
    }

    // Builder Pattern
    #region Builder
    
    public TeaNode Left(TeaNode left){
        this.left = left;
        return this;
    }
    public TeaNode Value(TeaValue value) {
        this.value = value;
        return this;
    }
    public TeaNode Right(TeaNode right) {
        this.right = right;
        return this;
    }
    public TeaNode Type(TeaNodeType type) {
        this.type = type;
        return this;
    }

    #endregion

    // Value Setters
    #region Value

    public TeaNode Oper(TeaOper oper)
    => Value(oper).Type(TeaNodeType.Oper);

    public TeaNode Str(TeaString str)
    => Value(str).Type(TeaNodeType.Str);
    
    public TeaNode Num(TeaNumber num)
    => Value(num).Type(TeaNodeType.Num);
    
    public TeaNode Invoke(TeaInvoke invoke)
    => Value(invoke).Type(TeaNodeType.Invoke);

    #endregion

    // For `using static TeaNode`
    #region Static
    
    public static TeaNode New() => new TeaNode();
    
    public static TeaNode Id(TeaId value)
    => New().Type(TeaNodeType.Id).Value(value);
    
    public static TeaNode Oper(TeaNode left, TeaOper oper, TeaNode right)
    => New().Left(left).Oper(oper).Right(right);

    public static TeaNode String(TeaString str)
    => New().Str(str);

    public static TeaNode Number(TeaNumber num)
    => New().Num(num);

    public static TeaNode Invoke(TeaNode name, params TeaNode[] args)
    => New().Invoke(new TeaInvoke(name, args));




    // public static TeaNode VarValue(TeaNode type, TeaNode name, TeaNode value)
    // => Oper(Oper(type, TeaOper.DoubleDot, name), TeaOper.Assign, value);
    
    // public static TeaNode ValueEmpty(TeaNode type, TeaNode name)
    // => Oper(type, TeaOper.DoubleDot, name);

    // public static TeaNode GetMember(TeaNode left, TeaNode right)
    // => Oper(left, TeaOper.GetMember, right);

    #endregion

    string GetValueTea()
    => value is null ? string.Empty : value.ToTea();

    // this.value -> string
    string GetValueStr()
    => value is null ? string.Empty : value.ToStr();

    string GetValueIR()
    => value is null ? string.Empty : value.ToIR();

    public string
    ToStr()
    {
        string res;
        
        var left  = this.left  is null ? string.Empty : this.left.ToStr();
        var right = this.right is null ? string.Empty : this.right.ToStr();
        var val   = GetValueStr();
        res = $"[{left} {val} {right}]";

        return res;
    }
    
    public string
    ToTea()
    {
        string res;
        /*
        if (type is TeaNodeType.Invoke)
        {
            (TeaNode name, TeaNode[] args) = (ValueTuple<TeaNode, TeaNode[]>)value!;
            var strArgs = string.Join(", ", args.Select((arg) => arg.ToCode()));
            res = $"{name.ToCode()}({strArgs})";

            return res;
        }
        */

        var left  = this.left  is null ? string.Empty : this.left.ToTea();
        var right = this.right is null ? string.Empty : this.right.ToTea();
        var val   = GetValueTea();
        res = $"{left}{val}{right}";

        return res;
    }

    public string ToIR() {
        var left  = this.left  is null ? string.Empty : this.left.ToIR();
        var right = this.right is null ? string.Empty : this.right.ToIR();
        var val   = GetValueIR();
        return $"{left}{val}{right}";
    }
    
    public void WriteCode  (CodeWriter wr) => wr.Write(this.ToTea());
    public void WriteString(CodeWriter wr) => wr.Write(this.ToStr());
}