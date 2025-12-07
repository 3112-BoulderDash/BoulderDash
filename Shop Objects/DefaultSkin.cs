namespace Boulder_Dash;

public class DefaultSkin : ISkin
{
    public string name { get; } = "Default Skin";
    public int price { get; } = 0;
    public string[] Sprite => new[]
    {
        "[*=*]",
        " | | ",
        "[|_|]"
    };
}