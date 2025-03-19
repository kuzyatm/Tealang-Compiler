public class TeaAssembly
{
    
    public TeaAssembly
    ()
    {
        
    }

    public void Reset()
    {
        
    }

    // public Temp
}
public abstract class TeaChildObject {

    public AccessModifierType access;
    public TeaChildObjectType type;


    public TeaChildObject
    (TeaChildObjectType type,
     AccessModifierType access)
    {
        this.type = type;
        this.access = access;
    }
}

public class TeaType : TeaChildObject {
    
    public TeaType
    (AccessModifierType access, TeaTypeId typeId)
    : base (TeaChildObjectType.Type, access)
    {

    }
}
/*
public class TeaMethod : TeaChildObject {
    
    public TeaMethod(AccessModifierType access)
    : base (TeaChildObjectType.Method, access)
    {

    }
}
*/

public class TeaFunc : TeaChildObject {
    public TeaFunc(AccessModifierType access)
    : base (TeaChildObjectType.Func, access)
    {

    }
}

public enum TeaTypeId
{

}

public enum TeaChildObjectType
{
    Type,
    Method,
    Func,
}