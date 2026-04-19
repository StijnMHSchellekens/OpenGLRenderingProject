using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;

namespace ThreeDeeRenderer.World;

public class Entity
{
    private RenderObject _renderObject;
    private Transform _transform;
    
    public Entity(RenderObject renderObject, Transform transform)
    {
        _renderObject = renderObject;
        _transform = transform;
    }
}