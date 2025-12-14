using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.Events;

public class DialogueDisplayer : MonoBehaviour
{
    // Dialogue Display
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI characterNameArea;
    [SerializeField] TextMeshProUGUI dialogueLineArea;

    // DialogueObject to display, gotten through public function SetDialogue() via NPCs
    private Dialogue currentDialogue;
    private NPCDials currentNpcStory;

    // Unity Event for Ogre:
    public UnityEvent OnOgreStopSinging;
    public UnityEvent OnOgreStartSinging;

    // Variables for moving through the dialogueLines
    private int dialogueStartIndex = 0;
    private int nextLineIndex;
    private bool dialogueStarted = false; 


    public void DisplayDialogue()
    {
        if (dialogueStarted == false)
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

            dialogueStarted = false;
            nextLineIndex = dialogueStartIndex;
        }
    }

    private void StartDialogue()
    {
        // Debug.Log("Entered StartDialogue()");
        
        if (currentNpcStory.name == "OgreDials") OnOgreStopSinging.Invoke();

        dialogueDisplay.SetActive(true);
        characterNameArea.text = currentDialogue.dialogue[0].characterName;
        dialogueLineArea.text = currentDialogue.dialogue[0].sentence;
    }

    private void EndDialogue()
    {
        if (currentNpcStory.name == "OgreDials") OnOgreStartSinging.Invoke();

        dialogueDisplay.SetActive(false);
        currentNpcStory = null;
        currentDialogue = null;
    }

    public void SetDialogue(NPCDials npcStory) // Utilisée par les DialogueTriggers
    {
        this.currentNpcStory = npcStory;
        this.currentDialogue = npcStory.npcDials[0];
    }
}
