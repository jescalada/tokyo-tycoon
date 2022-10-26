using Naninovel;
using Naninovel.Commands;
using UnityEngine;

[CommandAlias("collectrent")]
public class CollectRentCommand : Command
{

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().CollectRentActiveProperty();
        return UniTask.CompletedTask;
    }
}
