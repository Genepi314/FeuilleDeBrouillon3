using UnityEngine;

public class DialTrig2 : MonoBehaviour
{
    [SerializeField] DialogueDisplayer dialogueDisplayer;
    [SerializeField] PlayerContr2 playerController;
    [SerializeField] private NPCDials npcStory;
    private bool dialoguePossible = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.GetDialogueTrigger(this);
            // Debug.Log("Entered dialogueTrigger Trigger");
            dialoguePossible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.RemoveDialogueTrigger();
            dialoguePossible = false;
        }
    }
    public void ButtonInteractPressed()
    {
        if (dialoguePossible)
        {
            Debug.Log("Gave dialogue to dialogueManager");
            dialogueDisplayer.SetDialogue(npcStory);
            dialogueDisplayer.DisplayDialogue();
        }
    }
}
