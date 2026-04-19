using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;

namespace ThreeDeeRenderer.World;

public class Entity
{
    private RenderObject _renderObject;
    public Transform transform { get; } = new Transform();

    public Entity(RenderObject renderObject)
    {
        _renderObject = renderObject;
    }

    public virtual void update(){}

    public virtual void Draw()
    {
        Matrix4 model = transform.GetMatrix();
        int modelLocation = GL.GetUniformLocation(_renderObject.Shader.Handle, "model");
        GL.UniformMatrix4(modelLocation, false, ref model);
        _renderObject.Draw();
    }
}