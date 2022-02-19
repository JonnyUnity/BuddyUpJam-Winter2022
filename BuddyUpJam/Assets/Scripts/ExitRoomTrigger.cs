using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoomTrigger : MonoBehaviour
{

    [SerializeField] private string _sceneName;
    [SerializeField] private Interaction[] _couplets;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(WaitForDialogue());
        }

    }


    private IEnumerator WaitForDialogue()
    {

        if (_couplets.Length != 0)
        {
            yield return StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));

            //do
            //{
            //    yield return null;
            //}
            //while (GameManager.Instance.GetState() == GameStatesEnum.NARRATION);

        }

        GameManager.Instance.LoadScene(_sceneName);

    }

}
