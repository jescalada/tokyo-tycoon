using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private static readonly int DAYS_IN_SEASON = 15;
    public int Day
    { get; private set; } = 1;
    public string Season
    { get; private set; } = "Spring";
    public int Year
    { get; private set; } = 2021;

    public void AdvanceDay()
    {
        if (Day >= DAYS_IN_SEASON)
        {
            if (Season.Equals("Winter"))
            {
                Year++;
            }
            Day = 1; 
            Season = NextSeason();
        } else
        {
            Day++;
        }
    }

    private string NextSeason()
    {
        return Season switch
        {
            "Spring" => "Summer",
            "Summer" => "Autumn",
            "Autumn" => "Winter",
            "Winter" => "Spring",
            _ => "Invalid Season",
        };
    }

    override
    public string ToString()
    {
        return string.Format("{0} {1}, {2}", Season.ToUpper(), Day, Year);
    }

}
