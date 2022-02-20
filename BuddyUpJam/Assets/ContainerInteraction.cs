using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInteraction : InteractWith
{
    [SerializeField] private GameObject _containedObject;

    //private void Awake()
    //{
    //    _transform = transform;
    //    //_interactSpriteTransform = _transform.position + new Vector3(0, 1f, 0);
    //    _interactSpriteTransform = _interactPromptPosition.position;
    //}

    //private void Update()
    //{
    //    if (!_canInteract)
    //        return;
    //    if (GameManager.Instance.GetState() == GameStatesEnum.NARRATION)
    //    {
    //        _keyPrompt.SetActive(false);
    //    }
    //    else
    //    {
    //        _keyPrompt.SetActive(true);

    //        var pos = _keyPrompt.transform.position;

    //        float newY = _interactSpriteTransform.y + (0.1f * Mathf.Sin(Time.time * 3f));
    //        _keyPrompt.transform.position = new Vector3(pos.x, newY, 0);
    //    }



    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        var playerObj = collision.gameObject;
    //        if (playerObj.TryGetComponent(out PlayerController controller))
    //        {
    //            if (!controller.IsHoldingObject() && !controller.IsResizing())
    //            {
    //                _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
    //                //controller.SetInteractableObject(this);
    //                _canInteract = true;
    //            }
    //        }

    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (_canInteract)
    //        return;

    //    if (collision.CompareTag("Player"))
    //    {
    //        var playerObj = collision.gameObject;
    //        if (playerObj.TryGetComponent(out PlayerController controller))
    //        {
    //            if (!controller.IsHoldingObject() && !controller.IsResizing())
    //            {
    //                _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
    //                //controller.SetInteractableObject(this);
    //                _canInteract = true;
    //            }
    //        }

    //    }
    //}


    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    _canInteract = false;
    //    var playerObj = collision.gameObject;
    //    if (playerObj.TryGetComponent(out PlayerController controller))
    //    {
    //        controller.UnsetInteractableObject();
    //        Destroy(_keyPrompt);
    //    }
    //}

    public override GameObject DoInteraction()
    {

        StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
        if (_singleInteraction)
        {
            _alreadyInteracted = true;
        }
        return _containedObject;
    }

}
