using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class D1Launcher : MonoBehaviour
{
    [SerializeField] NPCDials dialogueObject;
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI characterNameArea;
    [SerializeField] TextMeshProUGUI dialogueLineArea;
    private int dialogueStartIndex = 0;
    private int nextLineIndex;
    private bool dialogueStarted = false;

    void Start()
    {
        
    }

    public void LaunchDialogue()
    {
        if(dialogueStarted == false)
        {
            StartDialogue();
            dialogueStarted = true;
        }
        else if (dialogueObject.npcDials[0].dialogue.Count > nextLineIndex) // Compte des lignes qui se trouve dans le dialogue No.0 de npcDials, voir dialogueObject (qui est un scriptable object contenant tous les dialoue d'un NPC)
        {
            characterNameArea.text = dialogueObject.npcDials[0].dialogue[nextLineIndex].characterName;
            dialogueLineArea.text = dialogueObject.npcDials[0].dialogue[nextLineIndex].sentence;
            
        }
        nextLineIndex ++;
        if (dialogueObject.npcDials[0].dialogue.Count < nextLineIndex)
        {
            EndDialogue();
            dialogueStarted = false;
            nextLineIndex = dialogueStartIndex;
        }
    }

    private void StartDialogue()
    {
        dialogueDisplay.SetActive(true);
        Debug.Log("Entered StartDialogue()");
        characterNameArea.text = dialogueObject.npcDials[0].dialogue[0].characterName;
        dialogueLineArea.text = dialogueObject.npcDials[0].dialogue[0].sentence;
    }

    private void EndDialogue()
    {
        dialogueDisplay.SetActive(false);
    }

}
