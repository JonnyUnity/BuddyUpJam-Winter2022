using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoomHandler : MonoBehaviour
{

    [SerializeField] private InteractWith _drawerInteraction;
    [SerializeField] private ContainerInteraction _bedInteraction;
    [SerializeField] private ContainerInteraction _wardrobeInteraction;

    [SerializeField] private GameObject _exitTrigger;
    [SerializeField] private ShrinkEnlarge _exitDoor;
    [SerializeField] private InteractWith _doorInteraction;
    private bool _bellPlayed;
    
    private void Start()
    {
        InvokeRepeating(nameof(CheckRoomSolved), 1, 1);
    }

    public void CheckRoomSolved()
    {

        if (IsRoomSolved())
        {

            Destroy(_doorInteraction);

            if (!_bellPlayed)
            {
                AudioManager.Instance.PlayUnlockDoorClip();
                _bellPlayed = true;
            }            

            Debug.Log("Room solved!");

            _exitTrigger.SetActive(true);
        }
    }

    private bool IsRoomSolved()
    {
        // enable other interactions
        if (_drawerInteraction.AlreadyInteractedWith())
        {
            _bedInteraction.enabled = true;

        }
        if (_bedInteraction.AlreadyInteractedWith())
        {
            _wardrobeInteraction.enabled = true;
        }

        if (_exitDoor.GetSizeFactor() == 2f)
        {
            return true;
        }

        return false;
    }

}
