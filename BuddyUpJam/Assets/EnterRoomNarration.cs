using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomNarration : MonoBehaviour
{

    [SerializeField] private Interaction[] _couplets;


    private void Start()
    {
        StartNarration();
    }

    public void StartNarration()
    {
        StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
    }

}
