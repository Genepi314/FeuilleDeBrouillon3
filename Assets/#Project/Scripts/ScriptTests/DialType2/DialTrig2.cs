using Unity.VisualScripting;
using UnityEngine;

public class DialTrig2 : MonoBehaviour
{
    [SerializeField] DialDisplayer dialogueDisplayer;
    [SerializeField] PlayerContr2 playerController;
    [SerializeField] private NPCDials npcStory;
    private bool dialoguePossible = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.GetDialogueTrigger(this);
            // Debug.Log("Entered dialogueTrigger's OnTriggerEnter2D");
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
            // Debug.Log("ButtonInteractPressed, DialoguePossible");
            // Debug.Log("nom du gameObject parent : " + transform.parent.gameObject.name);
            if (transform.parent.gameObject.name == "Circle")
            {
                dialogueDisplayer.SetDialogue(npcStory.npcDials[0]);
                dialogueDisplayer.DisplayDialogue();
                DialTracker.talkedToCircle = true;
            }
            else if(transform.parent.gameObject.name == "Square")
            {
                if(DialTracker.talkedToCircle == false)
                {
                    dialogueDisplayer.SetDialogue(npcStory.npcDials[0]);
                    dialogueDisplayer.DisplayDialogue();
                }
                else
                {
                    dialogueDisplayer.SetDialogue(npcStory.npcDials[1]);
                    dialogueDisplayer.DisplayDialogue();
                }
            }
        }
    }
}
