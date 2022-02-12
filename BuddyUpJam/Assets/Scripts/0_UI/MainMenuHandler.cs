using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{

    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _creditsMenu;

    private AudioSource _audioSource;

    [SerializeField] private Button QuitButton;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void StartGame()
    {
        Debug.Log("Start Game");
        //_audioSource.
        FadeOutMenuMusic();

        
    }



    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void FadeOutMenuMusic()
    {
        var args = new Hashtable
        {
            { "volume", 0 },
            { "time", 2f },
            { "oncomplete", "LoadFirstLevel" }
        };

        iTween.AudioTo(gameObject, args);
    }

    public void ShowSettings()
    {
        Debug.Log("Show Settings");
        _settingsMenu.SetActive(true);
    }

    public void HideSettings()
    {
        Debug.Log("Hide Settings");
        _settingsMenu.SetActive(false);
    }

    public void ShowCredits()
    {
        Debug.Log("Show Credits");
        _creditsMenu.SetActive(true);
    }

    public void HideCredits()
    {
        Debug.Log("Hide Credits");
        _creditsMenu.SetActive(false);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
  Application.Quit();
#endif


    }

}
