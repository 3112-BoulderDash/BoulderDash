namespace Boulder_Dash;

public class DefaultSkin : ISkin
{
    public string name { get; } = "Old beatup";
    public int price { get; } = 0;
    public string[] Sprite => new[]
    {
        "[*=*]",
        " | | ",
        "[|_|]"
    };
}