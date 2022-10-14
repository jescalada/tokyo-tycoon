using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date : MonoBehaviour
{
    private string[] dialogue;
    private int dialogueIndex = 0;
    public DateSpot DateSpot
    { get; }

    public void AdvanceDialogue()
    {
        dialogueIndex++;
    }

    public string GetCurrentDialogue()
    {
        return dialogue[dialogueIndex];
    }

    public bool DialogueFinished()
    {
        return (dialogueIndex >= dialogue.Length);
    }
}
