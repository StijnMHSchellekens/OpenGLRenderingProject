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
        
        camera.initializeCamera(1920, 1080, 90f);
        
        Shader solidShader = _resourceManager.GetShader("PosColorTrans");
        Shader solidTransform = _resourceManager.GetShader("SolidTrans");
        Mesh triangle = _resourceManager.GetMesh("pyramid");
        Mesh square = _resourceManager.GetMesh("cube");
        
        RenderObject triangleObj = new RenderObject(triangle, solidShader);
        RenderObject cubeObj = new RenderObject(square, solidTransform);

        Random random = new Random();
        
        for (int i = 0; i < 15000; i++)
        {
            float randoX = (random.NextSingle() * (50f - -50f)) + -50f;
            float randoY = (random.NextSingle() * (50f - -50f)) + -50f;
            float randoZ = (random.NextSingle() * (-10 - -50f)) + -50f;
            
            DemoTriangleEntity entity = new DemoTriangleEntity(triangleObj, RenderPolicy.Auto);
            entity.transform.SetPosition(new Vector3(randoX, randoY, randoZ));
        
            scene.AddEntity(entity);
        }
        
        for (int i = 0; i < 1000; i++)
        {
            float randoX = (random.NextSingle() * (50f - -50f)) + -50f;
            float randoY = (random.NextSingle() * (50f - -50f)) + -50f;
            float randoZ = (random.NextSingle() * (-10 - -50f)) + -50f;
            
            Entity cube = new Entity(cubeObj);
            cube.transform.SetPosition(new Vector3(randoX, randoY, randoZ));
        
            scene.AddEntity(cube);
        }
        
        scene.AddCamera(camera);

        return scene;
    }
}