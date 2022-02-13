using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWith : MonoBehaviour
{

    [SerializeField] private GameObject _interactPromptPrefab;
    private GameObject _keyPrompt;
    [SerializeField] private string _interactText;

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
                if (!controller.IsHoldingObject())
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

        if (GameManager.Instance.DialogueIsShowing())
        {
            GameManager.Instance.CloseDialogue();
            _keyPrompt.SetActive(true);
            return false;
        }
        else
        {
            GameManager.Instance.OpenDialogue(_interactText);
            _keyPrompt.SetActive(false);
            return true;
        }
    }

}
