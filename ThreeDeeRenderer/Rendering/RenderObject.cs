namespace ThreeDeeRenderer.Rendering;

public class RenderObject(Mesh mesh, Shader shader)
{
    public Mesh Mesh { get; } = mesh;
    public Shader Shader { get; } = shader;

    public void Draw()
    {
        Shader.Use();
        Mesh.Draw();
    }
}