using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ThreeDeeRenderer.World.Scenes.Demos;

public class DemoScene : Scene
{
    private int _currentObjectIndex = 0;
    
    public override void Render()
    {
        DrawSingleObject(_currentObjectIndex);
    }

    public override void Update(KeyboardState keyboardState, float deltaTime)
    {
        if (keyboardState.IsKeyPressed(Keys.Right))
        {
            NextObject();
        }

        if (keyboardState.IsKeyPressed(Keys.Left))
        {
            PreviousObject();
        }
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