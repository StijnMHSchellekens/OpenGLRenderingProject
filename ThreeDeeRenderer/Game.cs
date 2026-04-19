using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ThreeDeeRenderer.Rendering;
using ThreeDeeRenderer.World;

namespace ThreeDeeRenderer;

public class Game : GameWindow
{
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

        _currentScene = _sceneFactory.CreateTestScene();
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
        if (KeyboardState.IsKeyPressed(Keys.Escape))
        {
            Close();
        }
        
        _currentScene.Update(KeyboardState, (float)e.Time);
    }

    protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
    {
        base.OnFramebufferResize(e);
        
        GL.Viewport(0, 0, e.Width, e.Height);
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        _resourceManager.UnLoad();
        _currentScene.Unload();
    }
}