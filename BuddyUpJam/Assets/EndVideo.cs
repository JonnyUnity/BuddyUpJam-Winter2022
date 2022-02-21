using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class EndVideo : MonoBehaviour
{

    [SerializeField] private VideoPlayer _video;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private Interaction[] _epilogueCouplets;
    [SerializeField] private GameObject _backToMenuButton;

    void Start()
    {
        _video.loopPointReached += OnVideoStopped;
    }


    private void OnVideoStopped(VideoPlayer vid)
    {
        _video.gameObject.SetActive(false);
        _endScreen.SetActive(true);

        StartCoroutine(GameManager.Instance.OpenDialogue(_epilogueCouplets));

        _backToMenuButton.SetActive(true);

    }

    public void BackToMenu()
    {
        GameManager.Instance.EndGameToMainMenu();
        SceneManager.LoadScene(0);
    }




}
