using ThreeDeeRenderer.Rendering;

namespace ThreeDeeRenderer.Scenes.Demos;

public class DemoScene : Scene
{
    private int _currentObjectIndex = 0;

    
    public override void Render()
    {
        GetObjects()[_currentObjectIndex].Draw();
    }

    public void NextObject()
    {
        _currentObjectIndex = (_currentObjectIndex + 1) % GetObjectCount();
    }

    public void PreviousObject()
    {
        _currentObjectIndex = (_currentObjectIndex - 1 + GetObjectCount()) % GetObjectCount();
    }
}