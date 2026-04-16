namespace ThreeDeeRenderer.Rendering;

public class ResourceManager
{
    private Dictionary<string, Shader> _shaders = new();
    private Dictionary<string, Mesh> _objects  = new();

    public Shader GetShader(string shaderName)
    {
        if (_shaders.ContainsKey(shaderName))
        {
            return _shaders[shaderName];
        }
        
        return GetShaderFromName(shaderName);
    }

    public Mesh GetMesh(string meshName)
    {
        if (_objects.ContainsKey(meshName))
        {
            return _objects[meshName];
        }
        
        return GetMeshFromName(meshName);
    }

    private Shader GetShaderFromName(string shaderName)
    {
        var pathName = Path.Combine("Shaders", shaderName);

        var vertexPath = Directory.GetFiles(pathName, "*.vert").FirstOrDefault();
        var fragmentPath = Directory.GetFiles(pathName, "*.frag").FirstOrDefault();

        if (vertexPath == null || fragmentPath == null)
        {
            throw new Exception($"Shader '{shaderName}' is missing .vert or .frag file");
        }
        
        var shader = new Shader(vertexPath, fragmentPath);

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

        Mesh _mesh;
        
        switch (meshName.ToLower()) // custom meshes not yet implemented
        {
            case "triangle":
                _mesh = new Mesh(_triangleVertices, Mesh.vertexFormat.positionAndColor);
                _objects[meshName] = _mesh;
                return _mesh;
            case "square":
                _mesh = new Mesh(_squareVertices, _indices, Mesh.vertexFormat.positionOnly);
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