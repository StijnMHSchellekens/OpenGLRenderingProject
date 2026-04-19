using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Rendering.Objects;
using ThreeDeeRenderer.Rendering.Shaders;
using ThreeDeeRenderer.World.Entities;
using ThreeDeeRenderer.World.Scenes.Demos;

namespace ThreeDeeRenderer.World;

public class SceneFactory(ResourceManager resourceManager)
{
    ResourceManager _resourceManager = resourceManager;
    
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
        
        Shader solidShader = _resourceManager.GetShader("PosColorTrans");
        Mesh triangle = _resourceManager.GetMesh("test_triangle");
        
        RenderObject renderObject = new RenderObject(triangle, solidShader);
        DemoTriangleEntity entity = new DemoTriangleEntity(renderObject);
        
        scene.AddEntity(entity);

        return scene;
    }
}