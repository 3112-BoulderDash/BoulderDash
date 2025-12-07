namespace Boulder_Dash;

public class FerrariSkin : ISkin
{
    public string name { get; } = "Ferrari";
    public int price { get; } = 300;
    public string[] Sprite => new[]
    {
        "0F0",
        "| |",
        "=F="
    };
}