using OpenTK.Mathematics;
using ThreeDeeRenderer.Rendering.Objects;

namespace ThreeDeeRenderer.World;

public class Camera
{
    private Matrix4 projectionMatrix;
    private Matrix4 viewMatrix ;

    Transform transform = new Transform();
    
    public void initializeCamera(int width, int height, float fov)
    {
        viewMatrix = Matrix4.Identity;
        projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians((float)fov), width / height, 0.1f, 100.0f);
    }

    public Matrix4 GetViewMatrix()
    {
        return viewMatrix;
    }
    
    public Matrix4 GetProjectionMatrix()
    {
        return projectionMatrix;
    }
}