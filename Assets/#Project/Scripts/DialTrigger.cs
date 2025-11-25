using UnityEngine;

public class DialTrigger : MonoBehaviour
{
    [SerializeField] D1Launcher dialogueLauncher;
    [SerializeField] PlayerController playerController;
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
            Debug.Log("Asked dialogue");
            dialogueLauncher.LaunchDialogue();
        }
    }
}
