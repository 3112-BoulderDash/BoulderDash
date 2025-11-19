namespace BoulderDash;

public class Car : TraversalEntity
{
    public int HealthPoints { get; set; }
    public CarTypes Type { get; set; }
    private IController Controller;

    public Car(int posX, int posY, CarTypes type, IController controller) : base(posX, posY)
    {
        Type = type;
        this.Controller = controller;
    }
    
    public override String DrawMe()
    {
        return "To be developed";
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