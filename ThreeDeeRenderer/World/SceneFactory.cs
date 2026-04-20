using OpenTK.Mathematics;
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
        Camera camera = new Camera();
        
        Shader solidShader = _resourceManager.GetShader("PosColorTrans");
        Mesh triangle = _resourceManager.GetMesh("pyramid");
        
        RenderObject renderObject = new RenderObject(triangle, solidShader);
        DemoTriangleEntity entity = new DemoTriangleEntity(renderObject);
        
        camera.initializeCamera(1920, 1080, 60f);
        
        entity.transform.SetPosition(new Vector3(0.0f, -0.5f, -5.0f));
        
        scene.AddEntity(entity);
        scene.AddCamera(camera);

        return scene;
    }
}