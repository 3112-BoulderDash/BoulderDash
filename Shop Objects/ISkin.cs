namespace Boulder_Dash;

public interface ISkin : IItem
{
    public string name {get;}
    public int price  {get;}
    public string[] Sprite { get; }
}