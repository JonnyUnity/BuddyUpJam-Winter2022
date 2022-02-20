using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWith : MonoBehaviour
{

    [SerializeField] protected GameObject _interactPromptPrefab;
    [SerializeField] protected Transform _interactPromptPosition;
    [SerializeField] protected Interaction[] _couplets;
    [SerializeField] protected bool _singleInteraction;
    [SerializeField] protected AudioClip[] _searchAudioClip;


    protected GameObject _keyPrompt;
    protected Transform _transform;
    protected Vector3 _interactSpriteTransform;
    protected bool _canInteract;
    protected bool _alreadyInteracted;

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

        if (_keyPrompt == null)
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
        if (_singleInteraction && _alreadyInteracted)
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_canInteract || _singleInteraction && _alreadyInteracted)
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
        }
        if (_keyPrompt != null)
        {
            Destroy(_keyPrompt);
        }       

    }

    public virtual GameObject DoInteraction()
    {
        PlayRandomAudioClip();

        StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
        if (_singleInteraction)
        {
            _alreadyInteracted = true;
            Destroy(_keyPrompt);
        }
        return null;
    }

    protected void PlayRandomAudioClip()
    {
        if (_searchAudioClip != null && _searchAudioClip.Length > 0)
        {
            int index = Random.Range(0, _searchAudioClip.Length - 1);
            AudioManager.Instance.PlayClip(_searchAudioClip[index]);
        }


    }

}
