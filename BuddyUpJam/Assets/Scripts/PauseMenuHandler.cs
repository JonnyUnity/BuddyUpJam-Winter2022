using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{

    [SerializeField] private GameObject _pauseMenu;


    //public void ShowHidePauseMenu()
    //{
    //    _pauseMenu.SetActive(!_pauseMenu.activeInHierarchy);
    //}

    public void ReturnToGame()
    {
        // save settings...

        // apply settings...

        //_pauseMenu.SetActive(false);
        GameManager.Instance.ShowHidePauseMenu();
    }

    public void ExitGame()
    {
        GameManager.Instance.LoadMainMenu();
    }

}
