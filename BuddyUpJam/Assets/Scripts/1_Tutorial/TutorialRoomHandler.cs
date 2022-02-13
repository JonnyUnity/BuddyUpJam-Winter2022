using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomHandler : MonoBehaviour
{



    [SerializeField] private GameObject _exitTrigger;

    [SerializeField] private ShrinkEnlarge _exitDoor;

    private void Start()
    {
        InvokeRepeating("CheckRoomSolved", 1, 1);
    }

    public void CheckRoomSolved()
    {

        if (IsRoomSolved())
        {

            // other possible dialogue?
            Debug.Log("Room solved!");

            _exitTrigger.SetActive(true);
        }
    }

    private bool IsRoomSolved()
    {


        if (_exitDoor.GetSizeFactor() == 2f)
        {
            return true;
        }


        return false;
    }


    // on action
    // every time the player interacts with something in the room, check the room logic to see what changes

}
