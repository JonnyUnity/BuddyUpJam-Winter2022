using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandPickup : MonoBehaviour
{

    [SerializeField] private GameObject _interactPromptPrefab;
    private GameObject _keyPrompt;

    private Transform _transform;
    private Vector3 _interactSpriteTransform;
    private bool _canInteract;
    [SerializeField] private bool _canBePickedUp = true;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _transform = transform;
        _interactSpriteTransform = _transform.position + new Vector3(0, 1f, 0);
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

}
