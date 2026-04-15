using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ThreeDeeRenderer.Rendering;

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
    

    private Shader _shader;
    private Shader _solidShader;
    
    private Mesh _triangle;
    private Mesh _square;

    private RenderObject _triangleObj;
    private RenderObject _squareObj;
    
    private List<RenderObject> _objects = new();
    private int _currentMeshIndex = 0;
    
    public Game(int width, int height, string title) : base(GameWindowSettings.Default, new NativeWindowSettings() 
        { ClientSize = (width, height), Title = title }) { }

    protected override void OnLoad()
    {
        base.OnLoad();
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag"); // set Shaders
        _solidShader = new Shader("Shaders/solid.vert", "Shaders/solid.frag"); // set Shaders
        
        _square = new Mesh(_squareVertices, _indices, Mesh.vertexFormat.positionOnly);
        _triangle = new Mesh(_triangleVertices, Mesh.vertexFormat.positionAndColor);
        
        _triangleObj = new RenderObject(_triangle, _shader);
        _squareObj = new RenderObject(_square, _solidShader);

        _objects.Add(_triangleObj);
        _objects.Add(_squareObj);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        try
        {
            base.OnRenderFrame(args);
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _objects[_currentMeshIndex].Draw();
        
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
            _currentMeshIndex =  (_currentMeshIndex + 1) % _objects.Count; // cycle through meshes.
        }

        if (KeyboardState.IsKeyReleased(Keys.Left))
        {
            _currentMeshIndex = (_currentMeshIndex - 1 + _objects.Count) % _objects.Count; // cycle the other way through meshes.
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
        foreach (var mesh in _objects)
        {
            mesh.Mesh.Dispose();
            mesh.Shader.Dispose();
        }
        _objects.Clear();
    }
}