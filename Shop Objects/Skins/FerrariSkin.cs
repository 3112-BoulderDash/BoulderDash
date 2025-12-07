namespace Boulder_Dash;

public class FerrariSkin : ISkin
{
    public string name { get; } = "Ferrari";
    public int price { get; } = 5000;
    public string[] Sprite => new[]
    {
        "[*F*]",
        " | | ",
        "[|F|]"
    };
}