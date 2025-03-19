public class TeaStatement {
    // TeaStatementType type;

    public TeaStatement(TeaNode node) {

    }

    public string ToCode() {
        return $"##";
    }
}
/*
public class TeaOperNode
: TeaNode
{
     

    public static TeaOperNode
    Invoke(string? member, string func)
    => new TeaOperNode();
}

public enum TeaStatementType {
    Invoke,
    Operator
}

/*
Func()
null - Func

myclass.MyMethod()
myclass - MyMethod

myvalue += 20 + Invoke()
myvalue
    +=
    20
        +
    Invoke(2)

value1 (Var : Type)
+=
    value2 (Const(ValueType) : Int32(string) : 20(string))
    +
    value3 (Invoke Result : Type : ())


public class TeaValueNode : TeaNode {
    TeaUndType undType;    //
    string typeId;         // String, SInt32, uint, int, char, MyClass.MyCustom 
    TeaValueNode values;   // Invoke -> array, Other -> 1

    public TeaValueNode(TeaUndType undType, string typeId) {

    }
}
public class TeaConstValueNode : TeaValueNode {
    
}

public class TeaInvokeValueNode : TeaValueNode {
    
}

public enum TeaUndType {
    Const,  // Literal or Number  :  32.0f, "hello", 'd', 54 
    Invoke, //
    Var,    // int: x;  :  x
}
*/