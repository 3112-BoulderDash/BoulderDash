using System.Security.Cryptography;

namespace Boulder_Dash;

public class ObstacleSpawner
{

    private int obstacleWaitTime = 2;

    private int obstacleTimer = 2;
    //Creates Obstacles in a random spot. of 1 or 3
    public void RequestObstacles()
    {
        GameField gameField = GameField.GetGameInstance();
        //Pick a random spot to have free, fill the rest with filled spots 
        if (obstacleTimer > 0) obstacleTimer--;
        else
        {
            obstacleTimer = obstacleWaitTime;
            Random rand = new Random();
            int freeSpace = rand.Next(0, gameField.rowLength);
            //For loop and make new obstacles, UNLESS we hit the free space then skip
            for (int i = 0; i < gameField.rowLength; i++)
            {
                if (i == freeSpace) continue;
                gameField.AddInstance(new Obstacles(i, 0));
            }
        }

    }
}