using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseBackground;

    [SerializeField] private Color _shrinkColor;
    [SerializeField] private Color _enlargeColor;
    [SerializeField] private float _timeToTransform;

    private GameObject _objectToShrink;
    private GameObject _shrinkAnchor;

    private GameObject _objectToEnlarge;
    private GameObject _enlargeAnchor;

    private GameObject _roomObject;
    
    private bool enlargeRayFired;
    private bool shrinkRayFired;

    [SerializeField] private Camera _camera;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _vcam;

    private GameObject _playerObj;
    private PlayerController _playerController;

    [SerializeField] private WandPickup _wandItem;

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _eventSystem;
    [SerializeField] private GameObject _audioManager;

    [SerializeField] private GameObject _dialoguePanel;
    private DialogueHandler _dialogueHandler;

    [SerializeField] private GameObject _changeSizePanel;

    [SerializeField] private GameObject _storyBookButton;
    [SerializeField] private GameObject _storyBookPanel;

    [SerializeField] private GameObject _mouseControlsPanel;

    [SerializeField] private Interaction[] _pickUpStorybookCouplets;
    [SerializeField] private Interaction[] _pickUpWandCouplets;

    private GameStatesEnum State;

    private void Awake()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        _playerController = _playerObj.GetComponent<PlayerController>();
        _dialogueHandler = _dialoguePanel.GetComponent<DialogueHandler>();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(_playerObj);
        DontDestroyOnLoad(_camera);
        DontDestroyOnLoad(_vcam);
        DontDestroyOnLoad(_canvas);
        DontDestroyOnLoad(_eventSystem);

        State = GameStatesEnum.PLAYING;

        SceneManager.sceneLoaded += LoadLevel;

    }

    #region Level Loading/Setup

    private void LoadLevel(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
            return;

        _vcam.LookAt = null;
        _vcam.Follow = null;
        _vcam.enabled = false;
        _camera.enabled = false;

        var playerSpawn = GameObject.FindGameObjectWithTag("Spawn");
        _playerObj = GameObject.FindGameObjectWithTag("Player"); // need to do this again??

        _playerObj.transform.position = playerSpawn.transform.position;

        _camera.enabled = true;
        _vcam.enabled = true;
        _vcam.LookAt = _playerObj.transform;
        _vcam.Follow = _playerObj.transform;

    }

    public void StartGame()
    {
        // Create player object

        // load tutorial level

    }

    public GameStatesEnum GetState()
    {
        return State;
    }

    public void LoadMainMenu()
    {
        
        Destroy(_playerObj);
        Destroy(_camera.gameObject);
        Destroy(_vcam.gameObject);
        Destroy(_canvas);
        Destroy(_eventSystem);
        Destroy(_audioManager);
        Destroy(gameObject);

        SceneManager.LoadScene(0);

    }

    public void LoadScene(string sceneName)
    {
        // do transition...
        

        SceneManager.LoadScene(sceneName);
    }

    #endregion

    #region Wand/Storybook items

    //public void PickupPermanentItem(int item)
    //{
    //    if (item == 1)  // wand
    //    {
    //        _playerController.GainWand();
    //    }
    //    else if (item == 2) // storybook
    //    {
    //        PickupStoryBook();
    //    }
    //}

    public IEnumerator PickupWand()
    {
        AudioManager.Instance.PlayVersionTrack(0);
        State = GameStatesEnum.NARRATION;

        Debug.Log("Start narration");
        yield return StartCoroutine(OpenDialogue(_pickUpWandCouplets));
        //_dialogueHandler.ShowCouplets(_pickUpWandCouplets);
        Debug.Log("narration over");

    }

    #endregion

    #region Storybook

    public IEnumerator PickupStoryBook()
    {

        _storyBookButton.SetActive(true);
        State = GameStatesEnum.NARRATION;

        Debug.Log("Start narration");
        yield return StartCoroutine(OpenDialogue(_pickUpStorybookCouplets));
        //_dialogueHandler.ShowCouplets(_pickUpStorybookCouplets);
        Debug.Log("narration over");

        _wandItem.SetCanBePickedUp(true);

    }

    public void OpenStoryBook()
    {

        if (State == GameStatesEnum.PAUSED)
            return;

        if (_storyBookPanel.activeInHierarchy)
        {
            State = GameStatesEnum.PLAYING;
            _playerController.ChangeState(PlayerStatesEnum.IDLE);
            _storyBookPanel.SetActive(false);
        }
        else
        {
            State = GameStatesEnum.STORYBOOK;
            _playerController.ChangeState(PlayerStatesEnum.STORYBOOK);
            _storyBookPanel.SetActive(true);
        }
    }

    #endregion

    #region Pause Menu

    public void ShowHidePauseMenu()
    {
        // pause audio?...
        if (State == GameStatesEnum.PAUSED)
        {
            State = GameStatesEnum.PLAYING;
            _pauseMenu.SetActive(false);
        }
        else if (State != GameStatesEnum.NARRATION)
        {
            _pauseMenu.SetActive(true);
            State = GameStatesEnum.PAUSED;
        }
    }

    public void SetPauseBackground(byte[] imageArray)
    {
        Texture2D tex = new Texture2D(500, 500, TextureFormat.ARGB32, false);
        tex.LoadRawTextureData(imageArray);
        tex.Apply();

        var sprite = Sprite.Create(tex, new Rect(0, 0, 500, 500), Vector2.zero);

        _pauseBackground.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
    }

    #endregion

    #region Wand Controls/Powers

    public Color GetEnlargeColour()
    {
        return _enlargeColor;
    }

    public Color GetShrinkColour()
    {
        return _shrinkColor;
    }

    public Color GetCursorColour()
    {
        return enlargeRayFired ? _shrinkColor : _enlargeColor;
    }


    public Color SetSelection(GameObject selectedObject, GameObject objectAnchor)
    {
        Color colourToReturn;

        if (selectedObject == _objectToEnlarge)
        {
            colourToReturn = _enlargeColor;
            return colourToReturn;
        }

        if (_objectToEnlarge == null)
        {
            _objectToEnlarge = selectedObject;
            _enlargeAnchor = objectAnchor;

            colourToReturn = _enlargeColor;
            enlargeRayFired = true;
        }
        else
        {
            _objectToShrink = selectedObject;
            _shrinkAnchor = objectAnchor;

            colourToReturn = _shrinkColor;
            shrinkRayFired = true;

        }

        if (_objectToEnlarge != null && _objectToShrink != null)
        {
            ShowChangeSizeHelp();
        }

        return colourToReturn;

    }





    public void DoSwapSize()
    {
        
        if (_objectToEnlarge != null && _objectToShrink != null)
        {

            HideChangeSizeHelp();

            ShrinkEnlarge se1 = _shrinkAnchor.GetComponent<ShrinkEnlarge>();
            ShrinkEnlarge se2 = _enlargeAnchor.GetComponent<ShrinkEnlarge>();

            // if either object has already been shrunk/enlarged before then it cannot be shrunk/enlarged again?
            
            if (se1.CanTransform(0.5f) && se2.CanTransform(2f))
            {
                Vector3 newScale = _shrinkAnchor.transform.localScale;
                newScale *= 0.5f;

                var shrinkArgs = new Hashtable();
                shrinkArgs.Add("time", _timeToTransform);
                shrinkArgs.Add("scale", newScale);
                shrinkArgs.Add("easetype", iTween.EaseType.easeOutElastic);
                shrinkArgs.Add("oncompletetarget", gameObject);
                shrinkArgs.Add("oncomplete", "ResetSelection");
                shrinkArgs.Add("oncompleteparams", _objectToShrink);


                iTween.ScaleTo(_shrinkAnchor, shrinkArgs);
                se1.SetNewSizeFactor(0.5f);

                newScale = _enlargeAnchor.transform.localScale;
                newScale *= 2f;

                shrinkArgs = new Hashtable
                {
                    { "time", _timeToTransform },
                    { "scale", newScale },
                    { "easetype", iTween.EaseType.easeOutElastic },
                    { "oncompletetarget", gameObject },
                    { "oncomplete", "ResetSelection" },
                    { "oncompleteparams", _objectToEnlarge }
                };


                iTween.ScaleTo(_enlargeAnchor, shrinkArgs);
                se2.SetNewSizeFactor(2f);


            }
            else
            {
                var args = new Hashtable
                {
                    { "x", 0.05f },
                    { "time", 0.5f },
                    { "oncompletetarget", gameObject },
                    { "oncomplete", "ResetSelection" },
                    { "oncompleteparams", _objectToShrink }
                };

                iTween.ShakePosition(_shrinkAnchor, args);

                args = new Hashtable
                {
                    { "x", 0.05f },
                    { "time", 0.5f },
                    { "oncompletetarget", gameObject },
                    { "oncomplete", "ResetSelection" },
                    { "oncompleteparams", _objectToEnlarge }
                };

                iTween.ShakePosition(_enlargeAnchor, args);
            }

            _objectToShrink = null;
            _objectToEnlarge = null;
            enlargeRayFired = false;
            shrinkRayFired = false;
        }

    }

    public void ResetSelection(GameObject obj)
    {
        Debug.Log("Reset selection!");
        Selected s = obj.GetComponent<Selected>();
        s.Reset();

    }

    public void ClearSelections()
    {
        Selected s = _objectToShrink.GetComponent<Selected>();
        s.Reset();
        s = _objectToEnlarge.GetComponent<Selected>();
        s.Reset();


        _objectToShrink = null;
        _objectToEnlarge = null;
        _shrinkAnchor = null;
        _enlargeAnchor = null;
        enlargeRayFired = false;
        shrinkRayFired = false;
        
    }

    #endregion

    #region Dialogue

    public void OpenDialogue(string text)
    {
        _dialogueHandler.ShowDialogue(text);
    }

    public IEnumerator OpenDialogue(Interaction[] couplets)
    {
        _playerController.ChangeState(PlayerStatesEnum.NARRATION);
        State = GameStatesEnum.NARRATION;

        _dialogueHandler.NarrationPlaying = true;
        Debug.Log(_dialogueHandler.NarrationPlaying);
        yield return _dialogueHandler.ShowCouplets(couplets);
        Debug.Log(_dialogueHandler.NarrationPlaying);

        //while (_dialogueHandler.NarrationPlaying)
        //{
        //    Debug.Log(_dialogueHandler.NarrationPlaying);
        //    //yield return null;
        //}

        State = GameStatesEnum.PLAYING;
        _playerController.ChangeState(PlayerStatesEnum.IDLE);

    }

    public bool NextDialogue()
    {
        bool moreDialogue = _dialogueHandler.NextDialogue();

        if (!moreDialogue)
        {
            State = GameStatesEnum.PLAYING;
        }    

        return moreDialogue;
    }


    public void CloseDialogue()
    {
        _dialogueHandler.HideDialogue();        
    }

    public bool DialogueIsShowing()
    {
        return _dialogueHandler.DialogueIsShowing();
    }

    #endregion

    public void ShowChangeSizeHelp()
    {
        _changeSizePanel.SetActive(true);
    }

    public void HideChangeSizeHelp()
    {
        _changeSizePanel.SetActive(false);
    }

}

public enum GameStatesEnum
{
    PLAYING,
    PAUSED,
    STORYBOOK,
    NARRATION
}

public enum WandEffectEnum
{
    ENLARGE,
    SHRINK
}
