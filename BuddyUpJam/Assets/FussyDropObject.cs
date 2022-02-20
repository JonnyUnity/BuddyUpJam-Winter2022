using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FussyDropObject : DropObject
{

    [SerializeField] List<GameObject> _acceptedItems;

    public override void DoDrop(GameObject objectBeingDropped)
    {

        if (_acceptedItems.Contains(objectBeingDropped))
        {
            base.DoDrop(objectBeingDropped);
        }        
    }

}
