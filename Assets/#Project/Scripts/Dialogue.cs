using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptable Objects/Dialogue")]
[System.Serializable]
public class Dialogue : ScriptableObject
{
    public List<Line> dialogue = new List<Line>();
}
