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
        GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseRelationshipActiveCharacter(relationshipPoints);
        return UniTask.CompletedTask;
    }
}
