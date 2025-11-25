using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class DialLaunch2 : MonoBehaviour
{
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI characterNameArea;
    [SerializeField] TextMeshProUGUI dialogueLineArea;
    private Dialogue currentDialogue;
    private int dialogueStartIndex = 0;
    private int nextLineIndex;
    private bool dialogueStarted = false;

    public void LaunchDialogue()
    {
        if(dialogueStarted == false)
        {
            StartDialogue();
            dialogueStarted = true;
        }
        else if (currentDialogue.dialogue.Count > nextLineIndex) // Compte des lignes qui se trouve dans le dialogue No.0 de npcDials, voir dialogueObject (qui est un scriptable object contenant tous les dialoue d'un NPC)
        {
            characterNameArea.text = currentDialogue.dialogue[nextLineIndex].characterName;
            dialogueLineArea.text = currentDialogue.dialogue[nextLineIndex].sentence;
            
        }
        nextLineIndex ++;
        if (currentDialogue.dialogue.Count < nextLineIndex)
        {
            EndDialogue();
            currentDialogue.hasBeenPlayed = true;
            dialogueStarted = false;
            nextLineIndex = dialogueStartIndex;
            
        }
    }

    private void StartDialogue()
    {
        dialogueDisplay.SetActive(true);
        Debug.Log("Entered StartDialogue()");
        characterNameArea.text = currentDialogue.dialogue[0].characterName;
        dialogueLineArea.text = currentDialogue.dialogue[0].sentence;
    }

    private void EndDialogue()
    {
        dialogueDisplay.SetActive(false);
    }

    public void SetDialogue(Dialogue dialogue)
    {
        currentDialogue = dialogue;
    }
}
