namespace BoulderDash;

public interface IController
{
    void ReadInputs();
    bool LeftInput();
    bool RightInput();
    bool UpInput();
    bool DownInput();
}