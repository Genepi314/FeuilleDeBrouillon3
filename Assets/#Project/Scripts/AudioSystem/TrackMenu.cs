using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
// using System.Numerics;

public class TrackMenu : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject buttonPrefab;

    public void OnMenuButtonPressed() // Appelé dans ByTileController sous la fonction OnInteract()
    {
        gameObject.SetActive(true);
    }

    void OnEnable()
    {
        createButtons();
    }

    void createButtons()
    {
        int i = 0;
        const float yPosOffset = -10f;
        float offsetCounter = 10;

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
    }
}
