using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryHandler : MonoBehaviour
{
    [SerializeField] private GameObject _exitTrigger;
    [SerializeField] private GameObject _potObject;
    [SerializeField] private GameObject _desiredPotPosition;

    [SerializeField] private GameObject _globeStandObject;
    private FussyDropObject _globeContainer;

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

    private void Awake()
    {
        AudioManager.Instance.PlayVersionTrack(2);
    }

    private void Start()
    {
        //_globeTransform = _globeObject.transform;
        _potTransform = _potObject.transform;
        //_featherTransform = _featherObject.transform;
        _globeContainer = _globeStandObject.GetComponent<FussyDropObject>();
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
                Debug.Log("Globe correct!");

                StartCoroutine(GameManager.Instance.OpenDialogue(_globeCorrectCouplets));
                _isGlobeCorrect = true;
                _globeCorrectNarrationDone = true;
            }
        }

        if (!_potCorrectNarrationDone)
        {
            if (IsPotCorrect())
            {
                Debug.Log("Pot correct!");
                
                StartCoroutine(GameManager.Instance.OpenDialogue(_potCorrectCouplets));
                _isPotCorrect = true;
                _potCorrectNarrationDone = true;
            }
        }

        if (!_potionBrewedNarrationDone)
        {

            if (IsPotionBrewed())
            {
                Debug.Log("Potion brewed!");
                StartCoroutine(GameManager.Instance.OpenDialogue(_potionBrewedCouplets));
                _isPotionBrewed = true;
                _potionBrewedNarrationDone = true;
            }

        }

        if (IsRoomSolved())
        {

            Debug.Log("Room solved!");
            _exitTrigger.SetActive(true);

        }
    }

    private bool IsPotionBrewed()
    {
        return (_potContainer.DroppedObjectCount == 2); // feather and flower
        //return (!_featherObject.activeInHierarchy && !_flowerObject.activeInHierarchy);
    }

    private bool IsPotCorrect()
    {
        
        if (_potTransform.position == _desiredPotPosition.transform.position)
        {
            //_potPickUp.enabled = false;
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

        //if (_globeTransform.position != _desiredGlobePosition.transform.position)
        //{
        //    return false;
        //}

        //ShrinkEnlarge se = _globeObject.GetComponent<ShrinkEnlarge>();
        //if (se.GetSizeFactor() != 1f)
        //{
        //    return false;
        //}

        //return true;
    }


    private bool IsRoomSolved()
    {
        //ShrinkEnlarge se = null;

        //se = _globeObject.GetComponent<ShrinkEnlarge>();
        //if (se.GetSizeFactor() != 0.5f)
        //{
        //    return false;
        //}

        if (!_isGlobeCorrect)
        {
            return false;
        }

        //if (_globeTransform.position != _desiredGlobePosition.transform.position)
        //{
        //    return false;
        //}

        if (!_isPotCorrect)
        {
            return false;
        }

        //if (_potTransform.position != _desiredPotPosition.transform.position)
        //{
        //    return false;
        //}

        if (!_isPotionBrewed)
        {
            return false;
        }

        return true;

    }
}
