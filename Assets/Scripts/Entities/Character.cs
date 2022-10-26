using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private static readonly int ACQUAINTANCE_THRESHOLD = 100;
    private static readonly int FRIEND_THRESHOLD = 500;
    private static readonly int CLOSE_FRIEND_THRESHOLD = 2000;
    private static readonly int LOVER_THRESHOLD = 10000;
    
    public string Name;
    public int Age;
    public string Description;
    public RelationshipLevel CurrentRelationshipLevel = RelationshipLevel.Stranger;
    public int RelationshipPoints;
    public List<string> Likes;
    public List<string> Dislikes;
    public List<string> IdleDialogues;
    public List<string> RentPayDialogues;
    public List<string> DateInvitationDialogues;
    public Dictionary<RelationshipLevel, List<string>> ChatDialoguesByRelation;

    public void InviteToDate()
    {

    }
    public string GetProfileInfo()
    {
        return string.Format("Name: {0}\nAge: {1}\n{2}", Name, Age, Description);
    }
    public void GainRelationshipPoints(int relationshipPoints)
    {
        RelationshipPoints += relationshipPoints;
        // Todo: Add animation in case relationship level changes
        if (RelationshipPoints >= LOVER_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.Lover;
        else if (RelationshipPoints >= CLOSE_FRIEND_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.CloseFriend;
        else if (RelationshipPoints >= FRIEND_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.Friend;
        else if (RelationshipPoints >= ACQUAINTANCE_THRESHOLD) CurrentRelationshipLevel = RelationshipLevel.Acquaintance;
        else CurrentRelationshipLevel = RelationshipLevel.Stranger;
    }
    public void LoseRelationshipPoints(int relationshipPoints)
    {
        RelationshipPoints -= relationshipPoints;
        // Todo: Add animation in case relationship level changes
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
