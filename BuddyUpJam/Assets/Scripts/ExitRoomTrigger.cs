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
            var playerObj = collision.gameObject;
            PlayerController player = playerObj.GetComponent<PlayerController>();
            player.StopMoving();

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
            yield return new WaitForSeconds(1f);
            GameManager.Instance.HideStoryBook();
            GameManager.Instance.EndGame();
        }
        GameManager.Instance.LoadScene(_sceneName);

    }

}
