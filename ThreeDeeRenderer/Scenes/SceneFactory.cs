using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Scenes.Demos;

namespace ThreeDeeRenderer.Scenes;

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
        
        Shader solidShader = _resourceManager.GetShader("Solid");
        Mesh triangle = _resourceManager.GetMesh("solidtriangle");
        
        scene.AddObject(triangle, solidShader);

        return scene;
    }
}