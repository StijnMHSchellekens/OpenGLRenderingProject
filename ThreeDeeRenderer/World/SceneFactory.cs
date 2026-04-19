using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;
using ThreeDeeRenderer.Rendering.Shaders;
using ThreeDeeRenderer.World.Scenes.Demos;

namespace ThreeDeeRenderer.World;

public class SceneFactory
{
    ResourceManager _resourceManager;
    
    public SceneFactory(ResourceManager resourceManager)
    {
        _resourceManager = resourceManager;
    }
    
    public DemoScene CreateDemoScene()
    {
        DemoScene scene = new DemoScene();

        Shader shader = _resourceManager.GetShader("PosColor");
        Shader solidShader = _resourceManager.GetShader("Solid");
        
        Mesh square = _resourceManager.GetMesh("Square");
        Mesh triangle = _resourceManager.GetMesh("Triangle");
        
        scene.AddObject(triangle, shader);
        scene.AddObject(square, solidShader);
        
        return scene;
    }

    public TestScene CreateTestScene()
    {
        TestScene scene = new TestScene();
        
        Shader solidShader = _resourceManager.GetShader("PosColor");
        Mesh triangle = _resourceManager.GetMesh("test_triangle");
        
        scene.AddObject(triangle, solidShader);

        return scene;
    }
}