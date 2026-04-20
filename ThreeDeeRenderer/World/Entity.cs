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

    public virtual void Draw(Camera camera = null)
    {
        Matrix4 model = transform.GetMatrix();
        Matrix4 view = camera?.GetViewMatrix() ?? Matrix4.Identity;
        Matrix4 projection = camera?.GetProjectionMatrix() ?? Matrix4.Identity;
        
        int modelLocation = GL.GetUniformLocation(_renderObject.Shader.Handle, "model");
        int viewLocation = GL.GetUniformLocation(_renderObject.Shader.Handle, "view");
        int projectionLocation = GL.GetUniformLocation(_renderObject.Shader.Handle, "projection");
        
        GL.UniformMatrix4(modelLocation, false, ref model);
        GL.UniformMatrix4(viewLocation, false, ref view);
        GL.UniformMatrix4(projectionLocation, false, ref projection);
        _renderObject.Draw();
    }
}