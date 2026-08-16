using Godot;
using System;

public partial class RestartButton : Button
{
    private void OnPressed()
	{
        Score.ResetScore();
        GetTree().CallDeferred(SceneTree.MethodName.ReloadCurrentScene);
    }

}
