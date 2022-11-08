using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public Sprite fullHeart;
    public Sprite emptyHeart;

    public void InviteToDate()
    {

    }
    public string GetProfileInfo()
    {
        return string.Format("Name: {0}\nAge: {1}\n{2}", Name, Age, Description);
    }
    public void SetRelationshipMeter()
    {
        var relationshipBar = GameObject.Find("RelationshipBarContainer");
        float ratio = CalculateRelationshipBarFillRatio();
        var slider = relationshipBar.GetComponentInChildren<Slider>();
        slider.value = CalculateRelationshipBarFillRatio();

        var heartsContainer = GameObject.Find("Hearts");
        Image[] heartIcons = heartsContainer.GetComponentsInChildren<Image>();
        int fullHearts = CalculateRelationshipHeartsLevel();
        int heartCounter = 0;
        foreach (Image icon in heartIcons.Reverse())
        {
            if (heartCounter < fullHearts)
            {
                icon.sprite = fullHeart;
            } else
            {
                icon.sprite = emptyHeart;
            }
            heartCounter++;
        }

        Debug.Log(string.Format("The relationship ratio is {0}, the bar is {1}, the slider is {2} with a value of {3}",
            ratio, relationshipBar, slider, slider.value));
    }

    public int CalculateRelationshipHeartsLevel()
    {
        if (CurrentRelationshipLevel.Equals(RelationshipLevel.Stranger))
        {
            return 1;
        }
        else if (CurrentRelationshipLevel.Equals(RelationshipLevel.Acquaintance))
        {
            return 2;
        }
        else if (CurrentRelationshipLevel.Equals(RelationshipLevel.Friend))
        {
            return 3;
        }
        else if (CurrentRelationshipLevel.Equals(RelationshipLevel.CloseFriend))
        {
            return 4;
        }
        else if (CurrentRelationshipLevel.Equals(RelationshipLevel.Lover))
        {
            return 5;
        } else
        {
            return 0;
        }
    }

    public float CalculateRelationshipBarFillRatio()
    {
        if (RelationshipPoints >= LOVER_THRESHOLD)
        {
            return 1f;
        } else if (RelationshipPoints >= CLOSE_FRIEND_THRESHOLD)
        {
            return (float)(RelationshipPoints - CLOSE_FRIEND_THRESHOLD) / (LOVER_THRESHOLD - CLOSE_FRIEND_THRESHOLD);
        } else if (RelationshipPoints >= FRIEND_THRESHOLD)
        {
            return (float)(RelationshipPoints - FRIEND_THRESHOLD) / (CLOSE_FRIEND_THRESHOLD - FRIEND_THRESHOLD);
        } else if (RelationshipPoints >= ACQUAINTANCE_THRESHOLD)
        {
            return (float)(RelationshipPoints - ACQUAINTANCE_THRESHOLD) / (FRIEND_THRESHOLD - ACQUAINTANCE_THRESHOLD);
        } else
        {
            return (float)RelationshipPoints / ACQUAINTANCE_THRESHOLD;
        }
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
