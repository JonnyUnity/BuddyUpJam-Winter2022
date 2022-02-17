using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{

    public void ReturnToGame()
    {
        GameManager.Instance.ShowHidePauseMenu();
    }

    public void ExitGame()
    {
        AudioManager.Instance.StopPlaying();
        GameManager.Instance.LoadMainMenu();
    }

}
