using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateSpot : Property
{
    private readonly string[] genericDialogues;
    private readonly Dictionary<string, string[]> uniqueDialogues;

    public string GetRandomDialogue()
    {
        int random = Random.Range(0, genericDialogues.Length - 1);
        return genericDialogues[random];
    }
    public string GetUniqueDialogue(string characterName)
    {
        string[] uniqueLines = uniqueDialogues[characterName];
        int random = Random.Range(0, uniqueLines.Length - 1);
        return uniqueLines[random];
    }
}
