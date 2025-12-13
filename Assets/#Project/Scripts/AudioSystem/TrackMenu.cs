using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class TrackMenu : MonoBehaviour
{
    [Header("Attached GameObjects")] 
    [SerializeField] GameObject panel;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Recorder recorder;


    [Header("panel and button adjustment settings")] 
    [SerializeField] float buttonOffset = -10f; // Space between buttons
    [SerializeField] float panelXOffset = 10;

    // For math
    private float panelYOffset;
    private float panelXSize;
    private float panelYSize;
    private float buttonXSize;
    private float buttonYSize;
    private List<GameObject> buttonList = new List<GameObject>(); // To clean the menu when closing it.

    public void OnMenuButtonPressed() // Appelé dans ByTileController sous la fonction OnInteract()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else gameObject.SetActive(true);
    }

    void OnEnable()
    {
        
        // Calcul for size of the panel
        buttonXSize = buttonPrefab.GetComponent<RectTransform>().sizeDelta.x;
        buttonYSize = buttonPrefab.GetComponent<RectTransform>().sizeDelta.y;
        panelYOffset = buttonOffset * -1; // because need to convert from neg to pos value for sizing
        panelXSize = buttonXSize + panelXOffset * 2;
        panelYSize = (buttonYSize + panelYOffset) * TrackList.tapes.Count + panelYOffset;

        createButtons();
        // Adapt panel size to number of tracks to display
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(panelXSize, panelYSize);
    }

    void OnDisable()
    {
        foreach(GameObject go in buttonList)
        {
            Destroy(go);
        }
    }

    void createButtons()
    {
        int i = 0;

        float offsetCounter = buttonOffset;

        foreach (Tape tape in TrackList.tapes)
        {
            //Create new Button
            GameObject tempObj = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            //Rename Button
            tempObj.name = "button: " + i;
            //Make the Button child of the Canvas
            tempObj.transform.SetParent(panel.transform, false);
            UnityEngine.UI.Button tempButton = tempObj.GetComponent<UnityEngine.UI.Button>();
            //Set Button Text
            tempObj.GetComponentInChildren<TextMeshProUGUI>().text = "Track " + i; 
            Debug.Log("texte du bouton No. " + i + " : " + tempObj.GetComponentInChildren<TextMeshProUGUI>().text);
            //Set Button Position
            Vector2 pos = Vector2.zero;
            pos.y = offsetCounter - buttonYSize/2; // "-" because we're placing the buttons from up to bottom
            Debug.Log(pos.y);
            tempObj.GetComponent<RectTransform>().anchoredPosition = pos;
            tempButton.onClick.AddListener(() => clickAction(tempButton));

            buttonList.Add(tempObj);

            offsetCounter += buttonOffset - buttonYSize; //Increment Position
            i++;
        }
    }

    //This function will be called when a Button is clicked
    public void clickAction(UnityEngine.UI.Button buttonClicked)
    {
        //Debug.Log("Clicked Button: " + buttonClicked.name);
        GameObject buttonObj = buttonClicked.gameObject;

        Debug.Log("Clicked Button: " + buttonObj.GetComponentInChildren<TextMeshProUGUI>().text);

        string buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>().text;
        int buttonIndex = int.Parse(buttonText[buttonText.Length - 1].ToString());

        recorder.PlayRecord(buttonIndex);
    }
}
