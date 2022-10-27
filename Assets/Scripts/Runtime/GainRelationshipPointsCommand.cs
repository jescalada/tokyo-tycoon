using Naninovel;
using Naninovel.Commands;
using UnityEngine;

[CommandAlias("gainrelationship")]
public class GainRelationshipPointsCommand : Command
{
    [ParameterAlias("points")]
    public IntegerParameter relationshipPoints = 0;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.IncreaseRelationshipActiveCharacter(relationshipPoints);
        // TODO GET RID OF THIS AND MAKE IT A COMMAND
        gameManager.ResetActiveProperty();

        return UniTask.CompletedTask;
    }
}
