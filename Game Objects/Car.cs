namespace  Boulder_Dash;

public class Car : TraversalEntity
{
    public int HealthPoints { get; set; }
    public CarTypes Type { get; set; }
    private Controller controller;

    public Car(int posX, int posY, CarTypes type, Controller controller) : base(posX, posY)
    {
        HealthPoints = 3;
        Type = type;
        this.controller = controller;
    }



    public void ScanInputs()
    {
        if (controller.IsKeyDown(KeyCode.Left))
            posX--;
                 
        if (controller.IsKeyDown(KeyCode.Right)) 
            posX++;
    }
    public override String DrawMe()
    {
        //throw new NotImplementedException();
        return "1";
    }
    public override void CollisionAction(TraversalEntity Tentity)
    {
        throw new NotImplementedException();
    }

    public override void Step()
    {
        //Does nothing
    }
}