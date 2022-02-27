using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomNarration : MonoBehaviour
{

    [SerializeField] private Interaction[] _couplets;

    private void Start()
    {
        Invoke(nameof(StartNarration), 1);
    }
    
    
    public void StartNarration()
    {
        StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
    }

}
