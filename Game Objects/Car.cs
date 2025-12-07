namespace  Boulder_Dash;

public class Car : TraversalEntity
{
    public int HealthPoints { get; set; }
    public CarTypes Type { get; set; }
    private Controller controller;
    public ISkin Skin { get; set; }

    public Car(int posX, int posY, CarTypes type, Controller controller) : base(posX, posY)
    {
        HealthPoints = 3;
        Type = type;
        this.controller = controller;
        // sets skin to default
        Skin = new DefaultSkin();
    }



    public void ScanInputs()
    {
        if (controller.IsKeyDown(KeyCode.Left))
            posX--;
                 
        if (controller.IsKeyDown(KeyCode.Right)) 
            posX++;
    }

    public override String[] GetSprite()
    {
        //returns currently equipped skin
        return Skin.Sprite;
    }
    
    //Equips new Skin
    public void EquipSkin(ISkin newSkin)
    {
        Skin = newSkin;
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