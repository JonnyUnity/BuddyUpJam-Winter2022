using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoomTrigger : MonoBehaviour
{

    [SerializeField] private string _sceneName;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.LoadScene(_sceneName);
        }

    }

}
