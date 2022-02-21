using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{

    private PlayerController _controller;
    private Renderer _renderer;

    private void Awake()
    {
        _controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CanChangeSize") && GameManager.Instance.CanSelect())
        {
            _renderer = collision.gameObject.GetComponent<Renderer>();
            _renderer.material.SetFloat("_ShowOutline", 1f);

            var objectAnchor = collision.transform.parent.gameObject;
            _controller.SetHighlightedObject(collision.gameObject, objectAnchor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_renderer != null)
        {
            _renderer.material.SetFloat("_ShowOutline", 0f);
            _controller.UnsetHighlightedObject();
        }
    }

}
