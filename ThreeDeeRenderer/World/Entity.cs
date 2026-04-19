namespace ThreeDeeRenderer.Rendering;

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