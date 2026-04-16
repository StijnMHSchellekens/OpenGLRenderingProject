using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.Scenes;
using ThreeDeeRenderer.Scenes.Demos;

namespace ThreeDeeRenderer;

public class Game : GameWindow
{
    float[] _squareVertices = {
        0.5f,  0.5f, 0.0f, // top right - 0
        0.5f, -0.5f, 0.0f, // bottom right - 1
        -0.5f, -0.5f, 0.0f, // bottom left - 2
        -0.5f,  0.5f, 0.0f // top left - 3
    };

    private uint[] _indices = {
        0, 1, 3,
        1, 2, 3
    };

    private float[] _triangleVertices =
    {
        -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, //Bottom-left vertex
        0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 0.0f, //Bottom-right vertex
        0.0f,  0.5f, 0.0f, 1.0f, 0.0f, 1.0f, //Top vertex
    };
    
    private ResourceManager _resourceManager;
    private SceneFactory _sceneFactory;
    
    private Scene _currentScene;
    
    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() 
        { ClientSize = (width, height), Title = title }) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        _resourceManager = new ResourceManager();
        _sceneFactory = new SceneFactory(_resourceManager);

        _currentScene = _sceneFactory.CreateDemoScene();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        try
        {
            base.OnRenderFrame(args);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            _currentScene.Render();
        
            SwapBuffers();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        
        if (KeyboardState.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        if (KeyboardState.IsKeyReleased(Keys.Right))
        {
            if (_currentScene is DemoScene demoScene)
            {
                demoScene.NextObject(); // cycle through meshes.
            }
        }

        if (KeyboardState.IsKeyReleased(Keys.Left))
        {
            if (_currentScene is DemoScene demoScene)
            {
                demoScene.PreviousObject(); // cycle the other way through meshes.
            } 
        }
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        
        _currentScene.Unload();
    }
}