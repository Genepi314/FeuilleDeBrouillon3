using System.Collections;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // [SerializeField] OgreStop ogrestop;
    [SerializeField] DialogueDisplayer dialogueDisplayer;
    [SerializeField] ByTileController playerController;
    [SerializeField] private NPCDials npcStory;
    private bool dialoguePossible = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Entered dialogueTrigger's OnTriggerEnter2D");

            playerController.GetDialogueTrigger(this);
            dialoguePossible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Entered dialogueTrigger's OnTriggerEXIT2D");

            playerController.RemoveDialogueTrigger();
            dialoguePossible = false;
        }
    }
    public void ButtonInteractPressed()
    {
        if (dialoguePossible) // Ajouter logique des dialogues ici. Idéalement ce serait mieux d'avoir un fichier à part qui "feed" cette fonction.
        {
            Debug.Log("From dialogueTrigger, ButtonInteractPressed, DialoguePossible");
            // Debug.Log("Here's the dialogueDisplayer : " + dialogueDisplayer.name);

            dialogueDisplayer.SetDialogue(npcStory);
            dialogueDisplayer.DisplayDialogue();
        }
    }
}
