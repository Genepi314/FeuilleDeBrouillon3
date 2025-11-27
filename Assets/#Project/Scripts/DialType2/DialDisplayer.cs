using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class DialogueDisplayer : MonoBehaviour
{
    // Dialogue Display
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI characterNameArea;
    [SerializeField] TextMeshProUGUI dialogueLineArea;

    // DialogueObject to display, gotten through public function SetDialogue() via NPCs
    private NPCDials npcStory;

    // Variables for moving through the dialogueLines
    private int dialogueStartIndex = 0;
    private int nextLineIndex;
    private bool dialogueStarted = false;



    public void DisplayDialogue()
    {
        if(dialogueStarted == false)
        {
            StartDialogue();
            dialogueStarted = true;
        }
        else if (npcStory.npcDials[0].dialogue.Count > nextLineIndex) // Compte des lignes qui se trouve dans le dialogue No.0 de npcDials, voir dialogueObject (qui est un scriptable object contenant tous les dialoue d'un NPC)
        {
            characterNameArea.text = npcStory.npcDials[0].dialogue[nextLineIndex].characterName;
            dialogueLineArea.text = npcStory.npcDials[0].dialogue[nextLineIndex].sentence;
            
        }
        nextLineIndex ++;
        if (npcStory.npcDials[0].dialogue.Count < nextLineIndex)
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
        characterNameArea.text = npcStory.npcDials[0].dialogue[0].characterName;
        dialogueLineArea.text = npcStory.npcDials[0].dialogue[0].sentence;
    }

    private void EndDialogue()
    {
        dialogueDisplay.SetActive(false);
    }

    public void SetDialogue(NPCDials npcStory)
    {
        this.npcStory = npcStory;
    }


}
