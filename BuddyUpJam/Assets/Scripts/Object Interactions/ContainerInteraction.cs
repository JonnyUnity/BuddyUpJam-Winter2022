using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteraction : InteractWith
{
    [SerializeField] private GameObject _containedObject;


    public override GameObject DoInteraction()
    {
        PlayRandomAudioClip();

        StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
        if (_singleInteraction)
        {
            _alreadyInteracted = true;
            Destroy(_keyPrompt);
        }
        return _containedObject;
    }

}
