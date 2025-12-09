using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ClickEvent : MonoBehaviour
{
    public UnityEvent<int> OnButtonClicked;
    public TextMeshProUGUI label;

    public void ButtonClicked() 
    {
        OnButtonClicked?.Invoke(Convert.ToInt32(label.text[label.text.Length - 1])); // On prend le dernier char du texte du bouton et on le convertit en int.
    }
}