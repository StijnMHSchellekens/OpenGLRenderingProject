using ThreeDeeRenderer.Rendering.Objects;
using ThreeDeeRenderer.Rendering.Shaders;
using ThreeDeeRenderer.World;

namespace ThreeDeeRenderer.Rendering;

public class RenderDispatcher
{
    // TODO: Scene calls RenderDispatcher with a list of (active) objects and camera. RenderDispatcher groups based on render intent (Instanced/Individual) and Draws meshes according to said intent

    private Boolean outDated = true;

    private Dictionary<(RenderPolicy, Mesh, Shader), List<Entity>> _renderGroups = new();
    
    public void Dispatch(List<Entity> entities, Camera camera)
    {
        if (outDated)
        {
            Update(entities);
        }
    }

    private void Update(List<Entity> _entities)
    {
        foreach (Entity entity in _entities)
        {
            if (!_renderGroups.ContainsKey(
                    (entity.RenderPolicy, entity._renderObject.Mesh, entity._renderObject.Shader)))
            {
                Console.WriteLine($"Creating new render group for {entity.RenderPolicy} {entity._renderObject.Mesh} {entity._renderObject.Shader}");
                _renderGroups[(entity.RenderPolicy, entity._renderObject.Mesh, entity._renderObject.Shader)] = new List<Entity>();
            }
            _renderGroups[(entity.RenderPolicy, entity._renderObject.Mesh, entity._renderObject.Shader)].Add(entity);
        }
        
        outDated = false;

        Console.WriteLine($"Created {_renderGroups.Count} render groups");
        for (int i = 0; i < _renderGroups.Count; i++)
        {
            Console.WriteLine($"Render group {i}: {_renderGroups.ElementAt(i).Value.Count} entities");
        }
    }
}