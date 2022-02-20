using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{

    [SerializeField] private GameObject _interactPromptPrefab;    
    [SerializeField] private Transform _parent;
    [SerializeField] private bool _canBePickedUp = true;
    [SerializeField] private float _pickUpableSize = 0.5f;
    [SerializeField] private Interaction[] _couplets;
    [SerializeField] private bool _playCoupletsOnce;
    [SerializeField] private Collider2D _spriteCollider;

    private bool _coupletsPlayed;

    private GameObject _keyPrompt;
    private Transform _transform;
    private Vector3 _interactSpriteTransform;
    private bool _canInteract;

    private DropObject _containerObject;

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

    public void UpdatePickUpStatus(float currentSize)
    {
        _canBePickedUp = (currentSize == _pickUpableSize);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_canBePickedUp)
            return;

        if (collision.CompareTag("Player"))
        {
            var playerObj = collision.gameObject;
            if (playerObj.TryGetComponent(out PlayerController controller))
            {
                _keyPrompt = Instantiate(_interactPromptPrefab, _interactSpriteTransform, Quaternion.identity);
                controller.SetPickableObject(gameObject);
                _canInteract = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canInteract = false;
        var playerObj = collision.gameObject;
        if (playerObj.TryGetComponent(out PlayerController controller))
        {
            controller.UnsetPickableObject();
            Destroy(_keyPrompt);
        }
    }

    public bool PickUpObject()
    {
        // change sorting layer...
        if (_spriteCollider != null)
        {
            _spriteCollider.enabled = false;
        }

        if (_containerObject != null)
        {
            _containerObject.DoPickup();
            _containerObject = null;
        }

        Debug.Log("PickupObject " + _playCoupletsOnce + " " + _coupletsPlayed);

        if (!_playCoupletsOnce)
        {
            StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
        }
        else if (_playCoupletsOnce && !_coupletsPlayed)
        {
            StartCoroutine(GameManager.Instance.OpenDialogue(_couplets));
            _coupletsPlayed = true;
        }
        else
        {
            return true;
        }

        return false;
        

    }


    public void DropObject(DropObject containerObject)
    {
        // reset sorting layer...
        if (_spriteCollider != null)
        {
            _spriteCollider.enabled = true;
        }

        _containerObject = containerObject;

        gameObject.transform.parent = _parent;
        _interactSpriteTransform = _transform.position + new Vector3(0, 1f, 0);

    }

}
