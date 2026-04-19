using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ThreeDeeRenderer.World.Entities;

namespace ThreeDeeRenderer.World.Scenes.Demos;

public class TestScene : Scene
{
    public override void Update(KeyboardState keyboardState, float deltaTime)
    {
        base.Update(keyboardState, deltaTime);
        //Console.WriteLine(deltaTime);
        foreach (Entity entity in GetEntities())
        {
            entity.update();
            if (entity is DemoTriangleEntity triangleEntity)
            {
                triangleEntity.animateRotation(deltaTime);
            }
            //Console.WriteLine(entity.transform.GetRotation());
        }
    }
}