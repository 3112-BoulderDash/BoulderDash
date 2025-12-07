namespace   Boulder_Dash;

public class Clock
{
    private int gameTime = 0;
    public int TickSpeed { get; set; } //In milliseconds
    private int clockCounter = 0;

    public Clock(int tickSpeed = 60)
    {
        TickSpeed = tickSpeed;
    }

    public void passTime()
    {
        Task.Delay(1).Wait();
        clockCounter++;

        if (clockCounter == TickSpeed)
        {
            gameTime++;
        }
        
    }

    public bool hasTickOccured()
    {
        //Console.WriteLine(clockCounter);
        if (clockCounter == TickSpeed)
        {
            //Console.WriteLine("tick occured");
            clockCounter = 0;
            return true;
        }
        return false;
    }
}