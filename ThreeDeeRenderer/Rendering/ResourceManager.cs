using OpenTK.Graphics.ES20;
using OpenTK.Mathematics;
using ThreeDeeRenderer.Rendering.Objects;
using ThreeDeeRenderer.Rendering.Shaders;

namespace ThreeDeeRenderer.Rendering;

public class ResourceManager
{
    private Dictionary<string, Shader> _shaders = new();
    private Dictionary<string, Mesh> _objects  = new();

    public Shader GetShader(string shaderName)
    {
        Console.WriteLine($"Loading shader: {shaderName}");
        if (_shaders.ContainsKey(shaderName))
        {
            Console.WriteLine($"Shader '{shaderName}' already cached. Loading from cache.");
            return _shaders[shaderName];
        }
        
        return GetShaderFromName(shaderName);
    }

    public Mesh GetMesh(string meshName)
    {
        Console.WriteLine($"Loading mesh: {meshName}");
        if (_objects.ContainsKey(meshName))
        {
            Console.WriteLine($"Mesh '{meshName}' already cached. Loading from cache.");
            return _objects[meshName];
        }
        
        return GetMeshFromName(meshName);
    }

    private Shader GetShaderFromName(string shaderName)
    {
        var pathName = Path.Combine("Rendering/Shaders", shaderName);

        Console.WriteLine($"Shader '{pathName}' not cached. Loading from disk.");
        
        var vertexPath = Directory.GetFiles(pathName, "*.vert").FirstOrDefault();
        var fragmentPath = Directory.GetFiles(pathName, "*.frag").FirstOrDefault();

        Console.WriteLine($"Looking for '{shaderName}' at {vertexPath} and {fragmentPath}");
        
        if (vertexPath == null || fragmentPath == null)
        {
            throw new Exception($"Shader '{shaderName}' is missing .vert or .frag file");
        }
        
        var shader = new Shader(vertexPath, fragmentPath);

        Console.WriteLine($"Shader '{shaderName}' loaded/cached");
        _shaders[shaderName] = shader; 
        return shader;
    }

    private Mesh GetMeshFromName(string meshName)
    {
        float[] _squareVertices = {
            0.5f,  0.5f, 0.0f, // top right - 0
            0.5f, -0.5f, 0.0f, // bottom right - 1
            -0.5f, -0.5f, 0.0f, // bottom left - 2
            -0.5f,  0.5f, 0.0f // top left - 3
        };

        uint[] _indices = {
            0, 1, 3,
            1, 2, 3
        };

        float[] _triangleVertices =
        {
            -0.5f, -0.5f, 0.0f, 1.0f, 0.3f, 0.2f, //Bottom-left vertex
            0.5f, -0.5f, 0.0f, 1.0f, 1.0f, 0.0f, //Bottom-right vertex
            0.0f,  0.5f, 0.0f, 1.0f, 0.0f, 1.0f, //Top vertex
        };

        List<Vector3> _triangle = new List<Vector3>()
        {
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3(0.5f, -0.5f, 0.0f),
            new Vector3(0.0f, 0.5f, 0.0f),
        };

        List<Vector3> _triangleColor = new List<Vector3>()
        {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 1.0f),
        };
        
        List<Vector3> _pyramid = new List<Vector3>()
        {
            // Front
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.0f,  0.5f,  0.0f),
            new Vector3( 0.5f, -0.5f,  0.5f),

            // Right
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.0f,  0.5f,  0.0f),
            new Vector3( 0.5f, -0.5f, -0.5f),

            // Back
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3( 0.0f,  0.5f,  0.0f),
            new Vector3(-0.5f, -0.5f, -0.5f),

            // Left
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3( 0.0f,  0.5f,  0.0f),
            new Vector3(-0.5f, -0.5f,  0.5f),

            // Bottom triangle 1
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),

            // Bottom triangle 2
            new Vector3(-0.5f, -0.5f,  0.5f),
            new Vector3( 0.5f, -0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, -0.5f),
        };
        
        List<Vector3> _pyramidColor = new List<Vector3>()
        {
            // Front - red
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),

            // Right - green
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),

            // Back - blue
            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 1.0f),
            new Vector3(0.0f, 0.0f, 1.0f),

            // Left - yellow
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),
            new Vector3(1.0f, 1.0f, 0.0f),

            // Bottom triangle 1 - gray
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),

            // Bottom triangle 2 - gray
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
        };

        Mesh _mesh;
        
        switch (meshName.ToLower()) // custom meshes not yet implemented
        {
            case "triangle":
                _mesh = new Mesh(_triangleVertices, Mesh.vertexFormat.positionAndColor);
                Console.WriteLine("Triangle mesh created");
                _objects[meshName] = _mesh;
                return _mesh;
            case "square":
                _mesh = new Mesh(_squareVertices, _indices, Mesh.vertexFormat.positionOnly);
                Console.WriteLine("Square mesh created");
                _objects[meshName] = _mesh;
                return _mesh;
            case "solidtriangle":
                _mesh = new Mesh(_triangle);
                Console.WriteLine("Triangle mesh created (Vector3)");
                _objects[meshName] = _mesh;
                return _mesh;
            case "test_triangle":
                _mesh = new Mesh(_triangle, _triangleColor);
                Console.WriteLine("Triangle+color mesh created (Vector3, Vector3)");
                _objects[meshName] = _mesh;
                return _mesh;
            case "pyramid":
                _mesh = new Mesh(_pyramid, _pyramidColor);
                Console.WriteLine("Pyramid mesh created (Vector3, Vector3)");
                _objects[meshName] = _mesh;
                return _mesh;
            default:
                throw new Exception($"Mesh '{meshName}' is not supported");
        }
    }

    public void UnLoad()
    {
        foreach (var mesh in _objects.Values)
        {
            mesh.Dispose();
        }
        _objects.Clear();
        foreach (var shader in _shaders.Values)
        {
            shader.Dispose();
        }
        _shaders.Clear();
    }
}