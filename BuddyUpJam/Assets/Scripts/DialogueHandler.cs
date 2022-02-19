using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{

    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMPro.TextMeshProUGUI _line1;
    [SerializeField] private TMPro.TextMeshProUGUI _line2;

    private Interaction[] _couplets;
    private int _coupletIndex;

    public bool NarrationPlaying;

    public IEnumerator ShowCouplets(Interaction[] couplets)
    {
        NarrationPlaying = true;
        _couplets = couplets;
        _coupletIndex = 0;

        gameObject.SetActive(true);

        while (IsMoreDialogue())
        {
            var couplet = _couplets[_coupletIndex];
            _line1.text = couplet.FirstLine;
            _line2.text = couplet.SecondLine;


            _coupletIndex++;

            yield return new WaitForSeconds(3f);

        }

        HideDialogue();
        NarrationPlaying = false;

    }

    public bool NextDialogue()
    {
        if (IsMoreDialogue())
        {
            var couplet = _couplets[_coupletIndex];
            _line1.text = couplet.FirstLine;
            _line2.text = couplet.SecondLine;

            _coupletIndex++;
            return true;
        }
        else
        {
            HideDialogue();
            return false;
        }

    }

    //private IEnumerator ProgressDialogue()
    //{
    //    //AudioSource.Play();
    //    //yield return new WaitWhile(() => AudioSource.isPlaying);

    //    while (IsMoreDialogue())
    //    {
    //        var couplet = _couplets[_coupletIndex];
    //        _line1.text = couplet.FirstLine;
    //        _line2.text = couplet.SecondLine;


    //        _coupletIndex++;

    //        yield return new WaitForSeconds(3f);

    //    }

    //    HideDialogue();
    //    NarrationPlaying = false;

    //}


    public void ShowDialogue(string text)
    {
        //_dialogueText.text = text;
        gameObject.SetActive(true);
    }

    public void HideDialogue()
    {
        gameObject.SetActive(false);
        //_dialogueText.text = "";

    }

    public bool DialogueIsShowing()
    {
        return gameObject.activeInHierarchy;
    }

    public bool IsMoreDialogue()
    {
        return (_coupletIndex < _couplets.Length);
    }



}
