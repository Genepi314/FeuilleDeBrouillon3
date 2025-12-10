using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Comedon : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject trackButtonPrefab;
    private GameObject secondButton;
    private Vector3 firstButtonPos;

    public void OnMenuButtonPressed() // Appelé dans ByTileController sous la fonction OnInteract()
    {
        gameObject.SetActive(true);
        trackButtonPrefab.GetComponentInChildren<TextMeshProUGUI>().text = "Track 01";

    }

    void OnEnable() // Pour l'instant CreateButton est dans OnEnable pour tester. Il faudra mettre cette fonction en lien avec le Recorder.
    {
        CreateButton(secondButton);
    }

    private void CreateButton(GameObject anyButton)
    {
        anyButton = Instantiate(trackButtonPrefab);
        anyButton.transform.SetParent(panel.transform, false);
        anyButton.transform.position = trackButtonPrefab.transform.position + Vector3.down * 10;
        anyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Track 02"; 
    }
}
