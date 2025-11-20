using UnityEngine;

public class DialTrigger : MonoBehaviour
{
    [SerializeField] D1Launcher dialogueLauncher;
    private bool dialoguePossible = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           dialoguePossible = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           dialoguePossible = false;
        }
    }
    public void ButtonInteractPressed()
    {
        if (dialoguePossible)
        {
            dialogueLauncher.LaunchDialogue();
        }
    }
}
