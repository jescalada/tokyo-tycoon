using UnityEngine;
using Naninovel;

public class DialogueTrigger : MonoBehaviour
{
    public string ScriptName;
    public string Label;

    public void Trigger()
    {
        // Todo: prevent clicking outside the UI box upon triggering?
        // Or otherwise don't reset the UI interactions upon clicking UI again

        // var controller = other.gameObject.GetComponentInChildren<CharacterController3D>();
        // controller.IsInputBlocked = true;
        
        var inputManager = Engine.GetService<IInputManager>();
        inputManager.ProcessInput = true;

        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        scriptPlayer.PreloadAndPlayAsync(ScriptName, label: Label).Forget();
    }
}

