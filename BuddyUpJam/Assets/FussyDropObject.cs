using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FussyDropObject : DropObject
{

    [SerializeField] private List<GameObject> _acceptedItems;
    [SerializeField] private AudioClip _dropObjectClip;

    public int DroppedObjectCount { get; private set; }

    public override void DoDrop(GameObject objectBeingDropped)
    {

        if (_acceptedItems.Contains(objectBeingDropped))
        {
            AudioManager.Instance.PlayClip(_dropObjectClip);
            DroppedObjectCount++;
            base.DoDrop(objectBeingDropped);
            
        }        
    }

}
