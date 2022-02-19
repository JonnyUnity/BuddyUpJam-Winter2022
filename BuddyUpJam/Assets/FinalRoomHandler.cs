using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoomHandler : MonoBehaviour
{
    [SerializeField] private GameObject _exitTrigger;
    [SerializeField] private GameObject _exitKey;
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        InvokeRepeating("CheckRoomSolved", 1, 1);
    }


    private void CheckRoomSolved()
    {

        if (IsRoomSolved())
        {
            Debug.Log("Room Solved!");

            _exitTrigger.SetActive(true);
        }

    }


    private bool IsRoomSolved()
    {
        //return _exitKey.activeInHierarchy && _playerController.IsHoldingObject();
        return _exitKey.activeInHierarchy;
    }


}
