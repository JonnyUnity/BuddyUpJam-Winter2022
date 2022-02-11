using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{

    private PlayerController _controller;

    private void Awake()
    {
        _controller = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CanChangeSize"))
        {
            Debug.Log("hovering over selectable object " + collision.gameObject.name);
            var objectAnchor = collision.transform.parent.gameObject;
            Debug.Log(objectAnchor.name + " is the parent!");
            _controller.SetHighlightedObject(collision.gameObject, objectAnchor);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _controller.UnsetHighlightedObject();
    }

}
