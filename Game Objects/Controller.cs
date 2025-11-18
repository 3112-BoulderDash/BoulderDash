namespace Boulder_Dash___Final_Project;



/// <summary>
/// Codes representing keyboard keys.
/// </summary>
/// <remarks>
/// Key code documentation:
/// http://msdn.microsoft.com/en-us/library/dd375731%28v=VS.85%29.aspx
/// </remarks>
public enum KeyCode : int
{
    /// <summary>
    /// The left arrow key.
    /// </summary>
    Left = 0x41,

    /// <summary>
    /// The up arrow key.
    /// </summary>
    Up = 0x57,

    /// <summary>
    /// The right arrow key.
    /// </summary>
    Right = 0x44,

    /// <summary>
    /// The down arrow key.
    /// </summary>
    Down = 0x53
}

public class Controller
{
    /// A positional bit flag indicating the part of a key state denoting
    /// key pressed.
    private const int KeyPressed = 0x8000;
    //Input flags to ensure input is only checked once and not every frame
    private static Dictionary<KeyCode, int> inputFlags = new Dictionary<KeyCode, int>(); 

    /// <summary>
    /// Returns a value indicating if a given key is pressed.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <c>true</c> if the key is pressed, otherwise <c>false</c>.
    /// </returns>
    public static bool IsKeyDown(KeyCode key)
    {
        if ((GetKeyState((int)key) & KeyPressed) != 0)
        {
            if (inputFlags[key] == 0)
            {
                inputFlags[key] = 1;
                return true;
            }
        }
        else
        {
            inputFlags[key] = 0;
        }
        
        return false;

    }

    /// <summary>
    /// Gets the key state of a key.
    /// </summary>
    /// <param name="key">Virtuak-key code for key.</param>
    /// <returns>The state of the key.</returns>
    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern short GetKeyState(int key);
}