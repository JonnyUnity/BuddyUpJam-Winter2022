using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HallwayHandler : MonoBehaviour
{

    [SerializeField] private GameObject _exitTrigger;

    [SerializeField] private List<ShrinkEnlarge> _paintings;

    void Update()
    {
        CheckRoomSolved();
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
        if (_paintings.Any(a => a.GetSizeFactor() != 1f))
        {
            return false;
        }

        return true;
      
    }

}
