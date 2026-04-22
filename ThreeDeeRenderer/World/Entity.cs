using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;

namespace ThreeDeeRenderer.World;

public enum RenderPolicy
{
    Auto, // Let the RenderDispatcher decide
    Instanced, // Always Instanced rendering (requires amount of objects to be known)
    SingleDraw // Always Single Draw per Entity Spawn (For special entities/entities requiring high interaction. I.E., Player, Special Chests, Certain Interactable NPCs)
}

public class Entity
{
    public RenderObject _renderObject { get; }
    public Transform transform { get; } = new Transform();
    
    public RenderPolicy RenderPolicy { get; }

    public Entity(RenderObject renderObject, RenderPolicy renderPolicy = RenderPolicy.Auto)
    {
        _renderObject = renderObject;
        RenderPolicy = renderPolicy;
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