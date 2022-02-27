using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryHandler : MonoBehaviour
{
    [SerializeField] private GameObject _exitTrigger;
    [SerializeField] private GameObject _potObject;
    [SerializeField] private GameObject _desiredPotPosition;

    [SerializeField] private FussyDropObject _globeContainer;

    [SerializeField] private GameObject _potPickUpObject;
    private PickUp _potPickUp;
    [SerializeField] private GameObject _potDropInObject;
    private FussyDropObject _potContainer;

    private Transform _globeTransform;
    private Transform _featherTransform;
    private Transform _potTransform;

    [SerializeField] private Interaction[] _globeCorrectCouplets;
    [SerializeField] private Interaction[] _potCorrectCouplets;
    [SerializeField] private Interaction[] _potionBrewedCouplets;

    private bool _isGlobeCorrect;
    private bool _globeCorrectNarrationDone;
    private bool _isPotCorrect;
    private bool _potCorrectNarrationDone;
    private bool _isPotionBrewed;
    private bool _potionBrewedNarrationDone;
    private bool _bellPlayed;

    private void Awake()
    {
        AudioManager.Instance.PlayVersionTrack(2);
    }

    private void Start()
    {
        _potTransform = _potObject.transform;
        _potPickUp = _potPickUpObject.GetComponent<PickUp>();
        _potContainer = _potDropInObject.GetComponent<FussyDropObject>();

        // Check every second if puzzle has been solved and player can proceed
        InvokeRepeating(nameof(CheckRoomSolved), 1, 1);

    }

    public void CheckRoomSolved()
    {

        if (!_globeCorrectNarrationDone)
        {
            if (IsGlobeCorrect())
            {
                StartCoroutine(GameManager.Instance.OpenDialogue(_globeCorrectCouplets));
                _isGlobeCorrect = true;
                _globeCorrectNarrationDone = true;
            }
        }

        if (!_potCorrectNarrationDone)
        {
            if (IsPotCorrect())
            {               
                StartCoroutine(GameManager.Instance.OpenDialogue(_potCorrectCouplets));
                _isPotCorrect = true;
                _potCorrectNarrationDone = true;
            }
        }

        if (!_potionBrewedNarrationDone)
        {

            if (IsPotionBrewed())
            {
                StartCoroutine(GameManager.Instance.OpenDialogue(_potionBrewedCouplets));
                _isPotionBrewed = true;
                _potionBrewedNarrationDone = true;
            }

        }

        if (IsRoomSolved())
        {

            if (!_bellPlayed)
            {
                AudioManager.Instance.PlayUnlockDoorClip();
                _bellPlayed = true;
            }
            
            _exitTrigger.SetActive(true);

        }
    }

    private bool IsPotionBrewed()
    {
        return (_potContainer.DroppedObjectCount == 2); // feather and flower
    }

    private bool IsPotCorrect()
    {
        
        if (_potTransform.position == _desiredPotPosition.transform.position)
        {
            _potDropInObject.SetActive(true);
        }
        else
        {
            return false;
        }
        
        ShrinkEnlarge se = _potObject.GetComponent<ShrinkEnlarge>();
        _potContainer.UpdateDroppableStatus(se.GetSizeFactor());
        if (se.GetSizeFactor() != 1f)
        {
            return false;
        }

        return true;

    }

    private bool IsGlobeCorrect()
    {
        return (_globeContainer.DroppedObjectCount == 1);
    }


    private bool IsRoomSolved()
    {

        if (!_isGlobeCorrect)
        {
            return false;
        }

        if (!_isPotCorrect)
        {
            return false;
        }

        if (!_isPotionBrewed)
        {
            return false;
        }

        return true;

    }
}
