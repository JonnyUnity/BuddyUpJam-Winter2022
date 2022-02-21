using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoomTrigger : MonoBehaviour
{

    [SerializeField] private string _sceneName;
    [SerializeField] private Interaction[] _couplets;
    [SerializeField] private bool _isFinalLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!_isFinalLevel)
            {
                AudioManager.Instance.PlayOpenDoorClip();
            }            
            StartCoroutine(WaitForDialogue());
        }

    }


    private IEnumerator WaitForDialogue()
    {

        if (_couplets.Length != 0)
        {
            yield return StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
        }

        if (_isFinalLevel)
        {
            GameManager.Instance.EndGame();
        }
        GameManager.Instance.LoadScene(_sceneName);

    }

}
