namespace  Boulder_Dash;

/// Codes representing keyboard keys.
/// Key code documentation:
/// http://msdn.microsoft.com/en-us/library/dd375731%28v=VS.85%29.aspx
public enum KeyCode : int
{
    //Left key
    Left = 0x41,

    //Up key
    Up = 0x57,

    //Right key
    Right = 0x44,

    //Down key
    Down = 0x53
}

public class Controller
{
    /// A positional bit flag indicating the part of a key state denoting
    /// key pressed.
    private const int KeyPressed = 0x8000;
    //Input flags to ensure input is only checked once and not every frame
    private static Dictionary<KeyCode, int> inputFlags = new Dictionary<KeyCode, int>(); 

    
    /// Returns a value indicating if a given key is pressed.
    /// <param name="key">The key to check.</param>
    /// <returns>
    /// <c>true</c> if the key is pressed, otherwise <c>false</c>.
    /// </returns>
    public bool IsKeyDown(KeyCode key)
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