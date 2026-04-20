using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ThreeDeeRenderer.Rendering.Objects;

public class Mesh
{
    private int _vertexBufferObject;
    private int _vertexBufferColor;
    private int _vertexArrayObject;
    private int _elementBufferObject;
    private int _indicesCount;
    private int _vertexCount;
    

    private int _stride;
    
    public enum vertexFormat
    {
        positionOnly,
        positionAndColor
    }

    public Mesh(List<Vector3> vertices, List<Vector3> color = null)
    {
        Console.WriteLine($"Creating mesh with {vertices.Count} vertices");
        _vertexCount = vertices.Count;
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);
        
        _vertexBufferObject = GL.GenBuffer();
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        
        var verticesSpan = CollectionsMarshal.AsSpan(vertices);
        
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * Vector3.SizeInBytes, ref verticesSpan[0], BufferUsageHint.StaticDraw);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
        GL.EnableVertexAttribArray(0);

        if (color != null)
        {
            if (color.Count != vertices.Count)
            {
                throw new Exception("The number of vertices must be equal to the number of colors");
            }
            
            _vertexBufferColor = GL.GenBuffer();
            var colorSpan = CollectionsMarshal.AsSpan(color);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // unbind Object Buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferColor); // Bind Color buffer
            
            GL.BufferData(BufferTarget.ArrayBuffer, color.Count * Vector3.SizeInBytes, ref colorSpan[0], BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.EnableVertexAttribArray(1);
        }
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0); // unbind any buffer
        GL.BindVertexArray(0); // unbind VAO
    }
        
    
    public Mesh(float[] vertices, uint[] indices, vertexFormat format)
    {
        _indicesCount = indices.Length;
        Console.WriteLine($"Creating mesh with {vertices.Length} vertices");
        _vertexCount = vertices.Length;
        
        // Create & Bind VAO
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject); // Bind VAO
        
        // Bind VBO
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        if (format == vertexFormat.positionAndColor)
        {
            _vertexCount = vertices.Length / 2;
            Console.WriteLine($"Vertex format is position and color. Actual vertices length: {vertices.Length}");
            
            // Record vertices data in VAO
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, 0);
            GL.EnableVertexAttribArray(0); //  record for location 0 == mesh shape
        
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, sizeof(float) * 3); // position 1 = color information
            GL.EnableVertexAttribArray(1); // record for location 1
        }
        else if  (format == vertexFormat.positionOnly)
        {
            // Record vertices data in VAO
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
            GL.EnableVertexAttribArray(0); //  record for location 0 == mesh shape
        }
        
        // Bind EBO
        _elementBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
        
        // Unbind buffer + VAO
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }
    
    public Mesh(float[] vertices, vertexFormat format)
    {
        Console.WriteLine($"Creating mesh with {vertices.Length} vertices");
        _vertexCount = vertices.Length;
        
        // Create & Bind VAO
        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject); // Bind VAO
        
        // Bind VBO
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        if (format == vertexFormat.positionAndColor)
        {
            // Record vertices data in VAO
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, 0);
            GL.EnableVertexAttribArray(0); //  record for location 0 == mesh shape
        
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, sizeof(float) * 6, sizeof(float) * 3); // position 1 = color information
            GL.EnableVertexAttribArray(1); // record for location 1
        }
        else
        {
            // Record vertices data in VAO
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);
            GL.EnableVertexAttribArray(0); //  record for location 0 == mesh shape
        }
        
        // Unbind buffer + VAO
        GL.BindVertexArray(0);
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    public void Draw()
    {
        GL.BindVertexArray(_vertexArrayObject);
        if (_elementBufferObject > 0)
        {
            GL.DrawElements(PrimitiveType.Triangles, _indicesCount, DrawElementsType.UnsignedInt, 0);
        }
        else
        {
            if (_vertexCount < 3)
            {
                Console.WriteLine("Less than 3 vertices available; falling back to 3. Is this a triangle?");
            }
            GL.DrawArrays(PrimitiveType.Triangles, 0, _vertexCount | 3);
        }
    }
    
    private bool _disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteBuffer(_elementBufferObject);

            _disposedValue = true;
        }
    }

    ~Mesh()
    {
        if (!_disposedValue)
        {
            Console.WriteLine("GPU Resource leak! Did you forget to call Dispose()?");
        }
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}