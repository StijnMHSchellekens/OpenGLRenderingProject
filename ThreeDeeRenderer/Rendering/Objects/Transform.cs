using OpenTK.Mathematics;

namespace ThreeDeeRenderer.Rendering.Objects;

public class Transform
{
    private Vector3 _rotation;
    private Vector3 _position;
    private Vector3 _scale = new Vector3(1.0f, 1.0f, 1.0f);
    
    public void SetPosition(Vector3 position)
    {
        _position = position;
    }

    public void SetRotation(Vector3 rotation)
    {
        _rotation = rotation;
    }
    
    public void SetScale(Vector3 scale)
    {
        _scale = scale;
    }
    
    public void Translate(Vector3 position)
    {
        _position += position;
    }
    
    public void Rotate(Vector3 rotation)
    {
        _rotation += rotation;
    }
    
    public void ApplyScale(Vector3 scale)
    {
        _scale += scale;
    }

    public Matrix4 GetMatrix()
    {
        Matrix4 translation = Matrix4.CreateTranslation(_position);
        
        Matrix4 rotatex = Matrix4.CreateRotationX(_rotation.X);
        Matrix4 rotatey = Matrix4.CreateRotationY(_rotation.Y);
        Matrix4 rotatez = Matrix4.CreateRotationZ(_rotation.Z);
        
        Matrix4 scale = Matrix4.CreateScale(_scale);
        
        Matrix4 rotation = rotatex * rotatey * rotatez;
        Matrix4 transform = translation * rotation * scale;
        
        return transform;
    }
}