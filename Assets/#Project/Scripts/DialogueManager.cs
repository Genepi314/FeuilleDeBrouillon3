using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] List<DialLaunch2> dialLaunchers;    
    [SerializeField] List<NPCDials> dialObjects;
    void Start()
    {
        dialLaunchers[0].SetDialogue(dialObjects[0].npcDials[0]); // Attribue le dial No.0 à Circle
        dialLaunchers[1].SetDialogue(dialObjects[1].npcDials[0]); // Attribue le dial No.0 à Square
    }


}
