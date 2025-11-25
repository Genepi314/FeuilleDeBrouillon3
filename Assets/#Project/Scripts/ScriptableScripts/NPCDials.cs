using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCDials", menuName = "Scriptable Objects/NPCDials")]
[System.Serializable]
public class NPCDials : ScriptableObject
{
    public List<Dialogue> npcDials = new List<Dialogue>();
}
