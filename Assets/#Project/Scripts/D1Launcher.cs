using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class D1Launcher : MonoBehaviour
{
    [SerializeField] Dialogue npcDial1;
    [SerializeField] GameObject dialogueDisplay;
    [SerializeField] TextMeshProUGUI characterNameArea;
    [SerializeField] TextMeshProUGUI dialogueLineArea;

    void Start()
    {
        dialogueDisplay.SetActive(true);
    }

    public void ButtonInteractPressed()
    {
        Debug.Log("ButtonInteractPressed");
        characterNameArea.text = npcDial1.dialogue[0].characterName;
        dialogueLineArea.text = npcDial1.dialogue[0].sentence;
    }

}
