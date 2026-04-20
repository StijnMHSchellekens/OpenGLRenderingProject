using OpenTK.Windowing.GraphicsLibraryFramework;
using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;
using ThreeDeeRenderer.Rendering.Shaders;

namespace ThreeDeeRenderer.World;

public class Scene
{
    private List<RenderObject> _objects = new(); // make dictionary with identifiers for each mesh????
    private List<Entity> _entities = new();

    private Camera _camera;
    
    public void AddObject(Mesh mesh,  Shader shader)
    {
        RenderObject obj = new RenderObject(mesh, shader);
        _objects.Add(obj);
        //_objects.Add(obj);
    }

    public void AddEntity(Entity entity)
    {
        _entities.Add(entity);
    }

    public void AddCamera(Camera camera)
    {
        _camera = camera;
    }
    
    public virtual void Render()
    {
        foreach (var entity in _entities)
        {
            entity.Draw(_camera); // Basic, draw all available entities. Should be extended to minimize VAO bindings before drawing (list ordering, only binding when needed).
        }
    }

    public virtual void Update(KeyboardState keyboardState, float deltaTime){} // Override for subclasses

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

    protected List<Entity> GetEntities()
    {
        return _entities;
    }
}