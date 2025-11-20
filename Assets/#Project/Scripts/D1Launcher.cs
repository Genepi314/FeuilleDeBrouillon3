using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class D1Launcher : MonoBehaviour
{
    [SerializeField] Dialogue npcDial1;
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI characterNameArea;
    [SerializeField] TextMeshProUGUI dialogueLineArea;
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
        else if (npcDial1.dialogue.Count > nextLineIndex)
        {
            characterNameArea.text = npcDial1.dialogue[nextLineIndex].characterName;
            dialogueLineArea.text = npcDial1.dialogue[nextLineIndex].sentence;
            
        }
        nextLineIndex ++;
        if (npcDial1.dialogue.Count < nextLineIndex)
        {
            EndDialogue();
            dialogueStarted = false;
            nextLineIndex = dialogueStartIndex;
        }
    }

    private void StartDialogue()
    {
        dialogueDisplay.SetActive(true);
        // Debug.Log("ButtonInteractPressed");
        characterNameArea.text = npcDial1.dialogue[0].characterName;
        dialogueLineArea.text = npcDial1.dialogue[0].sentence;
    }

    private void EndDialogue()
    {
        dialogueDisplay.SetActive(false);
    }

}
