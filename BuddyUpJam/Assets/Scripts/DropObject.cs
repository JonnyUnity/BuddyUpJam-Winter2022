using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{

    [SerializeField] private Transform _dropPosition;
    [SerializeField] private GameObject _interactPromptPrefab;
    private GameObject _keyPrompt;

    private GameObject _placedObject;

    private Transform _transform;
    private Vector3 _interactSpriteTransform;
    private bool _canInteract;

    private void Awake()
    {
        _transform = transform;
        _interactSpriteTransform = _transform.position + new Vector3(0, 1f, 0);
    }

    private void Update()
    {
        if (!_canInteract)
            return;

        var pos = _keyPrompt.transform.position;

        float newY = _interactSpriteTransform.y + (0.1f * Mathf.Sin(Time.time * 3f));
        _keyPrompt.transform.position = new Vector3(pos.x, newY, 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerObj = collision.gameObject;
            if (playerObj.TryGetComponent(out PlayerController controller))
            {
                if (controller.IsHoldingObject())
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canInteract)
            return;

        if (collision.CompareTag("Player"))
        {
            var playerObj = collision.gameObject;
            if (playerObj.TryGetComponent(out PlayerController controller))
            {
                if (controller.IsHoldingObject())
                {
                    _canInteract = true;
                    if (_keyPrompt == null)
                    {
                        _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
                        controller.SetDropObject(this);
                    }                    

                }
                else if (_placedObject != null)
                {
                    // pick back up?
                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var playerObj = collision.gameObject;
        if (playerObj.TryGetComponent(out PlayerController controller))
        {
            _canInteract = false;
            controller.UnsetDropObject();
            if (_keyPrompt != null)
            {
                Destroy(_keyPrompt);
            }
        }
    }

    public void DoDrop(GameObject objectBeingDropped)
    {
        objectBeingDropped.transform.parent = null;
        objectBeingDropped.transform.position = _dropPosition.position;
        _placedObject = objectBeingDropped;
        PickUp pickup = _placedObject.GetComponent<PickUp>();
        pickup.DropObject();
        if (_keyPrompt != null)
        {
            Destroy(_keyPrompt);
        }
        _canInteract = false;
    }




}
