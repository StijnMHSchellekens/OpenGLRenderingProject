using OpenTK.Windowing.GraphicsLibraryFramework;
using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;
using ThreeDeeRenderer.Rendering.Shaders;

namespace ThreeDeeRenderer.World;

public class Scene
{
    private List<RenderObject> _objects = new(); // make dictionary with identifiers for each mesh????
    private List<Entity> _entities = new();
    
    private RenderDispatcher _renderDispatcher = new RenderDispatcher();

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
        // TODO: Work on separating entities based on required Rendering Logic (I.E Instanced/Special (single Draw per Entity).
        _renderDispatcher.Dispatch(_entities, _camera);
        
        // TODO: Work on adding logic for instanced rendering.
        foreach (var entity in _entities)
        {
            entity.Draw(_camera); // Basic, draw all available entities.
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