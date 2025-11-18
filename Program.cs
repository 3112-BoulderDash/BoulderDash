namespace Boulder_Dash___Final_Project;

class Program
{
    static void Main(string[] args)
    {
        GameField gameField = new GameField();
        gameField.StartGame();
        Controller myController = new Controller();
        int x = 1;
        int y = 3;
        while (true)
        {
            //Console.WriteLine(gameField.whereIsOne()[0] + ", " + gameField.whereIsOne()[1]);
            int[] location = gameField.whereIsOne();
            gameField.MoveInstanceTo(location[1], location[0], x, y);
            if (Controller.IsKeyDown(KeyCode.Right)) x++;
            if (Controller.IsKeyDown(KeyCode.Left)) x--;
            gameField.RunGame();
        }
    }
}