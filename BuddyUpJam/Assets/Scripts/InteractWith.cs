using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWith : MonoBehaviour
{

    [SerializeField] private GameObject _interactPromptPrefab;
    [SerializeField] private Transform _interactPromptPosition;
    [SerializeField] private string _interactText;

    [SerializeField] private Interaction[] _couplets;

    private GameObject _keyPrompt;
    private Transform _transform;
    private Vector3 _interactSpriteTransform;
    private bool _canInteract;

    private void Awake()
    {
        _transform = transform;
        //_interactSpriteTransform = _transform.position + new Vector3(0, 1f, 0);
        _interactSpriteTransform = _interactPromptPosition.position;
    }

    private void Update()
    {
        if (!_canInteract)
            return;
        if (GameManager.Instance.GetState() == GameStatesEnum.NARRATION)
        {
            _keyPrompt.SetActive(false);
        }
        else
        {
            _keyPrompt.SetActive(true);

            var pos = _keyPrompt.transform.position;

            float newY = _interactSpriteTransform.y + (0.1f * Mathf.Sin(Time.time * 3f));
            _keyPrompt.transform.position = new Vector3(pos.x, newY, 0);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerObj = collision.gameObject;
            if (playerObj.TryGetComponent(out PlayerController controller))
            {
                if (!controller.IsHoldingObject() && !controller.IsResizing())
                {
                    _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
                    controller.SetInteractableObject(this);
                    _canInteract = true;
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
                if (!controller.IsHoldingObject() && !controller.IsResizing())
                {
                    _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
                    controller.SetInteractableObject(this);
                    _canInteract = true;
                }
            }

        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        _canInteract = false;
        var playerObj = collision.gameObject;
        if (playerObj.TryGetComponent(out PlayerController controller))
        {
            controller.UnsetInteractableObject();
            Destroy(_keyPrompt);
        }
    }

    public bool DoInteraction()
    {

        StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
        return true;

        //if (GameManager.Instance.DialogueIsShowing())
        //{
        //    GameManager.Instance.CloseDialogue();
        //    _keyPrompt.SetActive(true);
        //    return false;
        //}
        //else
        //{
        //    GameManager.Instance.OpenDialogue(_interactText);
        //    _keyPrompt.SetActive(false);
        //    return true;
        //}
    }

}
