using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class TrackMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Recorder recorder;

    [SerializeField] const float yPosOffset = -12f;
    [SerializeField] float buttonOffset = -6;
    private List<GameObject> buttonList = new List<GameObject>();

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
        createButtons();
        // Adapt panel size to number of tracks to display
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(140, TrackList.tapes.Count * Mathf.Abs(yPosOffset));
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
        // const float yPosOffset = -10f;
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
            pos.y = offsetCounter;
            Debug.Log(pos.y);
            tempObj.GetComponent<RectTransform>().anchoredPosition = pos;
            tempButton.onClick.AddListener(() => clickAction(tempButton));

            buttonList.Add(tempObj);

            offsetCounter += yPosOffset; //Increment Position
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
