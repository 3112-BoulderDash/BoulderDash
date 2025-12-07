namespace Boulder_Dash;

public class WheelSkin : ISkin
{
    public string name { get; } = "Wheel";
    public int price { get; } = 100;
    public string[] Sprite => new[]
    {
        "     ",
        " [ ] ",
        "     "
    };
}