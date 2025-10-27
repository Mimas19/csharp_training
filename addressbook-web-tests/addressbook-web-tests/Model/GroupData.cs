namespace addressbook_web_tests;

public class GroupData : IEquatable<GroupData>
{
    private string name;
    private string header = "";
    private string footer = "";

    public bool Equals(GroupData other)
    {
        if (Object.ReferenceEquals(other, null))
        {
            return false;
        }

        if (Object.ReferenceEquals(this, other))
        {
            return true;
        }
        return Name == other.Name;
    }

    public int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public GroupData(string name)
    {
        this.name = name;
    }
    public GroupData(string name, string header, string footer)
    {
        this.name = name;
        this.header = header;
        this.footer = footer;
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public string Header
    {
        get
        {
            return header;
        }
        set
        {
            header = value;
        }
    }

    public string Footer
    {
        get
        {
            return footer;
        }
        set
        {
            footer = value;
        }
    }
}