using BoulderDash;

namespace Boulder_Dash___Final_Project.Game_Objects;

public class TraversalController
{   //controlls the traversal entity
    public void Traverse(TraversalEntity entity)
    {
        if (Controller.IsKeyDown(KeyCode.Left))
            entity.RequestMoveLeft();
        
        if (Controller.IsKeyDown(KeyCode.Right))
            entity.RequestMoveRight();
        
        if (Controller.IsKeyDown(KeyCode.Down))
            entity.RequestMoveDown();
    }
}