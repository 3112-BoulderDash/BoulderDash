namespace Boulder_Dash;

public class WheelSkin : ISkin
{
    public string name { get; } = "Wheel";
    public int price { get; } = 1000;
    public string[] Sprite => new[]
    {
        "     ",
        " [ ] ",
        "     "
    };
}