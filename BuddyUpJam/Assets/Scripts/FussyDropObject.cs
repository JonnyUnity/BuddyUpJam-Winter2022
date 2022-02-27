using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FussyDropObject : DropObject
{

    [SerializeField] private List<GameObject> _acceptedItems;
    [SerializeField] private AudioClip _dropObjectClip;

    public int DroppedObjectCount { get; private set; }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerObj = collision.gameObject;
            if (playerObj.TryGetComponent(out PlayerController controller))
            {
                if (!controller.IsHoldingObject())
                    return;

                if (!_acceptedItems.Contains(controller.HeldObject()))
                    return;

                if (_canBeDroppedTo && _placedObject == null)
                {
                    _canInteract = true;
                    _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
                    controller.SetDropObject(this);

                }
                else if (_placedObject != null)
                {
                    // pick back up?
                }
            }

        }
    }


    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (_canInteract)
            return;

        if (collision.CompareTag("Player"))
        {
            var playerObj = collision.gameObject;
            if (playerObj.TryGetComponent(out PlayerController controller))
            {
                if (!controller.IsHoldingObject())
                    return;

                if (!_acceptedItems.Contains(controller.HeldObject()))
                    return;

                if (_canBeDroppedTo && _placedObject == null)
                {
                    _canInteract = true;
                    _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
                    controller.SetDropObject(this);

                }
                else if (_placedObject != null)
                {
                    // pick back up?
                }
            }

        }
    }


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
