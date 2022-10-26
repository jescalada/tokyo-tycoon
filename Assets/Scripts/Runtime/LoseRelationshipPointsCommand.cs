using Naninovel;
using Naninovel.Commands;
using UnityEngine;

[CommandAlias("loserelationship")]
public class LoseRelationshipPointsCommand : Command
{
    [ParameterAlias("points")]
    public IntegerParameter relationshipPoints = 0;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().DecreaseRelationshipActiveCharacter(relationshipPoints);
        return UniTask.CompletedTask;
    }
}
