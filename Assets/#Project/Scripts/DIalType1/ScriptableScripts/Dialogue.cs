using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Dialogue
{
    public List<Line> dialogue = new List<Line>();
    public bool hasBeenPlayed;
}
