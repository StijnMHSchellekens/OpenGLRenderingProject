using ThreeDeeRenderer.Rendering;

namespace ThreeDeeRenderer.Scenes;

public class Scene
{
    private List<RenderObject> _objects = new(); // make dictionary with identifiers for each mesh????

    public void AddObject(Mesh mesh,  Shader shader)
    {
        RenderObject obj = new RenderObject(mesh, shader);
        _objects.Add(obj);
    }

    public virtual void Render()
    {
        foreach (var obj in _objects)
        {
            obj.Draw(); // Basic, draw all available objects. Should be extended to minimize VAO bindings before drawing (list ordering, only binding when needed).
        }
    }

    public void Unload()
    {
        _objects.Clear();
    }

    protected void DrawSingleObject(int index)
    {
        _objects[index].Draw();
    }
    
    protected int GetObjectCount()
    {
        return _objects.Count;
    }
}