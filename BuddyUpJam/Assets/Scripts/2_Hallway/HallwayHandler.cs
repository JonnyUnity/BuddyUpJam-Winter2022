using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HallwayHandler : MonoBehaviour
{

    [SerializeField] private GameObject _exitTrigger;
    [SerializeField] private List<ShrinkEnlarge> _paintings;
    [SerializeField] private InteractWith _doorInteraction;

    private void Awake()
    {
        AudioManager.Instance.PlayVersionTrack(1);
    }

    private void Start()
    {
        InvokeRepeating(nameof(CheckRoomSolved), 1, 0.1f);
    }

    public void CheckRoomSolved()
    {

        if (IsRoomSolved())
        {
            Destroy(_doorInteraction);
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
