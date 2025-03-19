using System.Text;
/*
public abstract class TeaStatement
{
	private TeaStatementType type;
	private List<TeaStatement> children;

	public TeaStatement(TeaStatementType type, List<TeaStatement> children)
	{
		this.type = type;
		this.children = children;
	}

	public TeaStatement()
		: this(TeaStatementType.None, new List<TeaStatement>())
	{
	}

	public TeaStatement(TeaStatementType type)
		: this(type, new List<TeaStatement>())
	{
	}

	public void AddChild(TeaStatement st)
	{
		children.Add(st);
	}

	public List<TeaStatement> GetChildren()
	{
		return children;
	}

	public abstract void AddSelfCode(StringBuilder builder);

	public string GetCode(StringBuilder builder)
	{
		AddChildrenCode(builder);
		string code = builder.ToString();
		builder.Clear();
		return code;
	}

	private void AddChildrenCode(StringBuilder b)
	{
		foreach (TeaStatement child in children)
		{
			child.AddSelfCode(b);
		}
	}
}
*/