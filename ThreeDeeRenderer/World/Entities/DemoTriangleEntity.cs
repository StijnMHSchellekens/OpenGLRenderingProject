using OpenTK.Mathematics;
using ThreeDeeRenderer.Rendering;

namespace ThreeDeeRenderer.World.Entities;

public class DemoTriangleEntity(RenderObject renderObject, RenderPolicy renderPolicy = RenderPolicy.Auto) : Entity(renderObject, renderPolicy)
{

    private float rotation_axis = 0.0f;
    private float position_axis = 0.0f;
    private float timer = 0.0f;
    
    private float origin_y = 0.0f;

    public void animateRotation(float deltaTime)
    {
        if (origin_y == 0.0f)
        {
            origin_y = transform.GetPosition().Y;
        }
        
        rotation_axis += 200 * deltaTime;
        rotation_axis %= 360;
        
        timer += deltaTime;

        position_axis += 0.01f;
        
        position_axis = 0 + Oscillate(timer, 2f) * 1;
        
        //Console.WriteLine(position_axis);
        
        transform.SetRotation(new Vector3(transform.GetRotation().X, rotation_axis, transform.GetRotation().Z));
        transform.SetPosition(new Vector3(transform.GetPosition().X, position_axis + origin_y, transform.GetPosition().Z));
    }
    
    float Oscillate(float time, float speed)
    {
        return MathF.Cos(time * speed);
    }

}