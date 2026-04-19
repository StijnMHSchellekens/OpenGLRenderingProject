using OpenTK.Mathematics;
using ThreeDeeRenderer.Rendering;

namespace ThreeDeeRenderer.World.Entities;

public class DemoTriangleEntity(RenderObject renderObject) : Entity(renderObject)
{

    private float rotation_axis = 0.0f;
    private float rotation_other_axis = 0.0f;

    public void animateRotation(float deltaTime)
    {
        rotation_axis += 45f * deltaTime;
        rotation_other_axis += 360f * deltaTime;
        transform.SetRotation(new Vector3(0.0f, rotation_other_axis %= 360, rotation_axis %= 360));
    }
}