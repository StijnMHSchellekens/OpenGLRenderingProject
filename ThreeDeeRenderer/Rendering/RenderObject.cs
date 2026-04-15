namespace ThreeDeeRenderer.Rendering;

public class RenderObject
{
    public Mesh Mesh { get; }
    public Shader Shader { get; }
    
    public RenderObject(Mesh mesh, Shader shader)
    {
        Mesh = mesh;
        Shader = shader;
    }
    
    public void Draw()
    {
        Shader.Use();
        Mesh.Draw();
    }
}