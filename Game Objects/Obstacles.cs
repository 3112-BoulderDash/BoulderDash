namespace  Boulder_Dash;

public class Obstacles : TraversalEntity 
{
    public ObstacleShape Shape { get; set; }
    public Obstacles(int posX, ObstacleShape shape) : base(posX, 0)
    {
        Shape = shape;
    }

    public override void CollisionAction(TraversalEntity TEntity)
    {
        
    }

    public void DropObject(TraversalEntity Obstacle)
    {
        MoveDown();
    }
    public override String DrawMe()
    {
      //  throw new NotImplementedException();
      return "2";
    }

    public override void ScheduleUpdate()
    {
        throw new NotImplementedException();
    }
}