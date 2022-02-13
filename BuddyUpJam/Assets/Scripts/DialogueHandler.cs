using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{

    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMPro.TextMeshProUGUI _dialogueText;


    public void ShowDialogue(string text)
    {
        _dialogueText.text = text;
        gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        gameObject.SetActive(false);
        _dialogueText.text = "";

    }

    public bool DialogueIsShowing()
    {
        return gameObject.activeInHierarchy;
    }



}
