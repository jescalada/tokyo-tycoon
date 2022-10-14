using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static readonly int ACQUAINTANCE_THRESHOLD = 100;
    private static readonly int FRIEND_THRESHOLD = 500;
    private static readonly int CLOSE_FRIEND_THRESHOLD = 2000;
    private static readonly int LOVER_THRESHOLD = 10000;
    public string Name
    { get; }
    public int Age
    { get; }
    public string Description
    { get; }
    public RelationshipLevel CurrentRelationshipLevel
    { get; private set; }
    public int RelationshipPoints
    { get; private set; }
    public List<string> Likes
    { get; }
    public List<string> Dislikes
    { get; }
    public List<string> IdleDialogues
    { get; }
    public List<string> RentPayDialogues
    { get; }
    public List<string> DateInvitationDialogues
    { get; }
    public Dictionary<RelationshipLevel, List<string>> ChatDialoguesByRelation
    { get; }

    public void InviteToDate()
    {

    }
    public string GetProfileInfo()
    {
        return string.Format("Name: {0}\nAge: {1}\n{2}", Name, Age, Description);
    }
    public void UpdateRelationshipLevel(int relationshipPoints)
    {
        RelationshipPoints += relationshipPoints;
        if (RelationshipPoints >= LOVER_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.Lover;
        else if (RelationshipPoints >= CLOSE_FRIEND_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.CloseFriend;
        else if (RelationshipPoints >= FRIEND_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.Friend;
        else if (RelationshipPoints >= ACQUAINTANCE_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.Acquaintance;
        else CurrentRelationshipLevel = RelationshipLevel.Stranger;
    }
    public enum RelationshipLevel
    {
        Stranger,
        Acquaintance,
        Friend,
        CloseFriend,
        Lover
    }
}
