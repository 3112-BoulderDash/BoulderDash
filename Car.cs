namespace BoulderDash;

public class Car : TraversalEntity
{
    public int HealthPoints { get; set; }
    public CarTypes Type { get; set; }
    private IController Controller;

    public Car(int posX, int posY, CarTypes type, IController controller) : base(posX, posY)
    {
        Type = type;
        this.controller = controller;
    }
    
    public override String DrawMe()
    {
        
    }

    public void Move(Controller controller)
    {
        controller.ReadInputs();
        
        if (controller.LeftInput())
            MoveLeft();

        if (controller.RightInput())
            MoveRight();
    }
}