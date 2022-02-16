using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryHandler : MonoBehaviour
{
    [SerializeField] private GameObject _exitTrigger;

    [SerializeField] private GameObject _globeObject;
    private Transform _globeTransform;
    [SerializeField] private GameObject _desiredGlobePosition;

    [SerializeField] private GameObject _potObject;
    private Transform _potTransform;
    [SerializeField] private GameObject _desiredPotPosition;

    [SerializeField] private GameObject _featherObject;
    private Transform _featherTransform;
    [SerializeField] private GameObject _desiredFeatherPosition;

    private void Awake()
    {
        AudioManager.Instance.PlayVersionTrack(2);
    }

    private void Start()
    {
        //_globeTransform = _globeObject.transform;
        //_potTransform = _potObject.transform;
        //_featherTransform = _featherObject.transform;

        // Check every second if puzzle has been solved and player can proceed
        //InvokeRepeating("CheckRoomSolved", 1, 1);

    }

    public void CheckRoomSolved()
    {

        if (IsRoomSolved())
        {

            // other possible dialogue?
            Debug.Log("Room solved!");

            _exitTrigger.SetActive(true);

            // then destory object?
        }
    }

    private bool IsRoomSolved()
    {
        //if (_paintings.Any(a => a.GetSizeFactor() != 1f))
        //{
        //    return false;
        //}
        ShrinkEnlarge se = null;

        se = _globeObject.GetComponent<ShrinkEnlarge>();
        if (se.GetSizeFactor() != 0.5f)
        {
            return false;
        }
        if (_globeTransform.position != _desiredGlobePosition.transform.position)
        {
            return false;
        }

        if (_potTransform.position != _desiredPotPosition.transform.position)
        {
            return false;
        }

        if (_featherTransform.position != _desiredFeatherPosition.transform.position)
        {
            return false;
        }


        return true;

    }
}
